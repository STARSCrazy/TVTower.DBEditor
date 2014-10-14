using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeKnight.Core.Test
{
	[TestClass]
	public class UniqueIdGeneratorTest
	{
		[TestMethod]
		public void GetInstanceTest()
		{
			var instance = UniqueIdGenerator.GetInstance();
			Assert.AreSame( instance, UniqueIdGenerator.GetInstance() );
		}

		[TestMethod]
		public void GetNextTest()
		{
			var b1 = new byte[16];
			for ( int i = 0; i < 16; i++ ) b1[i] = 0;
			UniqueIdGenerator.GetInstance().GetNext( b1 );
			Assert.IsTrue( Array.Exists( b1, b => b != 0 ) ); // This could be false every billion years or so
		}

		[TestMethod]
		public void GetBase32UniqueIdTest()
		{
			var b1 = new byte[16];
			for ( int i = 0; i < 16; i++ ) b1[i] = 0;
			string id = UniqueIdGenerator.GetInstance().GetBase32UniqueId( b1, 26 );
			Assert.AreEqual( 26, id.Length );
			Assert.AreEqual( new string( '2', 26 ), id );

			b1 = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
			id = UniqueIdGenerator.GetInstance().GetBase32UniqueId( b1, 26 );
			System.Diagnostics.Trace.WriteLine( id );
			Assert.AreEqual( 26, id.Length );
			Assert.AreEqual( "ZZZZZZ", id.Substring( 20, 6 ) );

			id = UniqueIdGenerator.GetInstance().GetBase32UniqueId( b1, 6 );
			System.Diagnostics.Trace.WriteLine( id );
			Assert.AreEqual( 6, id.Length );
			Assert.AreEqual( "ZZZZZZ", id );

			id = UniqueIdGenerator.GetInstance().GetBase32UniqueId( 18 );
			System.Diagnostics.Trace.WriteLine( id );
			Assert.AreEqual( 18, id.Length );

			var id2 = UniqueIdGenerator.GetInstance().GetBase32UniqueId( 18 );
			System.Diagnostics.Trace.WriteLine( id2 );
			Assert.AreEqual( 18, id2.Length );

			Assert.AreNotEqual( id, id2 );
		}

		[TestMethod]
		public void GetBase32UniqueIdDupeTest()
		{
			var alreadySeen = new Dictionary<string, string>( 1000000 );
			System.Diagnostics.Trace.WriteLine( "Allocated" );
			for ( int i = 0; i < 1000000; i++ )
			{
				var id = UniqueIdGenerator.GetInstance().GetBase32UniqueId( 12 );
				Assert.IsTrue( !alreadySeen.ContainsKey( id ) );
				alreadySeen.Add( id, id );
			}
		}
	}
}
