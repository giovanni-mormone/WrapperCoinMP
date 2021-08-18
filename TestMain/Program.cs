using System;
using WrapperCoinMP;
namespace TestMain
{
    class Program
    {
        static int Main(string[] args)
        {
            
            try
            {
				//se la dll CoinMP.dll non è nella cartella di debug del programma, passare una stringa con il percorso completo della dll
				//e.g. C://percorso//della/dll/CoinMP.dll
				//@"/mnt/c/Users/giann/Desktop/coin2/Test/CoinMP/lib/libCoinMP.so"
				//
				string filepath = @"c:\git\WrapperCoinMP\WrapperCoinMP\bin\CoinMP.dll";
				WrapperCoin.InitSolver();
				}
			catch(Exception e1)
            {
				Console.WriteLine(e1);
                return -1;
            }
			//s1(c0),s2(c1),21(c2),1d(c3),2d(c4)
			//s1<3,s2<5,1d<6,2d<4,21<1
			//max 2d+1d
			//c3-c0+c2=0
			//c4-c1=0
			const int NUM_COLS = 22;
			const int NUM_ROWS = 0;
			const int NUM_NZ = 0;
			const int NUM_RNG = 0;
			string probname = "MaxFlow";
			string objectname = "Cost";
			int objsens = WrapperCoin.SOLV_OBJSENS_MAX;
			double objconst = 0.0;
			double infinite = WrapperCoin.GetInfinity();
			double[] objectCoeffs = new double[NUM_COLS] { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0};
			double[] lowerBounds = new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			double[] upperBounds = new double[NUM_COLS] { infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite, infinite };
			char[] rowType = new char[NUM_ROWS] {};
			double[] drhs = new double[NUM_ROWS] { };
			int[] mbeg = new int[NUM_COLS + 1] { 0, 0, 0 , 0, 0, 0 , 0, 0, 0 , 0, 0, 0 , 0, 0, 0 , 0, 0, 0 , 0, 0, 0 , 0, 0};
			int[] mcnt = new int[NUM_COLS] { 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 , 0, 0 };
			int[] midx = new int[NUM_NZ] { };
			double[] mval = new double[NUM_NZ] { };
			WrapProblem problem = WrapperCoin.CreateProblem(probname);
			WrapperCoin.LoadProblem(problem, 22, 0, 0, 0, objsens, objconst, objectCoeffs, lowerBounds, upperBounds, rowType, drhs, null, mbeg, mcnt, midx, mval
				, null, null, objectname);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			double[] activity = new double[NUM_COLS];
			double[] reducedCost = new double[NUM_COLS];
			double[] slackValues = new double[16];
			double[] shadowPrice = new double[16];
			WrapperCoin.GetSolutionValues(problem, activity, reducedCost, slackValues, shadowPrice);
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 1, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 1, 1, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 1, 0, 1, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 1, 0, 1, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, -1, -1, 0, 0, 0, 0, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, -1, -1, 0, 0, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, -1, -1, 0, 0 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, -1, -1 }, 0, 'E', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 8, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 }, 8, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, 4, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 }, 4, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 }, 4, 'L', "");
			WrapperCoin.AddRow(ref problem, new double[NUM_COLS] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }, 4, 'L', "");
			WrapperCoin.OptimizeProblem(problem);
			res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.GetSolutionValues(problem, activity, reducedCost, slackValues, shadowPrice);
			printResult(res, activity);
			//il secondo parametro passato è il nome del file mps che verrà salvato, si può creare un path come fatto nell'init solver
			WrapperCoin.WriteMPSFile(problem, "ProblemaCaricato");
			return 0;
        }

		static void printResult(double result, double[] activity)
        {
			Console.WriteLine($"The result is: {result}");
			string ac = string.Join(",",activity);
			Console.WriteLine($"The activity is: {ac}");
        }
    }
}
