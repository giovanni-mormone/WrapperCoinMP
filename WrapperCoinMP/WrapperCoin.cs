using System;
using System.Runtime.InteropServices;
using WrapperCoinMP;
namespace WrapperCoinMP
{
    public class WrapperCoin
    {
        [DllImport("Coinmp.dll")] static extern int CoinInitSolver(string licenseStr);
        [DllImport("Coinmp.dll")] static extern int CoinFreeSolver();

        [DllImport("Coinmp.dll")] static extern string CoinGetSolverName();

        public int InitSolver() => CoinInitSolver("");

        public string GetSolverName() => CoinGetSolverName();


    }
}
