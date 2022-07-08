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
		public void Parse()
		{
			string expected = "3 + 4i";
			string expected004 = "3";

			string test001 = "(3, 4)";
			string test002 = "3 + 4i";
			string test003 = "(3 + 4i) ";
			string test004 = "3";

			BigComplex bc001 = BigComplex.Parse(test001);
			BigComplex bc002 = BigComplex.Parse(test002);
			BigComplex bc003 = BigComplex.Parse(test003);
			BigComplex bc004 = BigComplex.Parse(test004);

			string actual001 = bc001.ToString();
			string actual002 = bc002.ToString();
			string actual003 = bc003.ToString();
			string actual004 = bc004.ToString();

			Assert.AreEqual(expected, actual001, $"{test001} => {actual001}");
			Assert.AreEqual(expected, actual002, $"{test002} => {actual002}");
			Assert.AreEqual(expected, actual003, $"{test003} => {actual003}");
			Assert.AreEqual(expected004, actual004, $"{test004} => {actual004}");
		}
	}
}