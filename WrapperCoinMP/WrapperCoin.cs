using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using WrapperCoinMP;
namespace WrapperCoinMP
{
    /// <summary>
    /// Structure that wraps the pointer to a problem created by coinmp dll
    /// </summary>
    public struct WrapProblem
    {
        private IntPtr problem;
        
        public WrapProblem(IntPtr problem)
        {
            this.problem = problem;
        }
        /// <summary>
        /// Method used to get the pointer to a problem.
        /// </summary>
        /// <returns>The IntPtr to the problem, wrapped</returns>
        public IntPtr getProblem()
        {
            return problem;
        }

    }
    /// <summary>
    /// Utility class that wraps the methods of the coinmp dll.
    /// </summary>
    public static partial class WrapperCoin
    {
        private const int SOLV_CALL_SUCCESS = 0;
        private const int SOLV_CALL_FAILED = -1;
        /*
        This variables apparently are not used by the coinmp dll; to change the method maybe we should deal with the options
        /// <summary>
        /// Constant used to use the primal solve method
        /// </summary>
        public const int SOLV_METHOD_PRIMAL = 0x1;
        /// <summary>
        /// Constant used to use the dual solve method
        /// </summary>
        public const int SOLV_METHOD_DUAL = 0x2;
        /// <summary>
        /// Constant used to use the network solve method
        /// </summary>
        public const int SOLV_METHOD_NETWORK = 0x4;
        /// <summary>
        /// Constant used to use the barrier solve method
        /// </summary>
        public const int SOLV_METHOD_BARRIER = 0x8;
        */
        /// <summary>
        /// Constant used to set a problem as maximization problem
        /// </summary>
        public const int SOLV_OBJSENS_MAX = -1;
        /// <summary>
        /// Constant used to set a problem as minimi problem
        /// </summary>
        public const int SOLV_OBJSENS_MIN = 1;

        //public const int SOLV_METHOD_DEFAULT = 0;

        /// <summary>
        /// Method used to initialize the solver and load the coin dll. It searches in the debug/release path if not specified.
        /// </summary>
        /// <param name="path"> The path where the coin mp dll is found; e.g. C://percorso//della/dll/CoinMP.dll</param>
        public static void InitSolver(string path = "CoinMP.dll")
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            LoadLibrary(path);
            CoinInitSolver("");
        }

