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

            var persister = new XmlPersisterV3();
            persister.SaveXML( initialDatabase, "UnitTestXMLV2.xml", DatabaseVersion.V3, DataStructure.FakeData );

            TVTDatabase loadDatabase = new TVTDatabase();
            loadDatabase.Initialize();

            persister.LoadXML( "UnitTestXMLV2.xml", loadDatabase );
            Assert.AreEqual( 1, loadDatabase.GetAllAdvertisings().Count() );
            var loadAdvertising = loadDatabase.GetAllAdvertisings().First();
            AssertAdvertisings( advertising, loadAdvertising, TestMode.XMLV3 );
        }
    }
}
