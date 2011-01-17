using System.Collections.Generic;
using System.Linq;
using HackHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackTest
{
	[TestClass]
	public class HackFixture
	{
		[TestMethod]
		public void GoRemovesAnyEntryNotMatchingPartial()
		{
			var hc = new HackerController { DataSet = "data dana dada darr 1234", EntryName = "darr", EntryCount = "2" };
			Assert.IsTrue( hc.IsReady );

			hc.Go();

			Assert.AreEqual( "data\ndana\ndada", hc.DataSet );
		}

		[TestMethod]
		public void GoRemovesAnyEntryNotMatchingCount()
		{
			var hc = new HackerController { DataSet = "data dana dada darr 1234", EntryName = "darr", EntryCount = "0" };
			Assert.IsTrue( hc.IsReady );

			hc.Go();

			Assert.AreEqual( "1234", hc.DataSet );
		}

		[TestMethod]
		public void OnlyValidIfNameIsInDataSet()
		{
			var hc = new HackerController { DataSet = "data more 1234", EntryName = "0000", EntryCount = "0" };
			Assert.IsFalse( hc.IsReady );
			hc.EntryName = "more";
			Assert.IsTrue( hc.IsReady );
		}

		[TestMethod]
		public void ControllerWithProperDataIsReady()
		{
			var hc = new HackerController { DataSet = "data\nmore\nname", EntryName = "name", EntryCount = "3" };
			Assert.IsTrue( hc.IsReady );
		}

		[TestMethod]
		public void UntouchedControllerIsntReady()
		{
			var hc = new HackerController();
			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerIsntReadyWithMissingData()
		{
			var hc = new HackerController { DataSet = "", EntryName = "name", EntryCount = "3" };
			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerIsntReadyWithNameEmpty()
		{
			var hc = new HackerController { EntryName = "" };
			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerIsntReadyWithNameNull()
		{
			var hc = new HackerController { EntryName = null };
			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestsForAllStringsEqual()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = "2",
				DataSet = "aone ttes\n rrrr test \r\nothr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsTrue( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestsForOneStringsUnequal()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = "2",
				DataSet = "aone ttes\n rrrr test \r\notrhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestsForAllStringsUnequal()
		{
			var hc = new HackerController
			{
				EntryName = "tst",
				EntryCount = "2",
				DataSet = "aone ttes\n rrrr \r\nothr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestForNullEntryCountNumber()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = null,
				DataSet = "aone ttes\n rrrr test \r\norhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestForEmptyEntryCountNumber()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = "",
				DataSet = "aone ttes\n rrrr test \r\nothr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestForValidEntryCountNumber()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = "anything",
				DataSet = "aone ttes\n rrrr test \r\norhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestForNegativeEntryCountNumber()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = "-2",
				DataSet = "aone ttes\n rrrr test \r\ntrhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestForOverflowEntryCountNumber()
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = "5",
				DataSet = "aone ttes\n rrrr test \r\ntrhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestForAcceptedEntryCountNumber()
		{
			Assert.IsTrue( TestNumber( "0" ) );
			Assert.IsTrue( TestNumber( "1" ) );
			Assert.IsTrue( TestNumber( "2" ) );
			Assert.IsTrue( TestNumber( "3" ) );
			Assert.IsTrue( TestNumber( "4" ) );
		}


		static bool TestNumber( string data )
		{
			var hc = new HackerController
			{
				EntryName = "test",
				EntryCount = data,
				DataSet = "aone ttes\n rrrr test \r\ntrhr\ndata\ttabs\t  \nhhhh"
			};
			return hc.IsReady;
		}


		[TestMethod]
		public void ControllerTestsForNullEntry()
		{
			var hc = new HackerController
			{
				EntryName = null,
				EntryCount = "1",
				DataSet = "aone ttes\n rrrr test \r\ntrhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestsForEmptyEntry()
		{
			var hc = new HackerController
			{
				EntryName = "",
				EntryCount = "1",
				DataSet = "aone ttes\n rrrr test \r\ntrhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsFalse( hc.IsReady );
		}

		[TestMethod]
		public void ControllerTestsForOKEntry()
		{
			var hc = new HackerController
			{
				EntryName = "tabs",
				EntryCount = "1",
				DataSet = "aone ttes\n rrrr test \r\ntrhr\ndata\ttabs\t  \nhhhh"
			};

			Assert.IsTrue( hc.IsReady );
		}

		[TestMethod]
		public void TestSetDataFiresEvents()
		{
			var hc = new HackerController();

			var list = new List<string>();

			hc.PropertyChanged += ( o, e ) => list.Add( e.PropertyName );

			hc.DataSet = "set";
			hc.EntryCount = "count";
			hc.EntryName = "name";

			Assert.AreEqual( 9, list.Count );

			Assert.AreEqual( 3, list.Count( s => s.Equals( "IsReady" ) ) );
			Assert.AreEqual( 3, list.Count( s => s.Equals( "ButtonMessage" ) ) );
			Assert.AreEqual( 1, list.Count( s => s.Equals( "DataSet" ) ) );
			Assert.AreEqual( 1, list.Count( s => s.Equals( "EntryCount" ) ) );
			Assert.AreEqual( 1, list.Count( s => s.Equals( "EntryName" ) ) );
		}
	}
}