        /// <summary>
        /// Method used to get the version of the coinmp dll wrapped
        /// </summary>
        /// <returns>A real value representing the coinmp dll wrapped</returns>
        public static double GetVersion() => CoinGetVersion();
        /// <summary>
        /// Method used to get the version of the coinmp dll wrapped
        /// </summary>
        /// <returns>A string representation of the coinmp dll wrapped</returns>
        public static string GetVersionStr() => Marshal.PtrToStringAnsi(CoinGetVersionStrIntPtr());
        /// <summary>
        /// Method used to free the solver utilized.
        /// </summary>
        /// <returns>The state of the operation</returns>
        public static int FreeSolver() => CoinFreeSolver();
        /// <summary>
        /// Method used to get the name of the solver utilized.
        /// </summary>
        /// <returns>A strimg representing the solver utilized</returns>
        public static string GetSolverName()
        {
            StringBuilder builder = new StringBuilder();
            CoinGetSolverNameBuf(builder, builder.Capacity);
            return builder.ToString();
        }
        /// <summary>
        /// Method used to get the infinity valeu utilized in the wrapper.
        /// </summary>
        /// <returns>The double infinity vsalue represented by coinmp dll</returns>
        public static double GetInfinity() => CoinGetInfinity();
        /// <summary>
        /// Method used to create a coinmp problem
        /// </summary>
        /// <param name="problemName"> A string representing the name assigned to the problem</param>
        /// <returns>A <see cref="WrapProblem"/> with the problem created</returns>
        public static WrapProblem CreateProblem(string problemName) => new WrapProblem(CoinCreateProblem(problemName));
        /// <summary>
        /// Method used to load the problem data.
        /// </summary>
        /// <param name="problem">The <see cref="WrapProblem"/> where the problem will be loaded</param>
        /// <param name="colCount">The number of columns of the problem</param>
        /// <param name="rowCount">The number of rows of the problem</param>
        /// <param name="nzCount">The number of non zero elements of the problem</param>
        /// <param name="rangeCount">The number of variables in a rangeù, always 0, use the overload if your problem has a range</param>
        /// <param name="objectSense">Indicates if a problem is a maximization(-1) or a minimization(1). Either <see cref="SOLV_OBJSENS_MAX"/> or <see cref="SOLV_OBJSENS_MIN"/> sould be used</param>
        /// <param name="objectConst">Value that indicates a constant values to add to the objecive result</param>
        /// <param name="objectCoeffs">Double array used to indicate the coeffs of the objective function</param>
        /// <param name="lowerBounds">Array that represents the lower bounds of the variables</param>
        /// <param name="upperBounds">Array that represents the upper bounds of the variables</param>
        /// <param name="rowType">Array of <see cref="char"/> representing the type of the row; 'L' represents a row with a Less or equal; 'E' a row Equal; 'G' a row greather than or equal</param>
        /// <param name="rhsValues">Array representing the right hand side values of a row</param>
        /// <param name="rangeValues">value that must be set to null; use the overload of this method with ranges.</param>
        /// <param name="matrixBegin">Array used to count the total of values of the problem values; it's first values should be zero, then for rach column add the number of non 
        /// zero values to the previous</param>
        /// <param name="matrixCount">Array used to count the values of each column</param>
        /// <param name="matrixIndex">Array that represent the row id of each non zero value column wise</param>
        /// <param name="matrixValues">Array that represents the value of the coeff of the values columnwise</param>
        /// <param name="colNames">Array that represents the name of the columns</param>
        /// <param name="rowNames">Array that represents the name of the rows</param>
        /// <param name="objName">The name of the objective function</param>
        /// <returns>the state of the operation</returns>
        public static int LoadProblem(WrapProblem problem, int colCount, int rowCount, int nzCount,
                        int rangeCount, int objectSense, double objectConst, double[] objectCoeffs,
                        double[] lowerBounds, double[] upperBounds, char[] rowType, double[] rhsValues,
                        double[] rangeValues, int[] matrixBegin, int[] matrixCount, int[] matrixIndex,
                        double[] matrixValues, string[] colNames, string[] rowNames, string objName) =>
                            CoinLoadProblemBuf(problem.getProblem(), colCount, rowCount, nzCount, rangeCount, objectSense, objectConst,
                                objectCoeffs, lowerBounds, upperBounds, rowType, rhsValues, rangeValues, matrixBegin, matrixCount, matrixIndex,
                                matrixValues, BufferizeArray(colNames), BufferizeArray(rowNames), objName);

        /// <summary>
        /// Overload of the LoadProblem method used with ranges; 
        /// </summary>
        /// <param name="problem">The <see cref="WrapProblem"/> where the problem will be loaded</param>
        /// <param name="colCount">The number of columns of the problem</param>
        /// <param name="rowCount">The number of rows of the problem</param>
        /// <param name="nzCount">The number of non zero elements of the problem</param>
        /// <param name="rangeCount">The number of variables in a rangeù, always 0, use the overload if your problem has a range</param>
        /// <param name="objectSense">Indicates if a problem is a maximization(-1) or a minimization(1). Either <see cref="SOLV_OBJSENS_MAX"/> or <see cref="SOLV_OBJSENS_MIN"/> sould be used</param>
        /// <param name="objectConst">Value that indicates a constant values to add to the objecive result</param>
        /// <param name="objectCoeffs">Double array used to indicate the coeffs of the objective function</param>
        /// <param name="lowerBounds">Array that represents the lower bounds of the variables</param>
        /// <param name="upperBounds">Array that represents the upper bounds of the variables</param>
        /// <param name="rowLower">Lower bounds of each row</param>
        /// <param name="rowUpper">Upper bounds of each row</param>
        /// <param name="matrixBegin">Array used to count the total of values of the problem values; it's first values should be zero, then for each column add the number of non 
        /// zero values to the previous</param>
        /// <param name="matrixCount">Array used to count the values of each column</param>
        /// <param name="matrixIndex">Array that represent the row id of each non zero value column wise</param>
        /// <param name="matrixValues">Array that represents the value of the coeff of the values columnwise</param>
        /// <param name="colNames">Array that represents the name of the columns</param>
        /// <param name="rowNames">Array that represents the name of the rows</param>
        /// <param name="objName">The name of the objective function</param>
        /// <returns>the state of the operation</returns>
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


