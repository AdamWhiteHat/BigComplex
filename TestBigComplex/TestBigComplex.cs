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


		[Test]
		public void TestSquareRootNegativeTwentyFive1()
		{
			string expectedValue = "0 + 5i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive2()
		{
			string expectedValue = "0 + 5i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex actual = BigComplex.Sqrt(negativeFive);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive3()
		{
			string expectedValue = "0 + 2.2360679774997896964i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-5));

			BigComplex actual = BigComplex.Sqrt(negativeFive);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-5)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive4()
		{
			string expectedValue = "0 + 2.236067977499789696i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-5));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-5)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegativeTwentyFive5()
		{
			string expectedValue = "0 + 5i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-25));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegativeFive2()
		{
			string expectedValue = "0 + 2.2360679774997896964i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-5));

			BigComplex actual = BigComplex.Sqrt(negativeFive);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-5)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}
		
		[Test]
		public void TestSquareRootNegativeTwentyFive6()
		{
			string expectedValue = "0 + 5i";

			BigComplex negativeFive = new BigComplex(new BigDecimal(-25));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-25)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegativeFive6()
		{
			string expectedValue = "0 + 2.236067977499789696i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-5));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-5)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegative9_6()
		{
			string expectedValue = "0 + 3i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-9));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-9)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}


		[Test]
		public void TestSquareRootPositive9_6()
		{
			string expectedValue = "3";

			BigComplex negativeFive = new BigComplex(new BigInteger(9));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(9)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegative81_6()
		{
			string expectedValue = "0 + 9i";

			BigComplex negativeFive = new BigComplex(new BigInteger(-81));

			BigComplex actual = BigComplex.Sqrt(negativeFive, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-81)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

		[Test]
		public void TestSquareRootNegative3218147()
		{
			string expectedValue = "0 + 1793.919451926423590615i";

			BigDecimal d = new BigDecimal(0);

			BigComplex ci = new BigComplex(new BigInteger(-3218147));

			BigComplex actual = BigComplex.Sqrt(ci, Precision);

			TestContext.WriteLine("");
			TestContext.WriteLine("sqrt(-3218147)");
			TestContext.WriteLine("=");
			TestContext.WriteLine($"{expectedValue} (expected)");
			TestContext.WriteLine($"{actual} (actual)");
			TestContext.WriteLine("");

			Assert.AreEqual(expectedValue, actual.ToString());
		}

	}
}
