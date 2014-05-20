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
			Assert.AreEqual( "Bollansghi", faker.Fake( "Polanski" ) );

			Assert.AreEqual( "Hanrih", faker.Fake( "Henry" ) );
			Assert.AreEqual( "Kattamharunn", faker.Fake( "Cutamaran" ) );

			Assert.AreEqual( "Halphrit", faker.Fake( "Alfred" ) );
			Assert.AreEqual( "Hidshgogg", faker.Fake( "Hitchcock" ) );


			Assert.AreEqual( "Ouwart", faker.Fake( "Howard" ) );
			Assert.AreEqual( "Havkz", faker.Fake( "Hawks" ) );

			Assert.AreEqual( "Dytmer", faker.Fake( "Dietmar" ) );
			Assert.AreEqual( "Hjüpsh", faker.Fake( "Hübsch" ) );

			Assert.AreEqual( "Tavit", faker.Fake( "David" ) );
			Assert.AreEqual( "Lientsh", faker.Fake( "Lynch" ) );


			Assert.AreEqual( "Djohnn", faker.Fake( "John" ) );
			Assert.AreEqual( "Hassdon", faker.Fake( "Huston" ) );

			Assert.AreEqual( "Halphrit", faker.Fake( "Alfred" ) );
			Assert.AreEqual( "Frohrehr", faker.Fake( "Vohrer" ) );

			Assert.AreEqual( "Rittli", faker.Fake( "Ridley" ) );
			Assert.AreEqual( "Sgodt", faker.Fake( "Scott" ) );


			Assert.AreEqual( "Djohnn", faker.Fake( "John" ) );
			Assert.AreEqual( "Garbender", faker.Fake( "Carpenter" ) );

			Assert.AreEqual( "Ricaart", faker.Fake( "Richard" ) );
			Assert.AreEqual( "Tonnr", faker.Fake( "Donner" ) );

			Assert.AreEqual( "Aaralt", faker.Fake( "Harald" ) );
			Assert.AreEqual( "Raynl", faker.Fake( "Reinl" ) );


			Assert.AreEqual( "Gurt", faker.Fake( "Kurt" ) );
			Assert.AreEqual( "Offmahn", faker.Fake( "Hoffmann" ) );

			Assert.AreEqual( "Guij", faker.Fake( "Guy" ) );
			Assert.AreEqual( "Hamildon", faker.Fake( "Hamilton" ) );

			Assert.AreEqual( "Kregorih J.", faker.Fake( "Gregory J." ) );
			Assert.AreEqual( "Poman", faker.Fake( "Bonann" ) );


			Assert.AreEqual( "Praian", faker.Fake( "Brian" ) );
			Assert.AreEqual( "De Balma", faker.Fake( "De Palma" ) );

			Assert.AreEqual( "Zertshio", faker.Fake( "Sergio" ) );
			Assert.AreEqual( "Lioner", faker.Fake( "Leone" ) );

			Assert.AreEqual( "Frunnzis Fortt", faker.Fake( "Francis Ford" ) );
			Assert.AreEqual( "Gopbolla", faker.Fake( "Coppola" ) );

		}
	}
}
