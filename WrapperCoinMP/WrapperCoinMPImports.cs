using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WrapperCoinMP
{
    public partial class WrapperCoin
    {
        [DllImport("Coinmp.dll")] static extern int CoinInitSolver(string licenseStr);
        [DllImport("Coinmp.dll")] static extern int CoinFreeSolver();

        [DllImport("coinmp.dll")] public static extern int CoinGetSolverNameBuf(StringBuilder solverName, int buflen);
        [DllImport("Coinmp.dll")] public static extern double CoinGetVersion();
        [DllImport("coinmp.dll", EntryPoint = "CoinGetVersionStr")] public static extern IntPtr CoinGetVersionStrIntPtr();
    }
}