        /// <summary>
        /// Method to set initial values of the variables
        /// </summary>
        /// <param name="problem">The problem whom set the constraints, after it is initializated with load problem </param>
        /// <param name="values">The value of each variable</param>
        /// <returns></returns>
        public static int InitValues(WrapProblem problem, double[] values) => CoinLoadInitValues(problem.getProblem(), values);

        /// <summary>
        /// Method used to set the integer type constraint to each variable of a problem, to make it a MIP problem
        /// </summary>
        /// <param name="problem">The problem whom set the constraints, after it is initializated with load problem </param>
        /// <param name="columnType">The type of each column; it can be I to represent am integer, B to represent a Bynary or C a real</param>
        /// <returns>the state of the operation</returns>
        public static int LoadInteger(WrapProblem problem, char[] columnType) => CoinLoadInteger(problem.getProblem(), columnType);
        /// <summary>
        ///  Method used to copy a priority order to an LP problem; the default priority is 1000; the max is 1. 
        /// </summary>
        /// <param name="problem">The problem whom set the constraints, after it is initializated with load problem </param>
        /// <param name="priorCount">The number of priorities set</param>
        /// <param name="priorIndex">The index of the variables with priorities</param>
        /// <param name="priorValues">The value of the priority</param>
        /// <param name="PriorBranch">The direction of the branching; may be null</param>
        /// <returns></returns>
        public static int LoadPriority(WrapProblem problem, int priorCount, int[] priorIndex, int[] priorValues, int[] PriorBranch) =>
            CoinLoadPriority(problem.getProblem(), priorCount, priorIndex, priorValues, PriorBranch);
        /// <summary>
        /// Method used to copy SOS information to a problem object 
        /// </summary>
        /// <param name="problem">The problem whom set the constraints, after it is initializated with load problem </param>
        /// <param name="sosCount">The count of sos values</param>
        /// <param name="sosNZCount">The total number of non zero values of the sos set</param>
        /// <param name="sosType">Array containig the sos types; can be 1 or 2; the order of the next variables is in reference to this</param>
        /// <param name="sosPrior">An array containing priority values for each set. sosPrioe[i] specifies the priority for set i, and may take any nonnegative value. This array may be NULL; otherwise it must be of length at least numsos.</param>
        /// <param name="sosBegin">An array stating beginning indices; sosRef clarifies it</param>
        /// <param name="sosIndex">An array stating indices; sosRef clarifies it</param>
        /// <param name="sosRef">Arrays declaring the indices and weights for the sets. For every set, the indices and weights must be stored in sequential locations in sosIndex and sosRef
        /// , respectively, with sosBegin[j] containing the index of the beginning of set j. The weights must be unique in their array. For j less than numsos-1 the indices of set j must be 
        /// stored in sosIndex[sosBegin[j]], ..., sosIndex[sosBegin[j+1]-1] and the weights in sosRef[sosBegin[j], ..., sosRef[sosbeg[j+1]-1]. For the last set,j = numsos-1, the indices must 
        /// be stored in sosIndex[sosBegin[numsos-1]], ..., sosIndex[numsosnz-1] and the corresponding weights in sosRef[sosBegin[numsos-1]] ..., sosRef[numsosnz-1]. Hence, sosBegin must be of length at 
        /// least numsos, while sosIndex and sosRef must be of length at least numsosnz.
        /// Value can be null</param>
        /// <returns></returns>
        public static int LoadSos(WrapProblem problem, int sosCount, int sosNZCount, int[] sosType,
            int[] sosPrior, int[] sosBegin, int[] sosIndex, double[] sosRef) => CoinLoadSos(problem.getProblem(), sosCount, sosNZCount, sosType, sosPrior, sosBegin,
                sosIndex, sosRef);
        /// <summary>
        /// Method used to load semi cont values.
        /// </summary>
        /// <param name="problem">The problem whom set the constraints, after it is initializated with load problem </param>
        /// <param name="semiCount">The number of semi cont values</param>
        /// <param name="semiIndex">The index of the semi cont values</param>
        /// <returns></returns>
        public static int LoadSemiCont(WrapProblem problem, int semiCount, int[] semiIndex) => CoinLoadSemiCont(problem.getProblem(), semiCount, semiIndex);
        //-------------------------------------------------------------------//
        // questi due metodi li inserisco in vista di eventuali aggiornamebti futuri di CoinMP; attualmente non sono implementati dalla dll che wrappo
        public static int LoadQuadratic(WrapProblem problem, int[] quadBegin, int[] quadCount, int[] quadIndex, double[] quadValues) =>
            CoinLoadQuadratic(problem.getProblem(), quadBegin, quadCount, quadIndex, quadValues);

