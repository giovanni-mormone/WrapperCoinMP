using System;
using System.Runtime.InteropServices;
using System.Text;
using WrapperCoinMP;
namespace WrapperCoinMP
{
    public partial class WrapperCoin
    {
        private int resultInit;

        public double GetVersion() => CoinGetVersion();
        public string GetVersionStr() => Marshal.PtrToStringAnsi(CoinGetVersionStrIntPtr());


        public WrapperCoin()
        {
            resultInit = CoinInitSolver("");
            if(resultInit != 0)
            {

            }
        }
        public string GetSolverName()
        {
            StringBuilder builder = new StringBuilder();
            CoinGetSolverNameBuf(builder, builder.Capacity);
            return builder.ToString();
        }
    }
}
