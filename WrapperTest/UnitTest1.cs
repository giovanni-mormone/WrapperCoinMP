using Xunit;
using WrapperCoinMP;
using System;

namespace WrapperTest
{
    
    public class UnitTest1
    {
		private WrapProblem problem;

		[Fact]
		public void TestAfiroProblem()
		{
			const int NUM_COLS = 32;
			const int NUM_ROWS = 27;
			const int NUM_NZ = 83;
			const int NUM_RNG = 0;
			const double DBL_MAX = 1e+037;

			string probname = "Afiro";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "Cost";
			int objsens = WrapperCoin.SOLV_OBJSENS_MIN;
			double objconst = 0.0;

			double[] dobj = new double[NUM_COLS] {0, -0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -0.32, 0, 0, 0, -0.6,
												  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -0.48, 0, 0, 10};

			double[] dclo = new double[NUM_COLS] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
												  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

			double[] dcup = new double[NUM_COLS] {DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX,
								  DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX,
								  DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX,
								  DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX};

			char[] rtyp = new char[NUM_ROWS] {'E', 'E', 'L', 'L', 'E', 'E', 'L', 'L', 'L', 'L', 'E', 'E', 'L',
										 'L', 'E', 'E', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L'};

			double[] drhs = new double[NUM_ROWS] {0, 0, 80, 0, 0, 0, 80, 0, 0, 0, 0, 0, 500, 0, 0, 44, 500, 0,
												  0, 0, 0, 0, 0, 0, 0, 310, 300};

			int[] mbeg = new int[NUM_COLS + 1] {0, 4, 6, 8, 10, 14, 18, 22, 26, 28, 30, 32, 34, 36, 38, 40,
									  44, 46, 48, 50, 52, 56, 60, 64, 68, 70, 72, 74, 76, 78, 80, 82, 83};

			int[] mcnt = new int[NUM_COLS] {4, 2, 2, 2, 4, 4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 4, 2, 2, 2, 2, 4,
											4, 4, 4, 2, 2, 2, 2, 2, 2, 2, 1};

			int[] midx = new int[NUM_NZ] {0, 1, 2, 23, 0, 3, 0, 21, 1, 25, 4, 5, 6, 24, 4, 5, 7, 24, 4, 5,
							   8, 24, 4, 5, 9, 24, 6, 20, 7, 20, 8, 20, 9, 20, 3, 4, 4, 22, 5, 26, 10, 11,
							   12, 21, 10, 13, 10, 23, 10, 20, 11, 25, 14, 15, 16, 22, 14, 15, 17, 22, 14,
							   15, 18, 22, 14, 15, 19, 22, 16, 20, 17, 20, 18, 20, 19, 20, 13, 15, 15, 24,
							   14, 26, 15};

			double[] mval = new double[NUM_NZ] {-1, -1.06, 1, 0.301, 1, -1, 1, -1, 1, 1, -1, -1.06, 1, 0.301,
							  -1, -1.06, 1, 0.313, -1, -0.96, 1, 0.313, -1, -0.86, 1, 0.326, -1, 2.364, -1,
							  2.386, -1, 2.408, -1, 2.429, 1.4, 1, 1, -1, 1, 1, -1, -0.43, 1, 0.109, 1, -1,
							  1, -1, 1, -1, 1, 1, -0.43, 1, 1, 0.109, -0.43, 1, 1, 0.108, -0.39, 1, 1,
							  0.108, -0.37, 1, 1, 0.107, -1, 2.191, -1, 2.219, -1, 2.249, -1, 2.279, 1.4,
							  -1, 1, -1, 1, 1, 1};

			string[] colNames = new string[NUM_COLS] {"x01", "x02", "x03", "x04", "x06", "x07", "x08", "x09",
								 "x10", "x11", "x12", "x13", "x14", "x15", "x16", "x22", "x23", "x24", "x25",
								 "x26", "x28", "x29", "x30", "x31", "x32", "x33", "x34", "x35", "x36", "x37",
								 "x38", "x39"};

			string[] rowNames = new string[NUM_ROWS] {"r09", "r10", "x05", "x21", "r12", "r13", "x17", "x18",
								 "x19", "x20", "r19", "r20", "x27", "x44", "r22", "r23", "x40", "x41", "x42",
								 "x43", "x45", "x46", "x47", "x48", "x49", "x50", "x51"};

			problem = WrapperCoin.CreateProblem(probname);
			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng, objsens, objconst, dobj, dclo, dcup, rtyp, drhs, null, mbeg, mcnt, midx, mval
				, colNames, rowNames, objectname);
			int x = WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			double optimalValue = -464.7531428571;
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));
		}

		[Fact]
		public void testBakery()
        {
			const int NUM_COLS = 2;
			const int NUM_ROWS = 3;
			const int NUM_NZ = 4;

			string problemName = "Bakery";
			int colCount = NUM_COLS;
			int rowCount = NUM_ROWS;
			int nonZeroCount = NUM_NZ;
			int rangeCount = 0;

			string objectName = "Profit";
			int objectSense = WrapperCoin.SOLV_OBJSENS_MAX;
			double objectConst = -4000.0 / 30.0;
			double[] objectCoeffs = new double[NUM_COLS] { 0.05, 0.08 };

			double[] lowerBounds = new double[NUM_COLS] { 0.0, 0.0 };
			double[] upperBounds = new double[NUM_COLS] { 1000000.0, 1000000.0 };

			char[] rowType = new char[NUM_ROWS] { 'L', 'L', 'L' };
			double[] rhsValues = new double[NUM_ROWS] { 1400, 8000, 5000 };

			int[] matrixBegin = new int[NUM_COLS + 1] { 0, 2, 4 };
			int[] matrixCount = new int[NUM_COLS] { 2, 2 };
			int[] matrixIndex = new int[NUM_NZ] { 0, 1, 0, 2 };
			double[] matrixValues = new double[NUM_NZ] { 0.1, 1.0, 0.2, 1.0 };

			string[] colNames = new string[NUM_COLS] { "Sun", "Moon" };
			string[] rowNames = new string[NUM_ROWS] { "c1", "c2", "c3" };

			double optimalValue = 506.66666667;
			problem = WrapperCoin.CreateProblem(problemName);

			WrapperCoin.LoadProblem(problem, colCount, rowCount,
				nonZeroCount, rangeCount, objectSense, objectConst, objectCoeffs,
				lowerBounds, upperBounds, rowType, rhsValues, null,
				matrixBegin, matrixCount, matrixIndex, matrixValues,
				colNames, rowNames, objectName);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}

		[Fact]
		public void coinTest()
		{
			const int NUM_COLS = 8;
			const int NUM_ROWS = 5;
			const int NUM_NZ = 14;

			string problemName = "CoinTest";

			int colCount = NUM_COLS;
			int rowCount = NUM_ROWS;
			int nonZeroCount = NUM_NZ;
			int rangeCount = 0;

			string objectName = "obj";
			int objectSense = WrapperCoin.SOLV_OBJSENS_MAX;
			double objectConst = 0.0;
			double[] objectCoeffs = new double[NUM_COLS] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 };

			double[] lowerBounds = new double[NUM_COLS] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
			double[] upperBounds = new double[NUM_COLS] { 1000000.0, 1000000.0, 1000000.0, 1000000.0,
														  1000000.0, 1000000.0, 1000000.0, 1000000.0  };

			char[] rowType = new char[NUM_ROWS] { 'L', 'L', 'L', 'L', 'L' };
			double[] rhsValues = new double[NUM_ROWS] { 14.0, 80.0, 50.0, 50.0, 50.0 };

			int[] matrixBegin = new int[NUM_COLS + 1] { 0, 2, 4, 6, 8, 10, 11, 12, 14 };
			int[] matrixCount = new int[NUM_COLS] { 2, 2, 2, 2, 2, 1, 1, 2 };
			int[] matrixIndex = new int[NUM_NZ] { 0, 4, 0, 1, 1, 2, 0, 3, 0, 4, 2, 3, 0, 4 };
			double[] matrixValues = new double[NUM_NZ] {3.0, 5.6, 1.0, 2.0, 1.1, 1.0, -2.0, 2.8,
														-1.0, 1.0, 1.0, -1.2, -1.0, 1.9};

			string[] colNames = new string[NUM_COLS] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8" };
			string[] rowNames = new string[NUM_ROWS] { "r1", "r2", "r3", "r4", "r5" };

			double[] initValues = new double[NUM_COLS] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };

			double optimalValue = 1428729.2857143;

			problem = WrapperCoin.CreateProblem(problemName);

			WrapperCoin.LoadProblem(problem, colCount, rowCount,
				nonZeroCount, rangeCount, objectSense, objectConst, objectCoeffs,
				lowerBounds, upperBounds, rowType, rhsValues, null,
				matrixBegin, matrixCount, matrixIndex, matrixValues,
				colNames, rowNames, objectName);
			WrapperCoin.InitValues(problem, initValues);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}


		[Fact]
		public void coinTestMIP()
		{
			const int NUM_COLS = 8;
			const int NUM_ROWS = 5;
			const int NUM_NZ = 14;
			const int NUM_RNG = 2;
			const double DBL_MAX = 1e37;

			string probname = "Exmip1";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "z";
			int objsens = WrapperCoin.SOLV_OBJSENS_MIN;
			double objconst = 0.0;
			double[] dobj = new double[NUM_COLS] { 1, 0, 0, 0, 2, 0, 0, -1 };

			double[] dclo = new double[NUM_COLS] { 2.5, 0, 0, 0, 0.5, 0, 0, 0 };
			double[] dcup = new double[NUM_COLS] { DBL_MAX, 4.1, DBL_MAX, DBL_MAX, 4, DBL_MAX, DBL_MAX, 4.3 };

			double[] drlo = { 2.5, -DBL_MAX, -DBL_MAX, 1.8, 3.0 };
			double[] drup = { DBL_MAX, 2.1, 4.0, 5.0, 15.0 };

			int[] mbeg = new int[NUM_COLS + 1] { 0, 2, 4, 6, 8, 10, 11, 12, 14 };
			int[] mcnt = new int[NUM_COLS] { 2, 2, 2, 2, 2, 1, 1, 2 };
			int[] midx = new int[NUM_NZ] { 0, 4, 0, 1, 1, 2, 0, 3, 0, 4, 2, 3, 0, 4 };
			double[] mval = new double[NUM_NZ] { 3, 5.6, 1, 2, 1.1, 1, -2, 2.8, -1, 1, 1, -1.2, -1, 1.9 };

			string[] colNames = new string[NUM_COLS] {"col01", "col02", "col03", "col04", "col05", "col06",
													  "col07", "col08"};
			string[] rowNames = new string[NUM_ROWS] { "row01", "row02", "row03", "row04", "row05" };

			char[] ctyp = new char[NUM_COLS] { 'C', 'C', 'B', 'B', 'C', 'C', 'C', 'C' };

			double optimalValue = 3.23684210526;

			problem = WrapperCoin.CreateProblem(probname);

			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng,
				objsens, objconst, dobj, dclo, dcup, drlo, drup, mbeg,
				mcnt, midx, mval, colNames, rowNames, objectname);
			WrapperCoin.LoadInteger(problem, ctyp);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}

		[Fact]
		public void coinTestSos1()
		{
			const int NUM_COLS = 3;
			const int NUM_ROWS = 1;
			const int NUM_NZ = 3;
			const int NUM_RNG = 0;
			const double DBL_MAX = 1e37;
			const int NUM_SOS = 1;
			const int NUM_SOSNZ = 3;

			string probname = "GamsSos1a";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "z";
			int objsens = WrapperCoin.SOLV_OBJSENS_MAX;
			double objconst = 0.0;
			double[] dobj = new double[NUM_COLS] { 0.9, 1.0, 1.1 };

			double[] dclo = new double[NUM_COLS] { 0, 0, 0 };
			double[] dcup = new double[NUM_COLS] { 0.8, 0.6, 0.6 };

			double[] drlo = { -DBL_MAX };
			double[] drup = { 1.0 };

			int[] mbeg = new int[NUM_COLS + 1] { 0, 1, 2, 3 };
			int[] mcnt = new int[NUM_COLS] { 1, 1, 1 };
			int[] midx = new int[NUM_NZ] { 0, 0, 0 };
			double[] mval = new double[NUM_NZ] { 1, 1, 1 };

			string[] colNames = new string[NUM_COLS] { "x1", "x2", "x3" };
			string[] rowNames = new string[NUM_ROWS] { "xsum" };

			int sosCount = 1;
			int sosNZCount = 3;
			int[] sosType = new int[NUM_SOS] { 1 };
			int[] sosBegin = new int[NUM_SOS + 1] { 0, 3 };
			int[] sosIndex = new int[NUM_SOSNZ] { 0, 1, 2 };

			double optimalValue = 0.72;
			problem = WrapperCoin.CreateProblem(probname);

			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng,
				objsens, objconst, dobj, dclo, dcup, drlo, drup, mbeg,
				mcnt, midx, mval, colNames, rowNames, objectname);
			WrapperCoin.LoadSos(problem, sosCount, sosNZCount, sosType, null, sosBegin, sosIndex, null);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}


		[Fact]
		public void coinTestSos2()
		{
			const int NUM_COLS = 7;
			const int NUM_ROWS = 5;
			const int NUM_NZ = 15;
			const int NUM_RNG = 0;
			const double DBL_MAX = 1e37;
			const int NUM_SOS = 1;
			const int NUM_SOSNZ = 3;

			string probname = "GamsSos2a";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "z";
			int objsens = WrapperCoin.SOLV_OBJSENS_MIN;
			double objconst = 0.0;
			double[] dobj = new double[NUM_COLS] { 0, 0, 0, 0, 0, 1, 1 };

			double[] dclo = new double[NUM_COLS] { 0.0, 0, 0, -DBL_MAX, -DBL_MAX, 0, 0 };
			double[] dcup = new double[NUM_COLS] {DBL_MAX, DBL_MAX, DBL_MAX, DBL_MAX,
												  DBL_MAX, DBL_MAX, DBL_MAX};

			char[] rtyp = new char[NUM_ROWS] { 'E', 'E', 'E', 'G', 'G' };
			double[] drhs = new double[NUM_ROWS] { 1, 0, 0, -1.3, 1.3 };

			int[] mbeg = new int[NUM_COLS + 1] { 0, 3, 6, 9, 10, 13, 14, 15 };
			int[] mcnt = new int[NUM_COLS] { 3, 3, 3, 1, 3, 1, 1 };
			int[] midx = new int[NUM_NZ] { 0, 1, 2, 0, 1, 2, 0, 1, 2, 1, 2, 3, 4, 3, 4 };
			double[] mval = new double[NUM_NZ] { 1, 1, 1, 1, 2, 2, 1, 3, 3, -1, -1, -1, 1, 1, 1 };

			string[] colNames = new string[NUM_COLS] { "w1", "w2", "w3", "x", "fx", "fplus", "fminus" };
			string[] rowNames = new string[NUM_ROWS] { "wsum", "xdef", "fxdef", "gapplus", "gapminus" };

			int sosCount = 1;
			int sosNZCount = 3;
			int[] sosType = new int[NUM_SOS] { 2 };
			int[] sosBegin = new int[NUM_SOS + 1] { 0, 3 };
			int[] sosIndex = new int[NUM_SOSNZ] { 0, 1, 2 };

			double optimalValue = 0.0;

			problem = WrapperCoin.CreateProblem(probname);

			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng,
				objsens, objconst, dobj, dclo, dcup, rtyp, drhs, null, mbeg,
				mcnt, midx, mval, colNames, rowNames, objectname);
			WrapperCoin.LoadSos(problem, sosCount, sosNZCount, sosType, null, sosBegin, sosIndex, null);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}

		[Fact]
		public void coinTestP033WithoutSettingTheIntegerConstraints()
		{
			const int NUM_COLS = 33;
			const int NUM_ROWS = 15;
			const int NUM_NZ = 98;
			const int NUM_RNG = 0;

			string probname = "P0033";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "Obj";
			int objsens = WrapperCoin.SOLV_OBJSENS_MIN;
			double objconst = 0.0;

			double[] dobj = new double[NUM_COLS] {171, 171, 171, 171, 163, 162, 163, 69, 69, 183, 183, 183,
								183, 49, 183, 258, 517, 250, 500, 250, 500, 159, 318, 159, 318, 159, 318, 159,
								318, 114, 228, 159, 318};

			double[] dclo = new double[NUM_COLS] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
								0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

			double[] dcup = new double[NUM_COLS] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
								1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};

			char[] rtyp = new char[NUM_ROWS] { 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L' };

			double[] drhs = new double[NUM_ROWS] {1, 1, 1, 1, -5, 2700, -2600, -100, -900, -1656, -335, -1026,
								-5, -500, -270};

			int[] mbeg = new int[NUM_COLS + 1] {0, 3, 6, 10, 14, 19, 24, 26, 31, 36, 38, 41, 45, 49, 53, 54,
								55, 56, 58, 60, 61, 62, 66, 70, 73, 76, 80, 84, 87, 90, 93, 96, 97, 98};

			int[] mcnt = new int[NUM_COLS] {3, 3, 4, 4, 5, 5, 2, 5, 5, 2, 3, 4, 4, 4, 1, 1, 1, 2, 2, 1, 1,
								4, 4, 3, 3, 4, 4, 3, 3, 3, 3, 1, 1};

			int[] midx = new int[NUM_NZ] {0, 8, 9, 0, 12, 13, 0, 5, 6, 9, 0, 5, 6, 7, 1, 5, 6, 10, 11, 1,
								5, 6, 8, 9, 1, 14, 2, 5, 6, 10, 11, 2, 5, 6, 8, 9, 3, 4, 3, 10, 11, 3, 5, 6,
								11, 3, 5, 6, 9, 5, 6, 8, 9, 3, 4, 4, 12, 13, 12, 13, 13, 13, 5, 6, 10, 11, 5,
								6, 10, 11, 5, 6, 11, 5, 6, 11, 5, 6, 8, 9, 5, 6, 8, 9, 5, 6, 9, 5, 6, 9, 5, 6,
								7, 5, 6, 7, 14, 14};

			double[] mval = new double[NUM_NZ] {1, -300, -300, 1, -300, -300, 1, 300, -300, -300, 1, 300,
								-300, -300, 1, 285, -285, -285, -285, 1, 285, -285, -285, -285, 1, -285, 1,
								265, -265, -265, -265, 1, 265, -265, -265, -265, 1, -230, 1, -230, -230, 1,
								230, -230, -230, 1, 230, -230, -230, 190, -190, -190, -190, 1, -200, -400,
								-200, -200, -400, -400, -200, -400, 200, -200, -200, -200, 400, -400, -400,
								-400, 200, -200, -200, 400, -400, -400, 200, -200, -200, -200, 400, -400,
								-400, -400, 200, -200, -200, 400, -400, -400, 200, -200, -200, 400, -400,
								-400, -200, -400};

			string[] colNames = new string[NUM_COLS] {"c157", "c158", "c159", "c160", "c161", "c162", "c163",
								"c164", "c165", "c166", "c167", "c168", "c169", "c170", "c171", "c172",
								"c173", "c174", "c175", "c176", "c177", "c178", "c179", "c180", "c181",
								"c182", "c183", "c184", "c185", "c186", "c187", "c188", "c189"};

			string[] rowNames = new string[NUM_ROWS] {"r114", "r115", "r116", "r117", "r118", "r119", "r120",
								"r121", "r122", "r123", "r124", "r125", "r126", "r127", "r128"};

			char[] ctyp = new char[NUM_COLS] { 'B', 'B', 'B', 'B', 'B', 'B', 'B',
								'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B',
								'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B',
								'B', 'B', 'B', 'B', 'B', 'B' };

			double optimalValue = 3089.0;


			problem = WrapperCoin.CreateProblem(probname);

			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng,
				objsens, objconst, dobj, dclo, dcup, rtyp, drhs, null, mbeg,
				mcnt, midx, mval, colNames, rowNames, objectname);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.False(compareRes(res, optimalValue));

		}

		[Fact]
		public void coinTestP033()
		{
			const int NUM_COLS = 33;
			const int NUM_ROWS = 15;
			const int NUM_NZ = 98;
			const int NUM_RNG = 0;

			string probname = "P0033";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "Obj";
			int objsens = WrapperCoin.SOLV_OBJSENS_MIN;
			double objconst = 0.0;

			double[] dobj = new double[NUM_COLS] {171, 171, 171, 171, 163, 162, 163, 69, 69, 183, 183, 183,
								183, 49, 183, 258, 517, 250, 500, 250, 500, 159, 318, 159, 318, 159, 318, 159,
								318, 114, 228, 159, 318};

			double[] dclo = new double[NUM_COLS] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
								0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

			double[] dcup = new double[NUM_COLS] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
								1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};

			char[] rtyp = new char[NUM_ROWS] { 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L', 'L' };

			double[] drhs = new double[NUM_ROWS] {1, 1, 1, 1, -5, 2700, -2600, -100, -900, -1656, -335, -1026,
								-5, -500, -270};

			int[] mbeg = new int[NUM_COLS + 1] {0, 3, 6, 10, 14, 19, 24, 26, 31, 36, 38, 41, 45, 49, 53, 54,
								55, 56, 58, 60, 61, 62, 66, 70, 73, 76, 80, 84, 87, 90, 93, 96, 97, 98};

			int[] mcnt = new int[NUM_COLS] {3, 3, 4, 4, 5, 5, 2, 5, 5, 2, 3, 4, 4, 4, 1, 1, 1, 2, 2, 1, 1,
								4, 4, 3, 3, 4, 4, 3, 3, 3, 3, 1, 1};

			int[] midx = new int[NUM_NZ] {0, 8, 9, 0, 12, 13, 0, 5, 6, 9, 0, 5, 6, 7, 1, 5, 6, 10, 11, 1,
								5, 6, 8, 9, 1, 14, 2, 5, 6, 10, 11, 2, 5, 6, 8, 9, 3, 4, 3, 10, 11, 3, 5, 6,
								11, 3, 5, 6, 9, 5, 6, 8, 9, 3, 4, 4, 12, 13, 12, 13, 13, 13, 5, 6, 10, 11, 5,
								6, 10, 11, 5, 6, 11, 5, 6, 11, 5, 6, 8, 9, 5, 6, 8, 9, 5, 6, 9, 5, 6, 9, 5, 6,
								7, 5, 6, 7, 14, 14};

			double[] mval = new double[NUM_NZ] {1, -300, -300, 1, -300, -300, 1, 300, -300, -300, 1, 300,
								-300, -300, 1, 285, -285, -285, -285, 1, 285, -285, -285, -285, 1, -285, 1,
								265, -265, -265, -265, 1, 265, -265, -265, -265, 1, -230, 1, -230, -230, 1,
								230, -230, -230, 1, 230, -230, -230, 190, -190, -190, -190, 1, -200, -400,
								-200, -200, -400, -400, -200, -400, 200, -200, -200, -200, 400, -400, -400,
								-400, 200, -200, -200, 400, -400, -400, 200, -200, -200, -200, 400, -400,
								-400, -400, 200, -200, -200, 400, -400, -400, 200, -200, -200, 400, -400,
								-400, -200, -400};

			string[] colNames = new string[NUM_COLS] {"c157", "c158", "c159", "c160", "c161", "c162", "c163",
								"c164", "c165", "c166", "c167", "c168", "c169", "c170", "c171", "c172",
								"c173", "c174", "c175", "c176", "c177", "c178", "c179", "c180", "c181",
								"c182", "c183", "c184", "c185", "c186", "c187", "c188", "c189"};

			string[] rowNames = new string[NUM_ROWS] {"r114", "r115", "r116", "r117", "r118", "r119", "r120",
								"r121", "r122", "r123", "r124", "r125", "r126", "r127", "r128"};

			char[] ctyp = new char[NUM_COLS] { 'B', 'B', 'B', 'B', 'B', 'B', 'B',
								'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B',
								'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B', 'B',
								'B', 'B', 'B', 'B', 'B', 'B' };

			double optimalValue = 3089.0;


			problem = WrapperCoin.CreateProblem(probname);

			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng,
				objsens, objconst, dobj, dclo, dcup, rtyp, drhs, null, mbeg,
				mcnt, midx, mval, colNames, rowNames, objectname);
			WrapperCoin.LoadInteger(problem, ctyp);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}

		[Fact]
		public void coinTestSemiCont()
		{
			const int NUM_COLS = 4;
			const int NUM_ROWS = 3;
			const int NUM_NZ = 6;
			const int NUM_RNG = 0;
			const double DBL_MAX = 1e37;
			const int NUM_SEMI = 1;

			string probname = "SemiCont";
			int ncol = NUM_COLS;
			int nrow = NUM_ROWS;
			int nels = NUM_NZ;
			int nrng = NUM_RNG;

			string objectname = "z";
			int objsens = WrapperCoin.SOLV_OBJSENS_MIN;
			double objconst = 0.0;
			double[] dobj = new double[NUM_COLS] { 0.0, 1.0, 1.0, 0.0 };

			double[] dclo = new double[NUM_COLS] { 2.8, 0.0, 0.0, 0.0 };
			double[] dcup = new double[NUM_COLS] { 10.0, DBL_MAX, DBL_MAX, DBL_MAX };

			char[] rtyp = new char[NUM_ROWS] { 'L', 'G', 'E' };
			double[] drhs = new double[NUM_ROWS] { 8.9, 8.9, 10.0 };

			int[] mbeg = new int[NUM_COLS + 1] { 0, 1, 2, 3, 6 };
			int[] mcnt = new int[NUM_COLS] { 1, 1, 1, 3 };
			int[] midx = new int[NUM_NZ] { 2, 0, 1, 0, 1, 2 };
			double[] mval = new double[NUM_NZ] { 1, -1, 1, 1, 1, 1 };

			string[] colNames = new string[NUM_COLS] { "s", "pup", "plo", "x" };
			string[] rowNames = new string[NUM_ROWS] { "bigx", "smallx", "f" };

			int semiCount = 1;
			int[] semiIndex = new int[NUM_SEMI] { 0 };

			double optimalValue = 1.1;


			problem = WrapperCoin.CreateProblem(probname);

			WrapperCoin.LoadProblem(problem, ncol, nrow, nels, nrng,
				objsens, objconst, dobj, dclo, dcup, rtyp, drhs, null, mbeg,
				mcnt, midx, mval, colNames, rowNames, objectname);
			WrapperCoin.LoadSemiCont(problem, semiCount,semiIndex);
			WrapperCoin.OptimizeProblem(problem);
			double res = WrapperCoin.GetObjectValue(problem);
			WrapperCoin.UnloadProblem(problem);
			Assert.True(compareRes(res, optimalValue));

		}
		private bool compareRes(double x1, double x2) => x1 - x2 < 0.001 && x1 - x2 > -0.001;
    }
}
