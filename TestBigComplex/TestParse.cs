using System;
using NUnit.Framework;
using ExtendedNumerics;

namespace TestBigComplex
{
	[TestFixture(Category = "TestParse")]
	public class TestParse
	{
		private TestContext m_testContext;
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }

		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void ParseInteger()
		{
			string expected = "3";
			InternalParse(expected);
		}

		[Test]
		public void ParseIntegerComplex()
		{
			string expected = "3 + 4i";
			InternalParse(expected);
		}

		[Test]
		public void ParseReal()
		{
			string expected = "3.14159";
			InternalParse(expected);
		}

		[Test]
		public void ParseRealComplex()
		{
			string expected = "3.14159 + 4.26535i";
			InternalParse(expected);
		}

		[Test]
		public void ParseAlternateForms()
		{
			string expected001 = "3 + 4i";

			string test001 = "3 + 4i";
			string test002 = "(3 + 4i) ";
			string test003 = "(3, 4)";

			BigComplex bc001 = BigComplex.Parse(test001);
			BigComplex bc002 = BigComplex.Parse(test002);
			BigComplex bc003 = BigComplex.Parse(test003);

			string actual001 = bc001.ToString();
			string actual002 = bc002.ToString();
			string actual003 = bc003.ToString();

			Assert.AreEqual(expected001, actual001, $"{test001} => {actual001}");
			Assert.AreEqual(expected001, actual002, $"{test002} => {actual002}");
			Assert.AreEqual(expected001, actual003, $"{test003} => {actual003}");
		}

		internal void InternalParse(string expected)
		{
			BigComplex parsedBigComplex = BigComplex.Parse(expected);
			string actual = parsedBigComplex.ToString();

			string debugString = $"Parse({expected}) => {actual}";
			TestContext.WriteLine(debugString);

			Assert.AreEqual(expected, actual, debugString);
		}
	}
}