using System;
using System.Diagnostics;
using System.Xml;
using TVTower.Entities;
using TVTower.Xml.Persister;
using TVTower.Converter;

namespace TVTower.Xml
{
	public enum DatabaseVersion
	{
		V2 = 2,
		V3 = 3
	}

	public enum DataStructure
	{
		Full,
		FakeData,
		OriginalData
	}

	public class XmlPersister
	{
		public const int CURRENT_VERSION = 3;

		public void SaveXML( ITVTDatabase database, string filename, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			XmlDocument doc = new XmlDocument();

			var declaration = doc.CreateXmlDeclaration( "1.0", "utf-8", null );
			doc.AppendChild( declaration );

			var tvgdb = doc.CreateElement( "tvgdb" );
			doc.AppendChild( tvgdb );

			var version = doc.CreateElement( "version" );
			version.AddAttribute( "value", ((int)dbVersion).ToString() );
			version.AddAttribute( "comment", "Export from TVTowerDBEditor" );
			version.AddAttribute( "exportDate", DateTime.Now.ToString() );
			tvgdb.AppendChild( version );

			var allmovies = doc.CreateElement( "allmovies" );
			//allmovies.AddElement( "version", CURRENT_VERSION.ToString() );
			tvgdb.AppendChild( allmovies );

			foreach ( var movie in database.GetAllMovies() )
			{
				//TODO SetMovieDetailNode( doc, allmovies, movie, dbVersion, dataStructure );
			}

			if ( ((int)dbVersion) >= 3 )
			{
				var allpeople = doc.CreateElement( "allpeople" );
				//allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
				tvgdb.AppendChild( allpeople );

				foreach ( var person in database.GetAllPeople() )
				{                    
                    SetPersonDetailNode( doc, allpeople, person, dbVersion, dataStructure );
				}
			}

            if (((int)dbVersion) >= 3)
            {
                var allads = doc.CreateElement("allads");
                //allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
                tvgdb.AppendChild(allads);

                foreach (var ad in database.GetAllAdvertisings())
                {
                    if (ad.Approved)
                        SetAdvertisingDetailNode(doc, allads, ad, dbVersion, dataStructure);
                }
            }

			var exportOptions = doc.CreateElement( "exportOptions" );
			exportOptions.AddAttribute( "onlyFakes", (dataStructure == DataStructure.FakeData).ToString().ToLower() );
			exportOptions.AddAttribute( "onlyCustom", "false" );
			exportOptions.AddAttribute( "dataStructure", dataStructure.ToString() );
			tvgdb.AppendChild( exportOptions );

			stopWatch.Stop();

			var time = doc.CreateElement( "time" );
			time.AddAttribute( "value", stopWatch.ElapsedMilliseconds.ToString() + "ms" );
			tvgdb.AppendChild( time );

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "	";

			using ( XmlWriter writer = XmlWriter.Create( filename, settings ) )
			{
				doc.Save( writer );
			}
		}

