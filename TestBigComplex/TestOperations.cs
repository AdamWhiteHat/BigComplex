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
			var expected = "10 + 18i";

			BigComplex low = BigComplex.Parse("3 + 13i");
			BigComplex high = BigComplex.Parse("7 + 5i");

			var result = high + low;
			var actual = result.ToString();

			string description = $"{low} + {high} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestSubtraction()
		{
			var expected = "4 - 8i";

			BigComplex low = BigComplex.Parse("3 + 13i");
			BigComplex high = BigComplex.Parse("7 + 5i");

			var result = high - low;
			var actual = result.ToString();

			string description = $"{low} - {high} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}

		[Test]
		public void TestMultiply()
		{
			var expected = "-44 + 106i";

			BigComplex low = BigComplex.Parse("3 + 13i");
			BigComplex high = BigComplex.Parse("7 + 5i");

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

			BigComplex low = BigComplex.Parse("3 + 2i");
			BigComplex high = BigComplex.Parse("6 + 4i");

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

			BigComplex low = BigComplex.Parse("4 + 3i");

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

			BigComplex low = BigComplex.Parse("4 + 0i");

			var result = BigComplex.Abs(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}


		[Test]
		public void TestConjugate()
		{
			var expected = "4 - 3i";

			BigComplex low = BigComplex.Parse("4 + 3i");

			var result = BigComplex.Conjugate(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}


		[Test]
		public void TestReciprocal()
		{
			var expected = "0 - 1i";

			BigComplex low = BigComplex.Parse("0 + 1i");

			var result = BigComplex.Reciprocal(low);
			var actual = result.ToString();

			string description = $"{low} => {expected}";
			TestContext.WriteLine(description);
			Assert.AreEqual(expected, actual, description);
		}
	}
}