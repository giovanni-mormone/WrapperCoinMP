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
        public static int UnloadProblem(WrapProblem problem) => CoinUnloadProblem(problem.getProblem());

        // metodo che torna 1 se ci sono 0 colonne; 2 se le righe,  non zero o il range count è <0;
        // 3 se le range > rows; 4 se objsens non è max o min; 5 se rowtype esiste e gli dai un valore =! da LEGRN;
        // 6 se con NZ > 0: matrixbegin è <0; 7 se matrixcount <0 e 8 se MatrixBegin[i+1] - pProblem->MatrixBegin[i] != pProblem->MatrixCount[i]
        // se il valore di matrixbegin in posizione colcount è != da NZcount torna 100 + MatrixBegin[pProblem->ColCount];
        // 10 se un valore di matindex è <0; 11 se un val di matindex è > di rowcount; 
        // 12 se ci son low e upp bound e un low è > di un upp; 13 se con coltype trova valori != da CBI;
        // 14 se non ci sono nomi a colonne; 15 se la lunghezza dei nomi col è > di 100 * num col;
        // 16 se no nomi row; 17 se la lunghezza dei nomi row è > di 100 * num row; 0 se tutto ok
        public static int CheckProblem(WrapProblem problem) => CoinCheckProblem(problem.getProblem());

        public static string GetProblemName(WrapProblem problem)
        {
            StringBuilder builder = new StringBuilder(100);
            CoinGetProblemNameBuf(problem.getProblem(), builder, builder.Capacity);
            return builder.ToString();
        }

        public static int GetColCount(WrapProblem problem) => CoinGetColCount(problem.getProblem());
        public static int GetRowCount(WrapProblem problem) => CoinGetRowCount(problem.getProblem());

        public static string GetColName(WrapProblem problem, int index) => Marshal.PtrToStringAnsi(CoinGetColName(problem.getProblem(), index));
        public static string GetRowName(WrapProblem problem, int index) => Marshal.PtrToStringAnsi(CoinGetRowName(problem.getProblem(), index));
        public static int OptimizeProblem(WrapProblem problem)
        {
            return CoinOptimizeProblem(problem.getProblem(), SOLV_METHOD_DEFAULT);
        }

        //0:	Optimal solution found
        //1:	Problem primal infeasible
        //2:	Problem dual infeasible
        //3:	Stopped on iterations
        //4:    Stopped due to errors
        //5:    Stopped by user
        public static int GetSolutionStatus(WrapProblem problem) => CoinGetSolutionStatus(problem.getProblem());
        public static string GetSolutionStatusText(WrapProblem problem) => Marshal.PtrToStringAnsi(CoinGetSolutionText(problem.getProblem()));

        public static double GetObjectValue(WrapProblem problem) => CoinGetObjectValue(problem.getProblem());
        public static double GetMipBestBound(WrapProblem problem) => CoinGetMipBestBound(problem.getProblem());
        public static int GetIterCount(WrapProblem problem) => CoinGetIterCount(problem.getProblem());
        public static int GetMipNodeCount(WrapProblem problem) => CoinGetMipNodeCount(problem.getProblem());
        //************************
        // i 3 met qua sotto prendono prima array lunghi quanto n colonne del prob, poi quanto nrows; a metà sempre
        public static int GetSolutionValues(WrapProblem problem, [In, Out] double[] activity,
                       [In, Out] double[] reducedCost, [In, Out] double[] slackValues, [In, Out] double[] shadowPrice) =>
            CoinGetSolutionValues(problem.getProblem(), activity, reducedCost, slackValues, shadowPrice);

        public static int GetSolutionRanges(WrapProblem problem, [In, Out] double[] objLoRange,
                      [In, Out] double[] objUpRange, [In, Out] double[] rhsLoRange, [In, Out] double[] rhsUpRange) =>
            CoinGetSolutionRanges(problem.getProblem(), objLoRange, objUpRange, rhsLoRange, rhsUpRange);

        public static int GetSolutionBasis(WrapProblem problem, [In, Out] int[] colStatus,
                        [In, Out] double[] rowStatus) => CoinGetSolutionBasis(problem.getProblem(), colStatus, rowStatus);
        //************************
        public static int GetIntOptionMinMax(WrapProblem problem, int optionNr,
                        [In, Out] int[] minValue, [In, Out] int[] maxValue)  => CoinGetIntOptionMinMax(problem.getProblem(), optionNr,
                        minValue, maxValue);

        public static string GetOptionName(WrapProblem problem, int optionID) => Marshal.PtrToStringAnsi(CoinGetOptionName(problem.getProblem(), optionID));


        public static int GetIntOption(WrapProblem problem, int optionID) => CoinGetIntOption(problem.getProblem(), optionID);


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