        public XmlNode SetAdvertisingDetailNode(XmlDocument doc, XmlElement element, TVTAdvertising ad, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            XmlNode adNode = null;

            adNode = doc.CreateElement("ad");
            {
                adNode.AddAttribute("id", ad.Id.ToString());
            }
            element.AppendChild(adNode);

            adNode.AddElement("title_de", ad.FakeTitleDE);
            adNode.AddElement("title_en", ad.FakeTitleEN);
            adNode.AddElement("description_de", ad.FakeDescriptionDE);
            adNode.AddElement("description_en", ad.FakeDescriptionEN);

            {
                XmlNode dataNode = doc.CreateElement("data");
                dataNode.AddAttribute("infomercial", ad.Infomercial ? "1" : "0");
                dataNode.AddAttribute("quality", ad.Quality.ToString());
                dataNode.AddAttribute("flexibleprofit", ad.FlexibleProfit ? "1" : "0");
                dataNode.AddAttribute("minaudience", ad.MinAudience.ToString());
                dataNode.AddAttribute("minimage", ad.MinImage.ToString());
                dataNode.AddAttribute("repetitions", ad.Repetitions.ToString());
                dataNode.AddAttribute("duration", ad.Duration.ToString());
                dataNode.AddAttribute("profit", ad.Profit.ToString());
                dataNode.AddAttribute("penalty", ad.Penalty.ToString());
                dataNode.AddAttribute("targetgroup", ((int)ad.TargetGroup).ToString());

                adNode.AppendChild(dataNode);
            }

            if (ad.AllowedGenres != null && ad.AllowedGenres.Count > 0 ||
                ad.ProhibitedGenres != null && ad.ProhibitedGenres.Count > 0 ||
                ad.AllowedProgrammeTypes != null && ad.AllowedProgrammeTypes.Count > 0 ||
                ad.ProhibitedProgrammeTypes != null && ad.ProhibitedProgrammeTypes.Count > 0)
            {
                XmlNode conditionsNode = doc.CreateElement("conditions");
                conditionsNode.AddAttribute("allowedgenres", ad.AllowedGenres != null ? ad.AllowedGenres.ToContentString(",") : null);
                conditionsNode.AddAttribute("prohibitedgenres", ad.ProhibitedGenres != null ? ad.ProhibitedGenres.ToContentString(",") : null);
                conditionsNode.AddAttribute("allowedprogrammetypes", ad.AllowedProgrammeTypes != null ? ad.AllowedProgrammeTypes.ToContentString(",") : null);
                conditionsNode.AddAttribute("prohibitedprogrammetypes", ad.ProhibitedProgrammeTypes != null ? ad.ProhibitedProgrammeTypes.ToContentString(",") : null);

                adNode.AppendChild(conditionsNode);
            }            

            return adNode;
        }

		public XmlNode SetPersonDetailNode( XmlDocument doc, XmlElement element, TVTPerson person, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			XmlNode personNode = null;

            //personNode = doc.CreateElement( "person" );
            //{
            //    personNode.AddAttribute( "id", person.Id.ToString() );
            //    if ( dataStructure == DataStructure.Full )
            //        personNode.AddAttribute( "type", person.DataContent.ToString() );
            //}
            //element.AppendChild( personNode );

            //switch ( dataStructure )
            //{
            //    case DataStructure.Full:
            //        personNode.AddElement( "name", person.FakeFullName );
            //        break;
            //    case DataStructure.FakeData:
            //        personNode.AddElement( "name", person.FakeFullName );
            //        break;
            //    case DataStructure.OriginalData:
            //        personNode.AddElement( "name", person.FullName );
            //        break;
            //}

            //personNode.AddElement( "function", person.Functions.ToContentString( ";" ) );
            //personNode.AddElement( "gender", person.Gender.ToString() );
            //personNode.AddElement( "birthday", person.Birthday );
            //personNode.AddElement( "deathday", person.Deathday );
            //personNode.AddElement( "country", person.Country );

            //personNode.AddElement( "image_url", person.ImageUrl );

            //XmlNode referencesNode = doc.CreateElement( "references" );
            //referencesNode.AddAttribute( "tmdb_id", person.TmdbId.ToString() );
            //referencesNode.AddAttribute( "imdb_id", person.ImdbId );
            //personNode.AppendChild( referencesNode );

            ////personNode.AddElement( "tmdb_id", person.TmdbId.ToString() );
            ////personNode.AddElement( "imdb_id", person.ImdbId );
            ////personNode.AddElement( "image_url", person.ImageUrl );

            //if ( dataStructure == DataStructure.Full )
            //{
            //    XmlNode additionalNode = doc.CreateElement( "additional" );
            //    additionalNode.AddAttribute( "original_name", person.FullName );
            //    additionalNode.AddAttribute( "place_of_birth", person.PlaceOfBirth );
            //    //additionalNode.AddAttribute( "info", person.Info );
            //    //additionalNode.AddAttribute( "movieRegistrations", person.MovieRegistrations.ToString() );
            //    //additionalNode.AddAttribute( "otherInfo", person.OtherInfo );
            //    personNode.AppendChild( additionalNode );
            //}

            //if ( dataStructure == DataStructure.Full || dataStructure == DataStructure.FakeData || dataStructure == DataStructure.OriginalData )
            //{
            //    XmlNode dataNode = doc.CreateElement( "data" );
            //    dataNode.AddAttribute( "professionSkill", person.ProfessionSkill.ToString() );
            //    dataNode.AddAttribute( "fame", person.Fame.ToString() );
            //    //dataNode.AddAttribute( "success", person.Success.ToString() );
            //    dataNode.AddAttribute( "power", person.Power.ToString() );
            //    dataNode.AddAttribute( "humor", person.Humor.ToString() );
            //    dataNode.AddAttribute( "charisma", person.Charisma.ToString() );
            //    dataNode.AddAttribute( "eroticAura", person.EroticAura.ToString() );
            //    dataNode.AddAttribute( "characterSkill", person.CharacterSkill.ToString() );
            //    dataNode.AddAttribute( "scandalizing", person.Scandalizing.ToString() );
            //    dataNode.AddAttribute( "priceFactor", person.PriceFactor.ToString() );
            //    dataNode.AddAttribute( "topGenre1", person.TopGenre1.ToString() );
            //    dataNode.AddAttribute( "topGenre2", person.TopGenre2.ToString() );
            //    personNode.AppendChild( dataNode );
            //}

			return personNode;
		}

