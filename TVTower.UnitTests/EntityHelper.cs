using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TVTower.UnitTests
{
    public static class EntityHelper
    {
        public static TVTAdvertising GetIconicAdvertising()
        {
            var advertising = new TVTAdvertising();
            advertising.GenerateGuid();
            advertising.OldId = "110";
            advertising.DataType = TVTDataType.Undefined;
            advertising.DataStatus = TVTDataStatus.Approved;
            advertising.DataRoot = TVTDataRoot.MadTV;
            advertising.DataUsage = TVTDataUsage.MadTVOriginal;
            advertising.Approved = true;
            advertising.CreatorId = "creatorX";
            advertising.EditorId = "editorY";
            advertising.LastModified = DateTime.Now.AddDays( -1 );
            advertising.IsNew = true;
            advertising.IsChanged = true;

            advertising.TitleDE = "ValueA";
            advertising.TitleEN = "ValueB";
            advertising.DescriptionDE = "ValueC";
            advertising.DescriptionEN = "ValueD";
            advertising.Quality = 50;
            advertising.FixPrice = true;
            advertising.MinAudience = (float)0.55;
            advertising.MinImage = 51;
            advertising.Repetitions = 2;
            advertising.Duration = 3;
            advertising.Profit = 110;
            advertising.Penalty = 120;
            advertising.ValidFrom = 1990;
            advertising.ValidTill = 1995;
            advertising.TargetGroup = TVTTargetGroup.Manager;
            advertising.AllowedGenres = new List<TVTProgrammeGenre>();
            advertising.AllowedGenres.Add( TVTProgrammeGenre.Comedy );
            advertising.AllowedGenres.Add( TVTProgrammeGenre.Drama );
            advertising.ProhibitedGenres = new List<TVTProgrammeGenre>();
            advertising.ProhibitedGenres.Add( TVTProgrammeGenre.Erotic );
            advertising.ProhibitedGenres.Add( TVTProgrammeGenre.Family );
            advertising.AllowedProgrammeTypes = new List<TVTProgrammeType>();
            advertising.AllowedProgrammeTypes.Add( TVTProgrammeType.Event );
            advertising.AllowedProgrammeTypes.Add( TVTProgrammeType.Commercial );
            advertising.ProhibitedProgrammeTypes = new List<TVTProgrammeType>();
            advertising.ProhibitedProgrammeTypes.Add( TVTProgrammeType.Movie );
            advertising.ProhibitedProgrammeTypes.Add( TVTProgrammeType.Reportage );

            advertising.ProPressureGroups = new List<TVTPressureGroup>();
            advertising.ProPressureGroups.Add( TVTPressureGroup.AntiSmoker );
            advertising.ProPressureGroups.Add( TVTPressureGroup.ArmsLobby );
            advertising.ContraPressureGroups = new List<TVTPressureGroup>();
            advertising.ContraPressureGroups.Add( TVTPressureGroup.Capitalists );
            advertising.ContraPressureGroups.Add( TVTPressureGroup.Communists );

            return advertising;
        }


    }

    public enum TestMode
    {
        XMLV3
    }

    [TestClass]
    public class EntityHelperTest
    {
        public void AssertEntity( TVTEntity ent1, TVTEntity ent2, TestMode testMode )
        {
            if ( testMode == TestMode.XMLV3 )
            {
                Assert.AreEqual( ent1.Id, ent2.Id );
                Assert.AreEqual( ent1.DataType, ent2.DataType );                
                Assert.AreEqual( ent1.CreatorId, ent2.CreatorId );
                Assert.AreEqual( ent1.LastModified.ToString(), ent2.LastModified.ToString() );
            }
            else
            {
                Assert.AreEqual( ent1.OldId, ent2.OldId );
                Assert.AreEqual( ent1.DataStatus, ent2.DataStatus );
                Assert.AreEqual( ent1.DataRoot, ent2.DataRoot );
                Assert.AreEqual( ent1.DataUsage, ent2.DataUsage );
                Assert.AreEqual( ent1.EditorId, ent2.EditorId );
                Assert.AreEqual( ent1.Approved, ent2.Approved );
                Assert.AreEqual( ent1.IsNew, ent2.IsNew );
                Assert.AreEqual( ent1.IsChanged, ent2.IsChanged );
            }
        }

        public void AssertAdvertisings( TVTAdvertising ad1, TVTAdvertising ad2, TestMode testMode )
        {
            AssertEntity( ad1, ad2, testMode );

            Assert.AreEqual( ad1.TitleDE, ad2.TitleDE );
            Assert.AreEqual( ad1.TitleEN, ad2.TitleEN );
            Assert.AreEqual( ad1.DescriptionDE, ad2.DescriptionDE );
            Assert.AreEqual( ad1.DescriptionEN, ad2.DescriptionEN );
            Assert.AreEqual( ad1.Quality, ad2.Quality );
            Assert.AreEqual( ad1.FixPrice, ad2.FixPrice );
            Assert.AreEqual( ad1.MinAudience, ad2.MinAudience );
            Assert.AreEqual( ad1.MinImage, ad2.MinImage );
            Assert.AreEqual( ad1.Repetitions, ad2.Repetitions );
            Assert.AreEqual( ad1.Duration, ad2.Duration );
            Assert.AreEqual( ad1.Profit, ad2.Profit );
            Assert.AreEqual( ad1.Penalty, ad2.Penalty );
            Assert.AreEqual( ad1.ValidFrom, ad2.ValidFrom );
            Assert.AreEqual( ad1.ValidTill, ad2.ValidTill );
            Assert.AreEqual( ad1.TargetGroup, ad2.TargetGroup );
            AssertExt.AreCollectionEqual( ad1.AllowedGenres, ad2.AllowedGenres );
            AssertExt.AreCollectionEqual( ad1.ProhibitedGenres, ad2.ProhibitedGenres );
            AssertExt.AreCollectionEqual( ad1.AllowedProgrammeTypes, ad2.AllowedProgrammeTypes );
            AssertExt.AreCollectionEqual( ad1.ProhibitedProgrammeTypes, ad2.ProhibitedProgrammeTypes );
            AssertExt.AreCollectionEqual( ad1.ProPressureGroups, ad2.ProPressureGroups );
            AssertExt.AreCollectionEqual( ad1.ContraPressureGroups, ad2.ContraPressureGroups );
        }
    }
}
