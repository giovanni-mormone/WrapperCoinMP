using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WrapperCoinMP
{
    public static partial class WrapperCoin
    {
        [DllImport("Coinmp.dll")] static extern int CoinInitSolver(string licenseStr);
        [DllImport("Coinmp.dll")] static extern int CoinFreeSolver();

        [DllImport("coinmp.dll")] static extern int CoinGetSolverNameBuf(StringBuilder solverName, int buflen);
        [DllImport("Coinmp.dll")] static extern double CoinGetVersion();
        [DllImport("Coinmp.dll", EntryPoint = "CoinGetVersionStr")] static extern IntPtr CoinGetVersionStrIntPtr();
        [DllImport("Coinmp.dll")] static extern double CoinGetInfinity();
        [DllImport("Coinmp.dll")] static extern IntPtr CoinCreateProblem(string problemName);
        [DllImport("Coinmp.dll")]
        public static extern int CoinLoadProblemBuf(IntPtr hProb, int colCount, int rowCount,
                        int nzCount, int rangeCount, int objectSense, double objectConst, double[] objectCoeffs,
                        double[] lowerBounds, double[] upperBounds, char[] rowType, double[] rhsValues,
                        double[] rangeValues, int[] matrixBegin, int[] matrixCount, int[] matrixIndex,
                        double[] matrixValues, string colNamesBuf, string rowNamesBuf, string objName);

        [DllImport("Coinmp.dll")] static extern int CoinLoadInitValues(IntPtr hProb, double[] initValues);
        [DllImport("Coinmp.dll")] static extern int CoinLoadInteger(IntPtr hProb, char[] columnType);
        [DllImport("Coinmp.dll")] static extern int CoinLoadPriority(IntPtr hProb, int priorCount, int[] priorIndex, int[] priorValues, int[] PriorBranch);

        [DllImport("coinmp.dll")] static extern int CoinLoadSos(IntPtr hProb, int sosCount, int sosNZCount, int[] sosType, 
            int[] sosPrior, int[] sosBegin, int[] sosIndex, double[] sosRef);

        [DllImport("coinmp.dll")] static extern int CoinLoadSemiCont(IntPtr hProb, int semiCount, int[] semiIndex);
        [DllImport("coinmp.dll")] static extern int CoinLoadQuadratic(IntPtr hProb, int[] quadBegin,
                        int[] quadCount, int[] quadIndex, double[] quadValues);
        [DllImport("coinmp.dll")]
        static extern int CoinLoadNonlinear(IntPtr hProb, int nlpTreeCount, int nlpLineCount, int[] nlpBegin, int[] nlpOper, int[] nlpArg1, 
            int[] nlpArg2, int[] nlpIndex1, int[] nlpIndex2, double[] nlpValue1, double[] nlpValue2);
        [DllImport("coinmp.dll")] static extern int CoinUnloadProblem(IntPtr hProb);

        [DllImport("coinmp.dll")] static extern int CoinCheckProblem(IntPtr hProb);

        [DllImport("coinmp.dll")] static extern int CoinGetProblemNameBuf(IntPtr hProb, StringBuilder problemName, int buflen);

        [DllImport("coinmp.dll")] static extern int CoinGetColCount(IntPtr hProb);
        [DllImport("coinmp.dll")] static extern int CoinGetRowCount(IntPtr hProb);

        [DllImport("coinmp.dll")] static extern IntPtr CoinGetColName(IntPtr hProb, int col);

        [DllImport("coinmp.dll")] static extern IntPtr CoinGetRowName(IntPtr hProb, int row);



        [DllImport("Coinmp.dll")] static extern int CoinOptimizeProblem(IntPtr hProb, int method);

        [DllImport("Coinmp.dll")]
        static extern int CoinGetSolutionValues(IntPtr hProb, [In, Out] double[] activity,
                       [In, Out] double[] reducedCost, [In, Out] double[] slackValues, [In, Out] double[] shadowPrice);
        [DllImport("coinmp.dll")] static extern double CoinGetObjectValue(IntPtr hProb);
        [DllImport("coinmp.dll")] static extern double CoinGetMipBestBound(IntPtr hProb);
        [DllImport("coinmp.dll")] static extern int CoinGetIterCount(IntPtr hProb);


    }
}