		//public XmlNode SetMovieDetailNode( XmlDocument doc, XmlElement element, TVTMovie movie, DatabaseVersion dbVersion, DataStructure dataStructure )
		//{
		//    XmlNode movieNode, dataNode;

		//    if ( movie.IsSeries )
		//        throw new NotImplementedException();

		//    movieNode = doc.CreateElement( "movie" );
		//    if ( dbVersion != DatabaseVersion.V2 )
		//    {
		//        movieNode.AddAttribute( "id", movie.Id.ToString() );
		//        movieNode.AddAttribute( "status", movie.DataStatus.ToString() );
		//    }
		//    element.AppendChild( movieNode );

		//    switch ( dataStructure )
		//    {
		//        case DataStructure.Full:
		//            movieNode.AddElement( "title_de", movie.TitleDE );
		//            movieNode.AddElement( "title_en", movie.TitleEN );
		//            movieNode.AddElement( "original_title_de", movie.OriginalTitleDE );
		//            movieNode.AddElement( "original_title_en", movie.OriginalTitleEN );
		//            movieNode.AddElement( "description_de", movie.DescriptionDE );
		//            movieNode.AddElement( "description_en", movie.DescriptionEN );
		//            movieNode.AddElement( "original_description_de", movie.OriginalDescriptionDE );
		//            movieNode.AddElement( "original_description_en", movie.OriginalDescriptionEN );
		//            movieNode.AddElement( "description_tmdb", movie.DescriptionMovieDB );
		//            break;
		//        case DataStructure.FakeData:
		//            if ( dbVersion == DatabaseVersion.V2 )
		//            {
		//                movieNode.AddElement( "title", movie.TitleDE );
		//                movieNode.AddElement( "description", movie.DescriptionDE );
		//            }
		//            else
		//            {
		//                movieNode.AddElement( "title_de", movie.TitleDE );
		//                movieNode.AddElement( "title_en", movie.TitleEN );
		//                movieNode.AddElement( "description_de", movie.DescriptionDE );
		//                movieNode.AddElement( "description_en", movie.DescriptionEN );
		//            }
		//            break;
		//        case DataStructure.OriginalData:
		//            if ( dbVersion == DatabaseVersion.V2 )
		//            {
		//                movieNode.AddElement( "title", movie.OriginalTitleDE );
		//                movieNode.AddElement( "description", movie.OriginalDescriptionDE );
		//            }
		//            else
		//            {
		//                movieNode.AddElement( "title_de", movie.OriginalTitleDE );
		//                movieNode.AddElement( "title_en", movie.OriginalTitleEN );
		//                movieNode.AddElement( "description_de", movie.OriginalDescriptionDE );
		//                movieNode.AddElement( "description_en", movie.OriginalDescriptionEN );
		//            }
		//            break;
		//    }

		//    //movieNode.AddElement( "version", movie.DataVersion.ToString() );

