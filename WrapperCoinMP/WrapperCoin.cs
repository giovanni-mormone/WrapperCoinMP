using System;
using System.Runtime.InteropServices;
using System.Text;
using WrapperCoinMP;
namespace WrapperCoinMP
{

    public struct WrapProblem
    {
        private IntPtr problem;
        
        public WrapProblem(IntPtr problem)
        {
            this.problem = problem;
        }
        public IntPtr getProblem()
        {
            return problem;
        }

    }
    public static partial class WrapperCoin
    {
        private const int SOLV_CALL_SUCCESS = 0;
        private const int SOLV_CALL_FAILED = -1;

        public const int SOLV_OBJSENS_MAX = -1;
        public const int SOLV_OBJSENS_MIN = 1;

        public const int SOLV_METHOD_DEFAULT = 0;

        public static double GetVersion() => CoinGetVersion();
        public static string GetVersionStr() => Marshal.PtrToStringAnsi(CoinGetVersionStrIntPtr());
        public static int FreeSolver() => CoinFreeSolver();
        public static string GetSolverName()
        {
            StringBuilder builder = new StringBuilder();
            CoinGetSolverNameBuf(builder, builder.Capacity);
            return builder.ToString();
        }
        public static double GetInfinity() => CoinGetInfinity();
        public static WrapProblem CreateProblem(string problemName) => new WrapProblem(CoinCreateProblem(problemName));

        public static int LoadProblem(WrapProblem problem, int colCount, int rowCount, int nzCount,
                        int rangeCount, int objectSense, double objectConst, double[] objectCoeffs,
                        double[] lowerBounds, double[] upperBounds, char[] rowType, double[] rhsValues,
                        double[] rangeValues, int[] matrixBegin, int[] matrixCount, int[] matrixIndex,
                        double[] matrixValues, string[] colNames, string[] rowNames, string objName) =>
                            CoinLoadProblemBuf(problem.getProblem(), colCount, rowCount, nzCount, rangeCount, objectSense, objectConst,
                                objectCoeffs, lowerBounds, upperBounds, rowType, rhsValues, rangeValues, matrixBegin, matrixCount, matrixIndex,
                                matrixValues, BufferizeArray(colNames), BufferizeArray(rowNames), objName);

        
        //aggiungere spiegazione migliore: si fa così perchè quando hai vincoli delle row con un range non usi rowtype,
        //ma si usano due vettori che indicano i vicoli della riga; questo viene dalla libreria, si vede a riga 360 di CoinProblem.c
        public static int LoadProblem(WrapProblem problem, int colCount, int rowCount, int nzCount,
                       int rangeCount, int objectSense, double objectConst, double[] objectCoeffs,
                       double[] lowerBounds, double[] upperBounds, double[] rowLower, double[] rowUpper,
                       int[] matrixBegin, int[] matrixCount, int[] matrixIndex, double[] matrixValues,
                       string[] colNames, string[] rowNames, string objName)
        {
            return LoadProblem(problem, colCount, rowCount, nzCount, rangeCount, objectSense,
                        objectConst, objectCoeffs, lowerBounds, upperBounds, null, rowLower,
                        rowUpper, matrixBegin, matrixCount, matrixIndex, matrixValues,
                        colNames, rowNames, objName);
        }

        public static int OptimizeProblem(WrapProblem problem)
        {
            return CoinOptimizeProblem(problem.getProblem(), SOLV_METHOD_DEFAULT);
        }

        public static int GetSolutionValues(WrapProblem problem, [In, Out] double[] activity,
                       [In, Out] double[] reducedCost, [In, Out] double[] slackValues, [In, Out] double[] shadowPrice) =>
            CoinGetSolutionValues(problem.getProblem(), activity, reducedCost, slackValues, shadowPrice);

        public static double GetObjectValue(WrapProblem problem) => CoinGetObjectValue(problem.getProblem());
        public static double GetMipBestBound(WrapProblem problem) => CoinGetMipBestBound(problem.getProblem());
        public static int GetIterCount(WrapProblem problem) => CoinGetIterCount(problem.getProblem());
        private static string BufferizeArray(string[] toBuff)
        {
            StringBuilder builder = new();
            if(toBuff.GetLength(0) > 0)
            {
                int position = 0;
                while(position < toBuff.GetLength(0))
                {
                    builder.Append(toBuff[position] + "\0");
                    position++;
                }
            }
            return builder.ToString();
        }
    }
}