        public static int CoinLoadNonlinear(WrapProblem problem, int nlpTreeCount, int nlpLineCount, int[] nlpBegin, int[] nlpOper, int[] nlpArg1,
            int[] nlpArg2, int[] nlpIndex1, int[] nlpIndex2, double[] nlpValue1, double[] nlpValue2) => CoinLoadNonlinear(problem.getProblem(), nlpTreeCount,
                nlpLineCount, nlpBegin, nlpOper, nlpArg1, nlpArg2, nlpIndex1, nlpIndex2, nlpValue1, nlpValue2);
        //-------------------------------------------------------------------//
        /// <summary>
        /// Method used to free the memory from a problem
        /// </summary>
        /// <param name="problem">The problem to free</param>
        /// <returns>The status of the operation</returns>
        public static int UnloadProblem(WrapProblem problem) => CoinUnloadProblem(problem.getProblem());

        /// <summary>
        /// Method tha check the state of a loaded problem
        /// </summary>
        /// <param name="problem">The problem to check </param>
        /// <returns>1 if there are 0 columns; 2 if the rows or non zero values are less than 0; 3 if range is greater than rows; 4 if objsens is non max or min; 5 if the values of 
        /// rowtype is wrong; 6 if with a number of non zero greater than 0 matbeg is less than 0; 7 if  matrixcount <0 and 8 if MatrixBegin[i+1] - pProblem->MatrixBegin[i] != pProblem->MatrixCount[i]
        /// if matrixbegin in position colcount  !=  NZcount returns 100 + MatrixBegin[pProblem->ColCount]; 10 if 1 value of matindex è <0; 11 if 1 value of matindex is > rowcount; 
        /// 12 if with low and upp bound 1 lowbound > upbound; 13 if the coltype values != da CBI;  14 if the columns have not got a name; 15 if the length of the col names > di 100 * num col;
        /// 16 if the rowss have not got a name; 17 if the length of the row names > di 100 * num row; 0 if all ok
        /// </returns>
        public static int CheckProblem(WrapProblem problem) => CoinCheckProblem(problem.getProblem());
        /// <summary>
        /// Method used to get the name of a problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The name of the problem</returns>
        public static string GetProblemName(WrapProblem problem)
        {
            StringBuilder builder = new StringBuilder(100);
            CoinGetProblemNameBuf(problem.getProblem(), builder, builder.Capacity);
            return builder.ToString();
        }

        /// <summary>
        /// Method used to get the number of columns of a problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The number of columns</returns>
        public static int GetColCount(WrapProblem problem) => CoinGetColCount(problem.getProblem());

        /// <summary>
        /// Method used to get the number of rows of a problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The number of roes</returns>
        public static int GetRowCount(WrapProblem problem) => CoinGetRowCount(problem.getProblem());