		//    dataNode = doc.CreateElement( "data" );

		//    if ( dbVersion == DatabaseVersion.V2 )
		//    {
		//        //altes Format
		//        dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.Name ).ToContentString( ", " ) );
		//        dataNode.AddAttribute( "director", movie.Director != null ? movie.Director.Name : "" );
		//    }
		//    else
		//    {
		//        movieNode.AddElement( "image_url", movie.ImageUrl );

		//        XmlNode referencesNode = doc.CreateElement( "references" );
		//        referencesNode.AddAttribute( "tmdb_id", movie.TmdbId.ToString() );
		//        referencesNode.AddAttribute( "imdb_id", movie.ImdbId );
		//        referencesNode.AddAttribute( "rt_id", movie.RottenTomatoesId.HasValue ? movie.RottenTomatoesId.Value.ToString() : "" );
		//        movieNode.AppendChild( referencesNode );

		//        //neues Format
		//        dataNode.AddAttribute( "actors", movie.Actors.Select( x => x.Name ).ToContentString( ", " ) );
		//        dataNode.AddAttribute( "actor_ids", movie.Actors.Select( x => x.Id ).ToContentString( ";" ) );
		//        dataNode.AddAttribute( "director", movie.Director != null ? movie.Director.Name : "" );
		//        dataNode.AddAttribute( "director_id", movie.Director != null ? movie.Director.Id.ToString() : "" );
		//    }

		//    dataNode.AddAttribute( "country", movie.Country );
		//    dataNode.AddAttribute( "year", movie.Year.ToString() );

		//    if ( dbVersion == DatabaseVersion.V2 )
		//    {
		//        dataNode.AddAttribute( "genre", movie.GenreOldVersion.ToString() );
		//    }
		//    else
		//    {
		//        dataNode.AddAttribute( "main_genre", ((int)movie.MainGenre).ToString() );
		//        dataNode.AddAttribute( "sub_genre", ((int)movie.SubGenre).ToString() );
		//        dataNode.AddAttribute( "show_genre", ((int)movie.ShowGenre).ToString() );
		//        dataNode.AddAttribute( "reportage_genre", ((int)movie.ReportageGenre).ToString() );
		//    }

		//    dataNode.AddAttribute( "blocks", movie.Blocks.ToString() );
		//    dataNode.AddAttribute( "time", movie.LiveHour.ToString() );

		//    if ( dbVersion != DatabaseVersion.V2 )
		//    {
		//        dataNode.AddAttribute( "flags", movie.Flags.ToContentString( " " ) );
		//        dataNode.AddAttribute( "target_groups", movie.TargetGroups.ToContentString( " " ) );
		//        dataNode.AddAttribute( "pro_pressure_groups", movie.ProPressureGroups.ToContentString( " " ) );
		//        dataNode.AddAttribute( "contra_pressure_groups", movie.ContraPressureGroups.ToContentString( " " ) );
		//    }

		//    dataNode.AddAttribute( "price", movie.PriceRate.ToString() );
		//    dataNode.AddAttribute( "critics", movie.CriticsRate.ToString() );
		//    dataNode.AddAttribute( "speed", movie.ViewersRate.ToString() );
		//    dataNode.AddAttribute( "outcome", movie.BoxOfficeRate.ToString() );

		//    movieNode.AppendChild( dataNode );

		//    if ( dataStructure == DataStructure.Full )
		//    {

		//        XmlNode additionalNode = doc.CreateElement( "additional" );
		//        additionalNode.AddAttribute( "main_genre_raw", movie.MovieAdditional.MainGenreRaw.ToString() );
		//        additionalNode.AddAttribute( "sub_genre_raw", movie.MovieAdditional.SubGenreRaw.ToString() );
		//        additionalNode.AddAttribute( "genre_old_version", movie.MovieAdditional.GenreOldVersion.ToString() );
		//        additionalNode.AddAttribute( "price_rate_old", movie.MovieAdditional.PriceRateOld.ToString() );
		//        additionalNode.AddAttribute( "critic_rate_old", movie.MovieAdditional.CriticRateOld.ToString() );
		//        additionalNode.AddAttribute( "speed_rate_old", movie.MovieAdditional.SpeedRateOld.ToString() );
		//        additionalNode.AddAttribute( "boxoffice_rate_old", movie.MovieAdditional.BoxOfficeRateOld.ToString() );
		//        additionalNode.AddAttribute( "budget", movie.MovieAdditional.Budget.ToString() );
		//        additionalNode.AddAttribute( "revenue", movie.MovieAdditional.Revenue.ToString() );
		//        movieNode.AppendChild( additionalNode );
		//    }

