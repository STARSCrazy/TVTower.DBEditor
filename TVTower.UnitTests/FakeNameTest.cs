using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVTower.Converter;

namespace TVTower.UnitTests
{
    [TestClass]
    public class FakeNameTest
    {
        [TestMethod]
        public void FakerTest()
        {
            var faker = new NameFaker();
            faker.InitializeData();
            Assert.AreEqual("Shtiphan", faker.Fake("Steven"));
            Assert.AreEqual("Sbylborg", faker.Fake("Spielberg"));

            Assert.AreEqual("Sthanli", faker.Fake("Stanley"));
            Assert.AreEqual("Cuprigg", faker.Fake("Kubrick"));

            Assert.AreEqual("Ronamn", faker.Fake("Roman"));
            Assert.AreEqual("Bollonsghi", faker.Fake("Polanski"));

            Assert.AreEqual("Hanrih", faker.Fake("Henry"));
            Assert.AreEqual("Kattamerunn", faker.Fake("Cutamaran"));

            Assert.AreEqual("Halphrit", faker.Fake("Alfred"));
            Assert.AreEqual("Hidshgogg", faker.Fake("Hitchcock"));


            Assert.AreEqual("Ouwart", faker.Fake("Howard"));
            Assert.AreEqual("Havkz", faker.Fake("Hawks"));

            Assert.AreEqual("Dytmer", faker.Fake("Dietmar"));
            Assert.AreEqual("Hjüpsh", faker.Fake("Hübsch"));

            Assert.AreEqual("Tavit", faker.Fake("David"));
            Assert.AreEqual("Lientsh", faker.Fake("Lynch"));


            Assert.AreEqual("Djohnn", faker.Fake("John"));
            Assert.AreEqual("Hasstjon", faker.Fake("Huston"));

            Assert.AreEqual("Halphrit", faker.Fake("Alfred"));
            Assert.AreEqual("Frohrehr", faker.Fake("Vohrer"));

            Assert.AreEqual("Rittli", faker.Fake("Ridley"));
            Assert.AreEqual("Sgodt", faker.Fake("Scott"));


            Assert.AreEqual("Djohnn", faker.Fake("John"));
            Assert.AreEqual("Garbender", faker.Fake("Carpenter"));

            Assert.AreEqual("Ricaart", faker.Fake("Richard"));
            Assert.AreEqual("Turnnr", faker.Fake("Donner"));

            Assert.AreEqual("Aaralt", faker.Fake("Harald"));
            Assert.AreEqual("Raynl", faker.Fake("Reinl"));


            Assert.AreEqual("Gurt", faker.Fake("Kurt"));
            Assert.AreEqual("Offmahn", faker.Fake("Hoffmann"));

            Assert.AreEqual("Guij", faker.Fake("Guy"));
            Assert.AreEqual("Hamiltjon", faker.Fake("Hamilton"));

            Assert.AreEqual("Kregorih J.", faker.Fake("Gregory J."));
            Assert.AreEqual("Pomen", faker.Fake("Bonann"));


            Assert.AreEqual("Praian", faker.Fake("Brian"));
            Assert.AreEqual("De Balna", faker.Fake("De Palma"));

            Assert.AreEqual("Zertshio", faker.Fake("Sergio"));
            Assert.AreEqual("Liohme", faker.Fake("Leone"));

            Assert.AreEqual("Frunnzis Fortt", faker.Fake("Francis Ford"));
            Assert.AreEqual("Gopbolla", faker.Fake("Coppola"));


            Assert.AreEqual("Gristian Slater", faker.Fake("Christian Slater"));
            //Assert.AreEqual( "Slaterrol", faker.Fake( "Slater" ) );
        }
    }
}
