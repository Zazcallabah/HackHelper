using HackHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackTest
{
	[TestClass]
	public class StringExtFixture
	{
		[TestMethod]
		public void CompareStringMatches()
		{
			Assert.IsTrue( "aoeu".Matches( "2euu", 1 ) );
			Assert.IsTrue( "aoeu".Matches( "aoee", 3 ) );
			Assert.IsTrue( "aoeu".Matches( "asdf", 1 ) );
			Assert.IsTrue( "aoeu".Matches( "0000", 0 ) );
			Assert.IsTrue( "aoeu".Matches( "aoeu", 4 ) );
		}

		[TestMethod]
		public void CompareStringNoMatchForFaultyData()
		{
			Assert.IsFalse( "aoeu".Matches( "aoee2", 3 ) );
			Assert.IsFalse( "aoeu".Matches( null, 1 ) );
			Assert.IsFalse( "aoeu".Matches( "", 0 ) );
			Assert.IsFalse( "".Matches( "aoeu", 4 ) );
			string s = null;
			Assert.IsFalse( s.Matches( "aoee", 3 ) );
		}

		[TestMethod]
		public void CompareStringDoesntMatch()
		{
			Assert.IsFalse( "aoeu".Matches( "aoe3", 2 ) );
			Assert.IsFalse( "aoeu".Matches( "aoe3", 1 ) );
			Assert.IsFalse( "aoeu".Matches( "aoe3", 0 ) );
			Assert.IsFalse( "aoeu".Matches( "aoe3", -1 ) );
			Assert.IsFalse( "aoeu".Matches( "aoe3", 4 ) );
			Assert.IsFalse( "aoeu".Matches( "aoe3", 5 ) );

			Assert.IsFalse( "aoeu".Matches( "2euu", 3 ) );
			Assert.IsFalse( "aoeu".Matches( "2euu", 2 ) );
			Assert.IsFalse( "aoeu".Matches( "2euu", 0 ) );
		}
	}
}