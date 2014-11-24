using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVTower.Database;
using TVTower.Entities;
using TVTower.Xml;

namespace TVTower.UnitTests
{    
    [TestClass]
    public class XMLV3Test : EntityHelperTest
    {
        [TestMethod]
        public void XMLV3ReadWriteTest()
        {
            TVTDatabase initialDatabase = new TVTDatabase();
            initialDatabase.Initialize();

            var advertising = EntityHelper.GetIconicAdvertising();
            initialDatabase.AddAdvertising(advertising);

            var person = EntityHelper.GetIconicPerson("Wurst", "Hans");
            initialDatabase.AddPerson( person );

            //var programme = EntityHelper.GetIconicProgramme();
            //initialDatabase.AddProgramme( programme );

            var persister = new XmlPersisterV3();
            persister.SaveXML( initialDatabase, "UnitTestXMLV2.xml", DatabaseVersion.V3, DataStructure.FakeData );

            TVTDatabase loadDatabase = new TVTDatabase();
            loadDatabase.Initialize();

            persister.LoadXML( "UnitTestXMLV2.xml", loadDatabase, DataStructure.FakeData );

            Assert.AreEqual( 1, loadDatabase.GetAllAdvertisings().Count() );
            var loadAdvertising = loadDatabase.GetAllAdvertisings().First();
            AssertAdvertisings( advertising, loadAdvertising, TestMode.XMLV3 );

            Assert.AreEqual( 1, loadDatabase.GetAllPeople().Count() );
            var loadPerson = loadDatabase.GetAllPeople().First();
            AssertPeople( person, loadPerson, TestMode.XMLV3, true );

            //Assert.AreEqual( 1, loadDatabase.GetAllProgrammes().Count() );
            //var loadProgrammes = loadDatabase.GetAllProgrammes().First();
            //AssertProgrammes( programme, loadProgrammes, TestMode.XMLV3 );
        }
    }
}