		//    return movieNode;
		//}

		public ITVTDatabase LoadXML( string filename, ITVTDatabase database )
		{
            var result = database;
            //int version = 0;
            //DatabaseVersion dbVersion = DatabaseVersion.V2;
            //TVTDataContent defaultType = TVTDataContent.FakeWithRefId;

            //var doc = new XmlDocument();

            //doc.Load( filename );

            //var versionElement = doc.GetElementsByTagName( "version" );
            //if ( versionElement[0].HasAttribute( "value" ) )
            //{
            //    version = versionElement[0].GetAttributeInteger( "value" );

            //    if ( version == 1 )
            //        throw new NotSupportedException( "database version '1' is not supported." );
            //    if ( version == 3 )
            //        dbVersion = DatabaseVersion.V3;
            //}

            //if ( version == 2 )
            //{
            //    var exportOptions = doc.GetElementsByTagName( "exportOptions" );
            //    if ( bool.Parse( exportOptions[0].GetAttribute( "onlyFakes" ) ) )
            //    {
            //        defaultType = TVTDataContent.Fake;
            //    }
            //}

            //{
            //    var allMovies = doc.GetElementsByTagName( "allmovies" );
            //    var movieExtPersister = new TVTMoviePersister();

            //    foreach ( XmlNode xmlMovie in allMovies )
            //    {
            //        foreach ( XmlNode childNode in xmlMovie.ChildNodes )
            //        {
            //            var movie = new TVTProgramme();
            //            movie.MovieAdditional = new TVTMovieAdditional();
            //            if ( version == 2 )
            //            {
            //                movie.GenerateGuid();
            //                movie.DataContent = defaultType;
            //            }

            //            switch ( childNode.Name )
            //            {
            //                case "movie":
            //                    movieExtPersister.Load( childNode, movie, result, dbVersion, DataStructure.FakeData ); //TODO
            //                    break;
            //            }

            //            ConvertOldMovieData( movie, version );
            //            result.AddMovie( movie );
            //        }
            //    }
            //}


            //{
            //    var allSeries = doc.GetElementsByTagName( "allseries" );
            //    var seriesPersister = new TVTSeriesMoviePersister();

            //    foreach ( XmlNode xmlSeries in allSeries )
            //    {
            //        foreach ( XmlNode childNode in xmlSeries.ChildNodes )
            //        {
            //            var movie = new TVTProgramme();
            //            movie.MovieAdditional = new TVTMovieAdditional();
            //            if ( version == 2 )
            //            {
            //                movie.GenerateGuid();
            //                movie.DataContent = defaultType;
            //            }

            //            switch ( childNode.Name )
            //            {
            //                case "serie":
            //                    seriesPersister.Load( childNode, movie, result, dbVersion, DataStructure.FakeData );
            //                    break;
            //            }

            //            ConvertOldMovieData( movie, version );
            //            result.AddMovie( movie );
            //        }
            //    }
            //}

			//var allSeries = doc.GetElementsByTagName( "allseries" );

			return result;
		}









		private void ConvertOldMovieData( TVTProgramme movie, int version )
		{
			if ( version <= 2 ) //Alte BlitzMax-Datenbank
			{
				movie.MovieAdditional.GenreOldVersion = movie.MovieAdditional.MainGenreRaw;
                OldV2Converter.ConvertGenreAndFlags(movie, null);
			}
			else
			{
				movie.MainGenre = (TVTProgrammeGenre)movie.MovieAdditional.MainGenreRaw;
                movie.SubGenre = (TVTProgrammeGenre)movie.MovieAdditional.SubGenreRaw;
			}
		}


	}
}
