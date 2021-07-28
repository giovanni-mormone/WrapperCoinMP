using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WrapperCoinMP
{
    public static partial class WrapperCoin
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string filename);

        [DllImport("CoinMP")] static extern int CoinInitSolver(string licenseStr);
        [DllImport("CoinMP")] static extern int CoinFreeSolver();

        [DllImport("CoinMP")] static extern int CoinGetSolverNameBuf(StringBuilder solverName, int buflen);
        [DllImport("CoinMP")] static extern double CoinGetVersion();
        [DllImport("CoinMP", EntryPoint = "CoinGetVersionStr")] static extern IntPtr CoinGetVersionStrIntPtr();
        [DllImport("CoinMP")] static extern double CoinGetInfinity();
        [DllImport("CoinMP")] static extern IntPtr CoinCreateProblem(string problemName);
        [DllImport("CoinMP")]
        static extern int CoinLoadProblem(IntPtr hProb, int colCount, int rowCount,
                        int nzCount, int rangeCount, int objectSense, double objectConst, double[] objectCoeffs,
                        double[] lowerBounds, double[] upperBounds, char[] rowType, double[] rhsValues,
                        double[] rangeValues, int[] matrixBegin, int[] matrixCount, int[] matrixIndex,
                        double[] matrixValues, string[] colNamesBuf, string[] rowNamesBuf, string objName);

        [DllImport("CoinMP")] static extern int CoinLoadInitValues(IntPtr hProb, double[] initValues);
        [DllImport("CoinMP")] static extern int CoinLoadInteger(IntPtr hProb, char[] columnType);
        [DllImport("CoinMP")] static extern int CoinLoadPriority(IntPtr hProb, int priorCount, int[] priorIndex, int[] priorValues, int[] PriorBranch);

        [DllImport("CoinMP")] static extern int CoinLoadSos(IntPtr hProb, int sosCount, int sosNZCount, int[] sosType, 
            int[] sosPrior, int[] sosBegin, int[] sosIndex, double[] sosRef);

        [DllImport("CoinMP")] static extern int CoinLoadSemiCont(IntPtr hProb, int semiCount, int[] semiIndex);
        [DllImport("CoinMP")] static extern int CoinLoadQuadratic(IntPtr hProb, int[] quadBegin,
                        int[] quadCount, int[] quadIndex, double[] quadValues);
        [DllImport("CoinMP")]
        static extern int CoinLoadNonlinear(IntPtr hProb, int nlpTreeCount, int nlpLineCount, int[] nlpBegin, int[] nlpOper, int[] nlpArg1, 
            int[] nlpArg2, int[] nlpIndex1, int[] nlpIndex2, double[] nlpValue1, double[] nlpValue2);
        [DllImport("CoinMP")] static extern int CoinUnloadProblem(IntPtr hProb);

        [DllImport("CoinMP")] static extern int CoinCheckProblem(IntPtr hProb);

        [DllImport("CoinMP")] static extern int CoinGetProblemNameBuf(IntPtr hProb, StringBuilder problemName, int buflen);

        [DllImport("CoinMP")] static extern int CoinGetColCount(IntPtr hProb);
        [DllImport("CoinMP")] static extern int CoinGetRowCount(IntPtr hProb);

        [DllImport("CoinMP")] static extern IntPtr CoinGetColName(IntPtr hProb, int col);

        [DllImport("CoinMP")] static extern IntPtr CoinGetRowName(IntPtr hProb, int row);
        [DllImport("CoinMP")] static extern int CoinOptimizeProblem(IntPtr hProb, int method);

        [DllImport("CoinMP")] static extern int CoinGetSolutionStatus(IntPtr hProb);
        [DllImport("CoinMP")] static extern IntPtr CoinGetSolutionText(IntPtr hProb);
        [DllImport("CoinMP")] static extern double CoinGetObjectValue(IntPtr hProb);
        [DllImport("CoinMP")] static extern double CoinGetMipBestBound(IntPtr hProb);
        [DllImport("CoinMP")] static extern int CoinGetIterCount(IntPtr hProb);
        [DllImport("CoinMP")] static extern int CoinGetMipNodeCount(IntPtr hProb);

        [DllImport("CoinMP")]
        static extern int CoinGetSolutionValues(IntPtr hProb, [In, Out] double[] activity,
                       [In, Out] double[] reducedCost, [In, Out] double[] slackValues, [In, Out] double[] shadowPrice);
        [DllImport("CoinMP")]
        static extern int CoinGetSolutionRanges(IntPtr hProb, [In, Out] double[] objLoRange,
                      [In, Out] double[] objUpRange, [In, Out] double[] rhsLoRange, [In, Out] double[] rhsUpRange);
        [DllImport("CoinMP")]
        static extern int CoinGetSolutionBasis(IntPtr hProb, [In, Out] int[] colStatus,
                        [In, Out] double[] rowStatus);


    }
}
