using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVTower.Faker;

namespace TVTower.UnitTests
{
	[TestClass]
	public class FakeNameTest
	{		
        [TestMethod]
        public void ConnectTest()
        {
			var faker = new FakeName();
			faker.InitializeData();
			Assert.AreEqual( "Shtiphan", faker.Fake( "Steven" ) );
			Assert.AreEqual( "Sbylborg", faker.Fake( "Spielberg" ) );

			Assert.AreEqual( "Sthanli", faker.Fake( "Stanley" ) );
			Assert.AreEqual( "Cuprigg", faker.Fake( "Kubrick" ) );

			Assert.AreEqual( "Ronann", faker.Fake( "Roman" ) );
			Assert.AreEqual( "Bholansghi", faker.Fake( "Polanski" ) );

			Assert.AreEqual( "Hanrih", faker.Fake( "Henry" ) );
			Assert.AreEqual( "Kattamharunn", faker.Fake( "Cutamaran" ) );

			Assert.AreEqual( "Halphrit", faker.Fake( "Alfred" ) );
			Assert.AreEqual( "Hidshgogg", faker.Fake( "Hitchcock" ) );


			Assert.AreEqual( "Ouwart", faker.Fake( "Howard" ) );
			Assert.AreEqual( "Havkz", faker.Fake( "Hawks" ) );

			Assert.AreEqual( "Dytmer", faker.Fake( "Dietmar" ) );
			Assert.AreEqual( "Hjüpsh", faker.Fake( "Hübsch" ) );

			Assert.AreEqual( "Javith", faker.Fake( "David" ) );
			Assert.AreEqual( "Liehntsh", faker.Fake( "Lynch" ) );


			Assert.AreEqual( "Djohnn", faker.Fake( "John" ) );
			Assert.AreEqual( "Hassdon", faker.Fake( "Huston" ) );

			Assert.AreEqual( "Halphrit", faker.Fake( "Alfred" ) );
			Assert.AreEqual( "Frohrehr", faker.Fake( "Vohrer" ) );

			Assert.AreEqual( "Rittli", faker.Fake( "Ridley" ) );
			Assert.AreEqual( "Sgodt", faker.Fake( "Scott" ) );


			Assert.AreEqual( "Djohnn", faker.Fake( "John" ) );
			Assert.AreNotEqual( "Carpenter", faker.Fake( "Carpenter" ) );

			Assert.AreNotEqual( "Richard", faker.Fake( "Richard" ) );
			Assert.AreNotEqual( "Donner", faker.Fake( "Donner" ) );

			Assert.AreNotEqual( "Harald", faker.Fake( "Harald" ) );
			Assert.AreNotEqual( "Reinl", faker.Fake( "Reinl" ) );	
        }
	}
}