        /// <summary>
        /// Method used to get the name of a column of a problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <param name="index">The index of the column</param>
        /// <returns>The name of the column</returns>
        public static string GetColName(WrapProblem problem, int index) => Marshal.PtrToStringAnsi(CoinGetColName(problem.getProblem(), index));
        /// <summary>
        /// Method used to get the name of a row of a problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <param name="index">The index of the row</param>
        /// <returns>The name of the row</returns>
        public static string GetRowName(WrapProblem problem, int index) => Marshal.PtrToStringAnsi(CoinGetRowName(problem.getProblem(), index));
        /// <summary>
        /// Method used to optimize a problem
        /// </summary>
        /// <param name="problem">The problem to optimize</param>
        /// <returns>The state of the operation</returns>
        public static int OptimizeProblem(WrapProblem problem)
        {
            return CoinOptimizeProblem(problem.getProblem(), 0);
        }
/*
        public static int OptimizeProblem(WrapProblem problem, int method)
        {
            return CoinOptimizeProblem(problem.getProblem(), method);
        }
   */
        /// <summary>
        /// Method used to check the state of a solution
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>
        /// 0:	Optimal solution found
        /// 1:	Problem primal infeasible
        /// 2:	Problem dual infeasible
        /// 3:	Stopped on iterations
        /// 4:  Stopped due to errors
        /// 5:  Stopped by user</returns>
        public static int GetSolutionStatus(WrapProblem problem) => CoinGetSolutionStatus(problem.getProblem());
        /// <summary>
        /// Method used to check the state of a solution
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The state of the problem</returns>
        public static string GetSolutionStatusText(WrapProblem problem) => Marshal.PtrToStringAnsi(CoinGetSolutionText(problem.getProblem()));
        /// <summary>
        /// Method that returns the solution objective value
        /// </summary>
        /// <param name="problem">Tge problem to check</param>
        /// <returns>The objective value</returns>
        public static double GetObjectValue(WrapProblem problem) => CoinGetObjectValue(problem.getProblem());
        /// <summary>
        /// Method used to get the currently best known bound on the optimal solution value of a MIP problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The best MIP bound</returns>
        public static double GetMipBestBound(WrapProblem problem) => CoinGetMipBestBound(problem.getProblem());
        /// <summary>
        /// Method used to get the total number of simplex iterations to solve an LP problem
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The number of iterations</returns>
        public static int GetIterCount(WrapProblem problem) => CoinGetIterCount(problem.getProblem());

        /// <summary>
        /// Method used to access the number of nodes used to solve a mixed integer problem.
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <returns>The node count</returns>
        public static int GetMipNodeCount(WrapProblem problem) => CoinGetMipNodeCount(problem.getProblem());

        /// <summary>
        /// Method used to get the solution values. 
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <param name="activity">An array where the values of the primal variables for the problem will be stored.
        /// The length of the array must be at least as large as the number of columns in the problem object.</param>
        /// <param name="reducedCost">An array where the values of the dual variables for each of the constraints will be stored. 
        /// The length of the array must be at least as large as the number of rows in the problem object.</param>
        /// <param name="slackValues">An array where  the values of the slack or surplus variables for each of the constraints will be stored. 
        /// The length of the array must be at least as large as the number of rows in the problem object.</param>
        /// <param name="shadowPrice">An array where the values of the reduced costs for each of the variables will be stored. 
        /// The length of the array must be at least as large as the number of columns in the problem object</param>
        /// <returns>The state of the operation. </returns>
        public static int GetSolutionValues(WrapProblem problem, [In, Out] double[] activity,
                       [In, Out] double[] reducedCost, [In, Out] double[] slackValues, [In, Out] double[] shadowPrice) =>
            CoinGetSolutionValues(problem.getProblem(), activity, reducedCost, slackValues, shadowPrice);

        /// <summary>
        /// Method used to get the solution ranges for obj and rhs values.
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <param name="objLoRange">An array where the values of objective function lower range values are to be returned</param>
        /// <param name="objUpRange">An array where the values of objective function upper range values are to be returned</param>
        /// <param name="rhsLoRange">An array where the values of righthand side lower range values are to be returned</param>
        /// <param name="rhsUpRange">An array where the values of righthand side upper range values are to be returned</param>
        /// <returns>The state of the operation. </returns>
        public static int GetSolutionRanges(WrapProblem problem, [In, Out] double[] objLoRange,
                      [In, Out] double[] objUpRange, [In, Out] double[] rhsLoRange, [In, Out] double[] rhsUpRange) =>
            CoinGetSolutionRanges(problem.getProblem(), objLoRange, objUpRange, rhsLoRange, rhsUpRange);

        /// <summary>
        /// Method used to get the basis resident in an LP problem object
        /// </summary>
        /// <param name="problem">The problem to check</param>
        /// <param name="colStatus">An array to receive the basis status of the columns in the LP problem object.
        /// The length of the array must be no less than the number of columns in the matrix.</param>
        /// <param name="rowStatus">An array to receive the basis status of the artificial/slack/surplus variable associated with each row in the constraint matrix.
        /// The array's length must be no less than the number of rows in the LP problem object</param>
        /// <returns>The state of the operation</returns>
        public static int GetSolutionBasis(WrapProblem problem, [In, Out] int[] colStatus,
                        [In, Out] double[] rowStatus) => CoinGetSolutionBasis(problem.getProblem(), colStatus, rowStatus);
        //************************


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
