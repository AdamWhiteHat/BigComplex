using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigComplex
{
	[TestFixture(Category = "TestSquareRoot")]
	public class TestBigComplex
	{
		private TestContext m_testContext;
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }

		private static int Precision = 18;
		/*
		[Test]
		public void TestSquareRootNegativeTwentyFive1()
		{
			string expectedValue = "0 +5 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex sqrtNegativeFive1 = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive1.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive1.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive2()
		{
			string expectedValue = "0 +5 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex sqrtNegativeFive2 = BigComplex.Sqrt2(negativeFive);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive2.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive2.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive3()
		{
			string expectedValue = "0 +5 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex sqrtNegativeFive3 = BigComplex.Sqrt3(negativeFive);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive3.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive3.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive4()
		{
			string expectedValue = "0 +5 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex sqrtNegativeFive4 = BigComplex.Sqrt4(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive4.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive4.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive5()
		{
			string expectedValue = "0 +5 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex sqrtNegativeFive5 = BigComplex.Sqrt5(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive5.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive5.ToString());
		}

		[Test]
		public void TestSquareRootNegativeFive2()
		{
			string expectedValue = "0 +2.236067977499789696 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-5));

			BigComplex sqrtNegativeFive2 = BigComplex.Sqrt2(negativeFive);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-5)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive2.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive2.ToString());
		}
		*/
		[Test]
		public void TestSquareRootNegativeTwentyFive6()
		{
			string expectedValue = "0 +5 i";

			BigComplex negativeFive = new BigComplex(new BigDecimal(-25));

			BigComplex sqrtNegativeFive6 = BigComplex.Sqrt6(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive6.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive6.ToString());
		}

		[Test]
		public void TestSquareRootNegativeFive6()
		{
			string expectedValue = "0 +2.236067977499789696 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-5));

			BigComplex sqrtNegativeFive6 = BigComplex.Sqrt6(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-5)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive6.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive6.ToString());
		}

		[Test]
		public void TestSquareRootNegative9_6()
		{
			string expectedValue = "0 +3 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-9));

			BigComplex sqrtNegativeFive6 = BigComplex.Sqrt6(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-9)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive6.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive6.ToString());
		}


		[Test]
		public void TestSquareRootPositive9_6()
		{
			string expectedValue = "3";

			BigComplex negativeFive = new BigComplex(new BigInteger(9));

			BigComplex sqrtNegativeFive6 = BigComplex.Sqrt6(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(9)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive6.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive6.ToString());
		}

		[Test]
		public void TestSquareRootNegative81_6()
		{
			string expectedValue = "0 +9 i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-81));

			BigComplex sqrtNegativeFive6 = BigComplex.Sqrt6(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-81)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtNegativeFive6.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtNegativeFive6.ToString());
		}

		[Test]
		public void TestSquareRootNegative3218147()
		{
			string expectedValue = "0 +1793.919451926423590615 i";

			BigDecimal d = new BigDecimal(0);

			BigComplex ci = new BigComplex(new BigInteger(-3218147));

			BigComplex sqrtCi = BigComplex.Sqrt6(ci, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-3218147)");
			TestContext.WriteLine("=");
			TestContext.WriteLine(sqrtCi.ToString());
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, sqrtCi.ToString());
		}

	}
}
