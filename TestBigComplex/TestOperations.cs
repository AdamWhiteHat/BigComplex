using System;
using NUnit.Framework;
using ExtendedNumerics;
using System.Numerics;

namespace TestBigComplex
{
	[TestFixture(Category = "TestOperations")]
	public class TestOperations
	{
		private TestContext m_testContext;
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void TestAddition()
		{
			var expected = "10 + 18 i";

			BigComplex low = BigComplex.Parse("(3, 13)");
			BigComplex high = BigComplex.Parse("(7, 5)");

			var result = high + low;
			var actual = result.ToString();

			string description = $"{low} + {high} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestSubtraction()
		{
			var expected = "4 - 8 i";

			BigComplex low = BigComplex.Parse("(3, 13)");
			BigComplex high = BigComplex.Parse("(7, 5)");

			var result = high - low;
			var actual = result.ToString();

			string description = $"{low} - {high} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestMultiply()
		{
			var expected = "-44 + 106 i";

			BigComplex low = BigComplex.Parse("(3, 13)");
			BigComplex high = BigComplex.Parse("(7, 5)");

			var result = high * low;
			var actual = result.ToString();

			string description = $"{low} * {high} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestDivide()
		{
			var expected = "2";

			BigComplex low = BigComplex.Parse("(3, 2)");
			BigComplex high = BigComplex.Parse("(6, 4)");

			var result = high / low;
			var actual = result.ToString();

			string description = $"{high} / {low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}


		[Test]
		public void TestAbs001()
		{
			var expected = "5";

			BigComplex low = BigComplex.Parse("(4, 3)");

			var result = BigComplex.Abs(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestAbs002()
		{
			var expected = "4";

			BigComplex low = BigComplex.Parse("(4, 0)");

			var result = BigComplex.Abs(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}


		[Test]
		public void TestConjugate()
		{
			var expected = "4 - 3 i";

			BigComplex low = BigComplex.Parse("(4, 3)");

			var result = BigComplex.Conjugate(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}


		[Test]
		public void TestReciprocal()
		{
			var expected = "0 - 1 i";

			BigComplex low = BigComplex.Parse("(0, 1)");

			var result = BigComplex.Reciprocal(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestPow()
		{
			int savePrecision = BigDecimal.Precision;
			BigDecimal.Precision = 20;
			BigDecimal.AlwaysTruncate = true;


			BigComplex toSquare = new BigComplex(0, 1);

			string expected = "-1";
			BigComplex actual = BigComplex.Pow(toSquare, 2).Round();

			Assert.AreEqual(expected, actual.Real.ToString());
			Assert.AreEqual(0, (double)BigDecimal.Round(actual.Imaginary), Math.Pow(10, -14));

			BigDecimal.Precision = savePrecision;
			BigDecimal.AlwaysTruncate = false;
		}

		[Test]
		public void TestLog()
		{
			BigComplex test = new BigComplex(2, 4);

			string expected = "1.49786613677699549669 + 1.10714871779409050301 i";
			BigComplex actual = BigComplex.Log(test).Round();

			Assert.AreEqual(expected, actual.ToString());
		}
	}
}