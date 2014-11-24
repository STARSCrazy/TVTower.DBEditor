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

        public static TVTPerson GetMinimalPerson( string firstName, string lastName )
        {
            var person = new TVTPerson();
            person.GenerateGuid();
            person.FirstName = firstName;
            person.LastName = lastName;
            return person;
        }

        public static TVTProgramme GetIconicProgramme()
        {
            var programme = new TVTProgramme();
            programme.GenerateGuid();
            programme.OldId = "110";
            programme.DataType = TVTDataType.Undefined;
            programme.DataStatus = TVTDataStatus.Approved;
            programme.DataRoot = TVTDataRoot.MadTV;
            programme.DataUsage = TVTDataUsage.MadTVOriginal;
            programme.Approved = true;
            programme.CreatorId = "creatorX";
            programme.EditorId = "editorY";
            programme.LastModified = DateTime.Now.AddDays( -1 );
            programme.IsNew = true;
            programme.IsChanged = true;

            programme.TitleDE = "ValueA";
            programme.TitleEN = "ValueB";
            programme.DescriptionDE = "ValueC";
            programme.DescriptionEN = "ValueD";

            programme.ProductType = TVTProductType.Programme;
            programme.ProgrammeType = TVTProgrammeType.Reportage;
            programme.FakeTitleDE = "fakeA";
            programme.FakeTitleEN = "fakeB";
            programme.FakeDescriptionDE = "fakeDescA";
            programme.FakeDescriptionEN = "fakeDescB";
            programme.DescriptionMovieDB = "hkh";

            var personA = GetMinimalPerson( "PersA", "PersALast" );
            var personB = GetMinimalPerson( "PersB", "PersBLast" );
            var personC = GetMinimalPerson( "PersC", "PersCLast" );
            programme.Staff.Add( new TVTStaff( personA, TVTPersonFunction.Actor ) );
            programme.Staff.Add( new TVTStaff( personB, TVTPersonFunction.Actor ) );
            programme.Staff.Add( new TVTStaff( personC, TVTPersonFunction.Director ) );

            programme.BettyBonus = 11;
            programme.PriceMod = (float)1.22;
            programme.CriticsRate = 33;
            programme.ViewersRate = 44;
            programme.BoxOfficeRate = 55;

            programme.Country = "FR";
            programme.Year = 1952;
            programme.DistributionChannel = TVTDistributionChannel.Auction;

            programme.MainGenre = TVTProgrammeGenre.Drama;
            programme.SubGenre = TVTProgrammeGenre.Fantasy;

            programme.Blocks = 3;
            programme.LiveHour = 20;

            programme.Flags.Add( TVTProgrammeFlag.Cult );
            programme.Flags.Add( TVTProgrammeFlag.BMovie );

            programme.TargetGroups.Add( TVTTargetGroup.Manager );
            programme.TargetGroups.Add( TVTTargetGroup.Pensioners );

            programme.ProPressureGroups = new List<TVTPressureGroup>();
            programme.ProPressureGroups.Add( TVTPressureGroup.AntiSmoker );
            programme.ProPressureGroups.Add( TVTPressureGroup.ArmsLobby );
            programme.ContraPressureGroups = new List<TVTPressureGroup>();
            programme.ContraPressureGroups.Add( TVTPressureGroup.Capitalists );
            programme.ContraPressureGroups.Add( TVTPressureGroup.Communists );

            programme.ImdbId = "pp";
            programme.TmdbId = 55;
            programme.RottenTomatoesId = 99;
            programme.ImageUrl = "urlX";

            return programme;
        }

        public static TVTPerson GetIconicPerson( string firstName, string lastName )
        {
            var person = new TVTPerson();            
            person.GenerateGuid();
            person.OldId = "110";
            person.DataType = TVTDataType.Undefined;
            person.DataStatus = TVTDataStatus.Approved;
            person.DataRoot = TVTDataRoot.MadTV;
            person.DataUsage = TVTDataUsage.MadTVOriginal;
            person.Approved = true;
            person.CreatorId = "creatorX";
            person.EditorId = "editorY";
            person.LastModified = DateTime.Now.AddDays( -1 );
            person.IsNew = true;
            person.IsChanged = true;

            person.FirstName = firstName;
            person.LastName = lastName;
            person.NickName = "nick";

            person.FakeFirstName = firstName + "Fake";
            person.FakeLastName = lastName + "Fake";
            person.FakeNickName = "nickFake";

            person.TmdbId = 111;
            person.ImdbId = "dbdb";
            person.ImageUrl = "imgUrl";

            person.Functions.Add(TVTPersonFunction.Actor);

            person.Gender = TVTPersonGender.Female;
            person.Birthday = 1999;
            person.Deathday = 2010;
            person.Country = "FR";
            person.Prominence = 2;
            person.Skill = 99;
            person.Fame = 10;
            person.Scandalizing = 33;
            person.PriceMod = 20;
            person.Power = 30;
            person.Humor = 20;
            person.Charisma = 70;
            person.Appearance = 80;
            person.TopGenre1 = TVTProgrammeGenre.Erotic;
            person.TopGenre2 = TVTProgrammeGenre.Fantasy;
            person.ProgrammeCount = 33;

            return person;
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

        public void AssertPeople( TVTPerson pers1, TVTPerson pers2, TestMode testMode, bool isFake = false )
        {
            AssertEntity( pers1, pers2, testMode );

            if ( isFake )
            {
                Assert.AreEqual( pers1.FakeFirstName, pers2.FakeFirstName );
                Assert.AreEqual( pers1.FakeLastName, pers2.FakeLastName );
                Assert.AreEqual( pers1.FakeNickName, pers2.FakeNickName );
            } else {
                Assert.AreEqual( pers1.FirstName, pers2.FirstName );
                Assert.AreEqual( pers1.LastName, pers2.LastName );
                Assert.AreEqual( pers1.NickName, pers2.NickName );
            }
            Assert.AreEqual( pers1.TmdbId, pers2.TmdbId );
            Assert.AreEqual( pers1.ImdbId, pers2.ImdbId );
            Assert.AreEqual( pers1.ImageUrl, pers2.ImageUrl );
            AssertExt.AreCollectionEqual( pers1.Functions, pers2.Functions );
            Assert.AreEqual( pers1.Gender, pers2.Gender );
            Assert.AreEqual( pers1.Birthday, pers2.Birthday );
            Assert.AreEqual( pers1.Deathday, pers2.Deathday );
            Assert.AreEqual( pers1.Country, pers2.Country );
            Assert.AreEqual( pers1.Prominence, pers2.Prominence );
            Assert.AreEqual( pers1.Skill, pers2.Skill );
            Assert.AreEqual( pers1.Fame, pers2.Fame );
            Assert.AreEqual( pers1.Scandalizing, pers2.Scandalizing );
            Assert.AreEqual( pers1.PriceMod, pers2.PriceMod );
            Assert.AreEqual( pers1.Power, pers2.Power );
            Assert.AreEqual( pers1.Humor, pers2.Humor );
            Assert.AreEqual( pers1.Charisma, pers2.Charisma );
            Assert.AreEqual( pers1.Appearance, pers2.Appearance );
            Assert.AreEqual( pers1.TopGenre1, pers2.TopGenre1 );
            Assert.AreEqual( pers1.TopGenre2, pers2.TopGenre2 );
            if ( testMode != TestMode.XMLV3 )
            {
                Assert.AreEqual( pers1.ProgrammeCount, pers2.ProgrammeCount );
            }
        }

        public void AssertProgrammes( TVTProgramme prog1, TVTProgramme prog2, TestMode testMode )
        {
            AssertEntity( prog1, prog2, testMode );

            Assert.AreEqual( prog1.TitleDE, prog2.TitleDE );
            Assert.AreEqual( prog1.TitleEN, prog2.TitleEN );
            Assert.AreEqual( prog1.DescriptionDE, prog2.DescriptionDE );
            Assert.AreEqual( prog1.DescriptionEN, prog2.DescriptionEN );

            Assert.AreEqual( prog1.ProductType, prog2.ProductType );
            Assert.AreEqual( prog1.ProgrammeType, prog2.ProgrammeType );
            Assert.AreEqual( prog1.FakeTitleDE, prog2.FakeTitleDE );
            Assert.AreEqual( prog1.FakeTitleEN, prog2.FakeTitleEN );
            Assert.AreEqual( prog1.FakeDescriptionDE, prog2.FakeDescriptionDE );
            Assert.AreEqual( prog1.FakeDescriptionEN, prog2.FakeDescriptionEN );

            Assert.AreEqual( prog1.DescriptionMovieDB, prog2.DescriptionMovieDB );
            Assert.AreEqual( prog1.Staff, prog2.Staff );
            Assert.AreEqual( prog1.BettyBonus, prog2.BettyBonus );
            Assert.AreEqual( prog1.PriceMod, prog2.PriceMod );
            Assert.AreEqual( prog1.CriticsRate, prog2.CriticsRate );
            Assert.AreEqual( prog1.ViewersRate, prog2.ViewersRate );
            Assert.AreEqual( prog1.BoxOfficeRate, prog2.BoxOfficeRate );
            Assert.AreEqual( prog1.Country, prog2.Country );
            Assert.AreEqual( prog1.Year, prog2.Year );
            Assert.AreEqual( prog1.DistributionChannel, prog2.DistributionChannel );
            Assert.AreEqual( prog1.MainGenre, prog2.MainGenre );
            Assert.AreEqual( prog1.SubGenre, prog2.SubGenre );
            Assert.AreEqual( prog1.Blocks, prog2.Blocks );
            Assert.AreEqual( prog1.LiveHour, prog2.LiveHour );
            Assert.AreEqual( prog1.Flags, prog2.Flags );
            Assert.AreEqual( prog1.TargetGroups, prog2.TargetGroups );
            AssertExt.AreCollectionEqual( prog1.ProPressureGroups, prog2.ProPressureGroups );
            AssertExt.AreCollectionEqual( prog1.ContraPressureGroups, prog2.ContraPressureGroups );
            Assert.AreEqual( prog1.ImdbId, prog2.ImdbId );
            Assert.AreEqual( prog1.TmdbId, prog2.TmdbId );
            Assert.AreEqual( prog1.RottenTomatoesId, prog2.RottenTomatoesId );
            Assert.AreEqual( prog1.ImageUrl, prog2.ImageUrl );
        }
    }
}
