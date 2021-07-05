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
        [DllImport("Coinmp.dll")] static extern int CoinOptimizeProblem(IntPtr hProb, int method);

        [DllImport("Coinmp.dll")]
        static extern int CoinGetSolutionValues(IntPtr hProb, [In, Out] double[] activity,
                       [In, Out] double[] reducedCost, [In, Out] double[] slackValues, [In, Out] double[] shadowPrice);
        [DllImport("coinmp.dll")] static extern double CoinGetObjectValue(IntPtr hProb);
        [DllImport("coinmp.dll")] static extern double CoinGetMipBestBound(IntPtr hProb);
        [DllImport("coinmp.dll")] static extern int CoinGetIterCount(IntPtr hProb);


    }
}
