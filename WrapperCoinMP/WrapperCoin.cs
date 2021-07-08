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
                       string[] colNames, string[] rowNames, string objName) => 
            LoadProblem(problem, colCount, rowCount, nzCount, rangeCount, objectSense,
                        objectConst, objectCoeffs, lowerBounds, upperBounds, null, rowLower,
                        rowUpper, matrixBegin, matrixCount, matrixIndex, matrixValues,
                        colNames, rowNames, objName);

        public static int InitValues(WrapProblem problem, double[] values) => CoinLoadInitValues(problem.getProblem(), values);

       //'I' -> intero; 'C' -> Reale? 'B' -> bynary
        public static int LoadInteger(WrapProblem problem, char[] columnType) => CoinLoadInteger(problem.getProblem(), columnType);
        // priorità default di coin == 1000; 1 == max priorità; priorbranch == ???
        public static int LoadPriority(WrapProblem problem, int priorCount, int[] priorIndex, int[] priorValues, int[] PriorBranch) =>
            CoinLoadPriority(problem.getProblem(), priorCount, priorIndex, priorValues, PriorBranch);
        public static int LoadSos(WrapProblem problem, int sosCount, int sosNZCount, int[] sosType,
            int[] sosPrior, int[] sosBegin, int[] sosIndex, double[] sosRef) => CoinLoadSos(problem.getProblem(), sosCount, sosNZCount, sosType, sosPrior, sosBegin,
                sosIndex, sosRef);

        public static int LoadSemiCont(WrapProblem problem, int semiCount, int[] semiIndex) => CoinLoadSemiCont(problem.getProblem(), semiCount, semiIndex);
        //-------------------------------------------------------------------//
        // questi due metodi li inserisco in vista di eventuali aggiornamebti futuri di CoinMP; attualmente non sono implementati dalla dll che wrappo
        public static int LoadQuadratic(WrapProblem problem, int[] quadBegin, int[] quadCount, int[] quadIndex, double[] quadValues) =>
            CoinLoadQuadratic(problem.getProblem(), quadBegin, quadCount, quadIndex, quadValues);

        public static int CoinLoadNonlinear(WrapProblem problem, int nlpTreeCount, int nlpLineCount, int[] nlpBegin, int[] nlpOper, int[] nlpArg1,
            int[] nlpArg2, int[] nlpIndex1, int[] nlpIndex2, double[] nlpValue1, double[] nlpValue2) => CoinLoadNonlinear(problem.getProblem(), nlpTreeCount,
                nlpLineCount, nlpBegin, nlpOper, nlpArg1, nlpArg2, nlpIndex1, nlpIndex2, nlpValue1, nlpValue2);
        //-------------------------------------------------------------------//

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
