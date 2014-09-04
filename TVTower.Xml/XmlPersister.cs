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

        public void SaveXML(ITVTDatabase database, string filename, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            XmlDocument doc = new XmlDocument();

            var declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(declaration);

            var tvgdb = doc.CreateElement("tvgdb");
            doc.AppendChild(tvgdb);

            var version = doc.CreateElement("version");
            version.AddAttribute("value", ((int)dbVersion).ToString());
            version.AddAttribute("comment", "Export from TVTowerDBEditor");
            version.AddAttribute("exportDate", DateTime.Now.ToString());
            tvgdb.AppendChild(version);

            {
                var allmovies = doc.CreateElement("allmovies");
                //allmovies.AddElement( "version", CURRENT_VERSION.ToString() );
                tvgdb.AppendChild(allmovies);

                foreach (var movie in database.GetAllMovies())
                {
                    SetProgrammeDetailNode(doc, allmovies, movie, dbVersion, dataStructure);
                }
            }

            {
                var allepisodes = doc.CreateElement("allepisodes");
                //allmovies.AddElement( "version", CURRENT_VERSION.ToString() );
                tvgdb.AppendChild(allepisodes);

                foreach (var episode in database.GetAllEpisodes())
                {
                    SetEpisodeDetailNode(doc, allepisodes, episode, dbVersion, dataStructure);
                }
            }

            if (((int)dbVersion) >= 3)
            {
                var allpeople = doc.CreateElement("celebritypeople");
                //allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
                tvgdb.AppendChild(allpeople);

                foreach (var person in database.GetAllPeople())
                {
                    person.RefreshStatus();

                    if (person.DataStatus == TVTDataStatus.Approved && person.Approved && person.Prominence != 3)
                        SetCelebrityPersonDetailNode(doc, allpeople, person, dbVersion, dataStructure);
                }
            }

            if (((int)dbVersion) >= 3)
            {
                var allpeople = doc.CreateElement("insignificantpeople");
                //allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
                tvgdb.AppendChild(allpeople);

                foreach (var person in database.GetAllPeople())
                {
                    if (person.Prominence == 3)
                    {
                        if (person.DataStatus == TVTDataStatus.Approved && person.Approved && person.Prominence == 3)
                            SetInsignificantPersonDetailNode(doc, allpeople, person, dbVersion, dataStructure);
                    }
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

            if (((int)dbVersion) >= 3)
            {
                var allnews = doc.CreateElement("allnews");
                //allpeople.AddElement( "version", CURRENT_VERSION.ToString() );
                tvgdb.AppendChild(allnews);

                foreach (var news in database.GetAllNews())
                {
                    if (news.Approved)
                        SetNewsDetailNode(doc, allnews, news, dbVersion, dataStructure);
                }
            }

            var exportOptions = doc.CreateElement("exportOptions");
            exportOptions.AddAttribute("onlyFakes", (dataStructure == DataStructure.FakeData).ToString().ToLower());
            exportOptions.AddAttribute("onlyCustom", "false");
            exportOptions.AddAttribute("dataStructure", dataStructure.ToString());
            tvgdb.AppendChild(exportOptions);

            stopWatch.Stop();

            var time = doc.CreateElement("time");
            time.AddAttribute("value", stopWatch.ElapsedMilliseconds.ToString() + "ms");
            tvgdb.AppendChild(time);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "	";

            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                doc.Save(writer);
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
                dataNode.AddAttribute("flexible_profit", ad.FlexibleProfit ? "1" : "0");
                dataNode.AddAttribute("min_audience", ad.MinAudience.ToString());
                dataNode.AddAttribute("min_image", ad.MinImage.ToString());
                dataNode.AddAttribute("repetitions", ad.Repetitions.ToString());
                dataNode.AddAttribute("duration", ad.Duration.ToString());
                dataNode.AddAttribute("profit", ad.Profit.ToString());
                dataNode.AddAttribute("penalty", ad.Penalty.ToString());
                dataNode.AddAttribute("target_group", ((int)ad.TargetGroup).ToString());

                dataNode.AddAttribute("pro_pressure_groups", ad.ProPressureGroups.ToContentString(','));
                dataNode.AddAttribute("contra_pressure_groups", ad.ContraPressureGroups.ToContentString(','));

                adNode.AppendChild(dataNode);
            }

            if (ad.AllowedGenres != null && ad.AllowedGenres.Count > 0 ||
                ad.ProhibitedGenres != null && ad.ProhibitedGenres.Count > 0 ||
                ad.AllowedProgrammeTypes != null && ad.AllowedProgrammeTypes.Count > 0 ||
                ad.ProhibitedProgrammeTypes != null && ad.ProhibitedProgrammeTypes.Count > 0)
            {
                XmlNode conditionsNode = doc.CreateElement("conditions");
                conditionsNode.AddAttribute("allowed_genres", ad.AllowedGenres != null ? ad.AllowedGenres.ToContentString(',') : null);
                conditionsNode.AddAttribute("prohibited_genres", ad.ProhibitedGenres != null ? ad.ProhibitedGenres.ToContentString(',') : null);
                conditionsNode.AddAttribute("allowed_programme_types", ad.AllowedProgrammeTypes != null ? ad.AllowedProgrammeTypes.ToContentString(',') : null);
                conditionsNode.AddAttribute("prohibited_programme_types", ad.ProhibitedProgrammeTypes != null ? ad.ProhibitedProgrammeTypes.ToContentString(',') : null);

                adNode.AppendChild(conditionsNode);
            }

            return adNode;
        }

        public XmlNode SetNewsDetailNode(XmlDocument doc, XmlElement element, TVTNews news, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            XmlNode adNode = null;

            adNode = doc.CreateElement("news");
            {
                adNode.AddAttribute("id", news.Id.ToString());
                adNode.AddAttribute("thread_id", news.NewsThreadId);
                adNode.AddAttribute("predecessor_id", news.Predecessor != null ? news.Predecessor.Id.ToString() : null);
            }
            element.AppendChild(adNode);

            adNode.AddElement("title_de", news.TitleDE);
            adNode.AddElement("title_en", news.TitleEN);
            adNode.AddElement("description_de", news.DescriptionDE);
            adNode.AddElement("description_en", news.DescriptionEN);
                        
            {
                XmlNode dataNode = doc.CreateElement("data");
                
                dataNode.AddAttribute("genre", news.Genre.ToString());
                dataNode.AddAttribute("price", news.Price.ToString());
                dataNode.AddAttribute("topicality", news.Topicality.ToString());

                adNode.AppendChild(dataNode);
            }

            {
                XmlNode dataHandling = doc.CreateElement("handling");
                
                dataHandling.AddAttribute("type", ((int)news.NewsType).ToString());
                dataHandling.AddAttribute("handling", ((int)news.NewsHandling).ToString());

                dataHandling.AddAttribute("effect", ((int)news.Effect).ToString());
                dataHandling.AddAttribute("effect_params", news.EffectParameters.ToContentString(' '));

                dataHandling.AddAttribute("Resource_type_1", news.Resource1Type != null ? news.Resource1Type.ToString() : null);
                dataHandling.AddAttribute("Resource_type_2", news.Resource2Type != null ? news.Resource2Type.ToString() : null);
                dataHandling.AddAttribute("Resource_type_3", news.Resource3Type != null ? news.Resource3Type.ToString() : null);
                dataHandling.AddAttribute("Resource_type_4", news.Resource4Type != null ? news.Resource4Type.ToString() : null);

                adNode.AppendChild(dataHandling);
            }

            {
                XmlNode dataCond = doc.CreateElement("conditions");

                dataCond.AddAttribute("fix_year", news.FixYear.ToString());
                dataCond.AddAttribute("available_after_days", news.AvailableAfterDays.ToString());
                dataCond.AddAttribute("year_range_from", news.YearRangeFrom.ToString());
                dataCond.AddAttribute("year_range_to", news.YearRangeTo.ToString());
                dataCond.AddAttribute("min_hours_after_predecessor", news.MinHoursAfterPredecessor.ToString());
                dataCond.AddAttribute("time_range_from", news.TimeRangeFrom.ToString());
                dataCond.AddAttribute("time_range_to", news.TimeRangeTo.ToString());

                adNode.AppendChild(dataCond);
            }

            return adNode;
        }


        public XmlNode SetInsignificantPersonDetailNode(XmlDocument doc, XmlElement element, TVTPerson person, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            var personNode = doc.CreateElement("person");
            {
                personNode.AddAttribute("id", person.Id.ToString());

                switch (dataStructure)
                {
                    case DataStructure.FakeData:
                        if (!string.IsNullOrEmpty(person.FakeFirstName))
                            personNode.AddAttribute("first_name", person.FakeFirstName);
                        else
                            personNode.AddAttribute("first_name", person.FirstName);
                        personNode.AddAttribute("last_name", person.FakeLastName);
                        break;
                    case DataStructure.OriginalData:
                        personNode.AddAttribute("first_name", person.FirstName);
                        personNode.AddAttribute("last_name", person.LastName);
                        break;
                }
            }
            element.AppendChild(personNode);
            return personNode;
        }

        public XmlNode SetCelebrityPersonDetailNode(XmlDocument doc, XmlElement element, TVTPerson person, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            var personNode = doc.CreateElement("person");
            {
                personNode.AddAttribute("id", person.Id.ToString());
                personNode.AddAttribute("prominence", person.Prominence.ToString());
                personNode.AddAttribute("tmdb_id", person.TmdbId.ToString());
                personNode.AddAttribute("imdb_id", person.ImdbId != null ? person.ImdbId.ToString() : null);
            }
            element.AppendChild(personNode);

            switch (dataStructure)
            {
                case DataStructure.FakeData:
                    if (!string.IsNullOrEmpty(person.FakeFirstName))
                        personNode.AddElement("first_name", person.FakeFirstName);
                    else
                        personNode.AddElement("first_name", person.FirstName);
                    personNode.AddElement("last_name", person.FakeLastName);
                    break;
                case DataStructure.OriginalData:
                    personNode.AddElement("first_name", person.FirstName);
                    personNode.AddElement("last_name", person.LastName);
                    break;
            }

            personNode.AddElement("image_url", person.ImageUrl);
            personNode.AddElement("functions", person.Functions.ToContentString(';'));

            {
                XmlNode detailsNode = doc.CreateElement("details");

                detailsNode.AddAttribute("gender", ((int)person.Gender).ToString());
                detailsNode.AddAttribute("birthday", person.Birthday);
                detailsNode.AddAttribute("deathday", person.Deathday);
                detailsNode.AddAttribute("country", person.Country);

                personNode.AppendChild(detailsNode);
            }

            {
                XmlNode dataNode = doc.CreateElement("data");
                dataNode.AddAttribute("skill", person.Skill.ToString());
                dataNode.AddAttribute("fame", person.Fame.ToString());
                dataNode.AddAttribute("scandalizing", person.Scandalizing.ToString());
                dataNode.AddAttribute("pricefactor", person.PriceFactor.ToString());

                dataNode.AddAttribute("power", person.Power.ToString());
                dataNode.AddAttribute("humor", person.Humor.ToString());
                dataNode.AddAttribute("charisma", person.Charisma.ToString());
                dataNode.AddAttribute("appearance", person.Appearance.ToString());

                dataNode.AddAttribute("topgenre1", ((int)person.TopGenre1).ToString());
                dataNode.AddAttribute("topgenre2", ((int)person.TopGenre2).ToString());
                personNode.AppendChild(dataNode);
            }

            return personNode;
        }

        public XmlNode SetProgrammeDetailNode(XmlDocument doc, XmlElement element, TVTProgramme programme, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            XmlNode movieNode, dataNode;

            if (programme.ProgrammeType == TVTProgrammeType.Series)
                throw new NotImplementedException();

            movieNode = doc.CreateElement("movie");
            {
                movieNode.AddAttribute("id", programme.Id.ToString());
                //movieNode.AddAttribute("status", programme.DataStatus.ToString());
                movieNode.AddAttribute("tmdb_id", programme.TmdbId.ToString());
                movieNode.AddAttribute("imdb_id", programme.ImdbId);
                movieNode.AddAttribute("rt_id", programme.RottenTomatoesId.HasValue ? programme.RottenTomatoesId.Value.ToString() : "");
            }
            element.AppendChild(movieNode);

            switch (dataStructure)
            {
                case DataStructure.FakeData:
                    if (!string.IsNullOrEmpty(programme.FakeTitleDE))
                        movieNode.AddElement("title_de", programme.FakeTitleDE);
                    else
                        movieNode.AddElement("title_de", "NEED_FAKE: " + programme.TitleDE);

                    if (!string.IsNullOrEmpty(programme.FakeTitleEN))
                        movieNode.AddElement("title_en", programme.FakeTitleEN);
                    else
                        movieNode.AddElement("title_en", "NEED_FAKE: " + programme.TitleEN);

                    if (!string.IsNullOrEmpty(programme.FakeDescriptionDE))
                        movieNode.AddElement("description_de", programme.FakeDescriptionDE);
                    else
                        movieNode.AddElement("description_de", programme.DescriptionDE);

                    if (!string.IsNullOrEmpty(programme.FakeDescriptionEN))
                        movieNode.AddElement("description_en", programme.FakeDescriptionEN);
                    else
                        movieNode.AddElement("description_en", programme.DescriptionEN);
                    break;
                case DataStructure.OriginalData:
                    movieNode.AddElement("title_de", programme.TitleDE);
                    movieNode.AddElement("title_en", programme.TitleEN);
                    movieNode.AddElement("description_de", programme.DescriptionDE);
                    movieNode.AddElement("description_en", programme.DescriptionEN);
                    break;
            }

            //movieNode.AddElement( "version", movie.DataVersion.ToString() );

            movieNode.AddElement("programme_type", ((int)programme.ProgrammeType).ToString());
            movieNode.AddElement("image_url", programme.ImageUrl);

            dataNode = doc.CreateElement("data");
            {
                dataNode.AddAttribute("participants", programme.Participants.ToContentString(' '));
                dataNode.AddAttribute("director", programme.Director != null ? programme.Director.Id.ToString() : "");
                dataNode.AddAttribute("country", programme.Country);
                dataNode.AddAttribute("year", programme.Year.ToString());

                dataNode.AddAttribute("maingenre", ((int)programme.MainGenre).ToString());
                dataNode.AddAttribute("subgenre", ((int)programme.SubGenre).ToString());

                dataNode.AddAttribute("blocks", programme.Blocks.ToString());
                dataNode.AddAttribute("time", programme.LiveHour.ToString());

                dataNode.AddAttribute("flags", programme.Flags.ToContentString(' '));
                dataNode.AddAttribute("target_groups", programme.TargetGroups.ToContentString(' '));
                dataNode.AddAttribute("pro_pressure_groups", programme.ProPressureGroups.ToContentString(' '));
                dataNode.AddAttribute("contra_pressure_groups", programme.ContraPressureGroups.ToContentString(' '));

                dataNode.AddAttribute("price", programme.PriceMod.ToString());
                dataNode.AddAttribute("critics", programme.CriticsRate.ToString());
                dataNode.AddAttribute("speed", programme.ViewersRate.ToString());
                dataNode.AddAttribute("outcome", programme.BoxOfficeRate.ToString());
            }

            movieNode.AppendChild(dataNode);

            return movieNode;
        }

        public XmlNode SetEpisodeDetailNode(XmlDocument doc, XmlElement element, TVTEpisode episode, DatabaseVersion dbVersion, DataStructure dataStructure)
        {
            XmlNode movieNode, dataNode;

            movieNode = doc.CreateElement("episode");
            {
                movieNode.AddAttribute("id", episode.Id.ToString());

                if (episode.SeriesMaster != null && episode.SeriesMaster.IsAlive)
                    movieNode.AddAttribute("series_id", episode.SeriesMaster.TargetGeneric.Id.ToString());
                else
                    movieNode.AddAttribute("series_id", "");

                movieNode.AddAttribute("index", episode.EpisodeIndex.ToString());
            }
            element.AppendChild(movieNode);

            switch (dataStructure)
            {
                case DataStructure.FakeData:
                    if (!string.IsNullOrEmpty(episode.FakeTitleDE))
                        movieNode.AddElement("title_de", episode.FakeTitleDE);
                    else
                        movieNode.AddElement("title_de", "NEED_FAKE: " + episode.TitleDE);

                    if (!string.IsNullOrEmpty(episode.FakeTitleEN))
                        movieNode.AddElement("title_en", episode.FakeTitleEN);
                    else
                        movieNode.AddElement("title_en", "NEED_FAKE: " + episode.TitleEN);

                    if (!string.IsNullOrEmpty(episode.FakeDescriptionDE))
                        movieNode.AddElement("description_de", episode.FakeDescriptionDE);
                    else
                        movieNode.AddElement("description_de", episode.DescriptionDE);

                    if (!string.IsNullOrEmpty(episode.FakeDescriptionEN))
                        movieNode.AddElement("description_en", episode.FakeDescriptionEN);
                    else
                        movieNode.AddElement("description_en", episode.DescriptionEN);
                    break;
                case DataStructure.OriginalData:
                    movieNode.AddElement("title_de", episode.TitleDE);
                    movieNode.AddElement("title_en", episode.TitleEN);
                    movieNode.AddElement("description_de", episode.DescriptionDE);
                    movieNode.AddElement("description_en", episode.DescriptionEN);
                    break;
            }

            //movieNode.AddElement( "version", movie.DataVersion.ToString() );

            dataNode = doc.CreateElement("data");
            {
                dataNode.AddAttribute("participants", episode.Participants.ToContentString(' '));
                dataNode.AddAttribute("director", episode.Director != null ? episode.Director.Id.ToString() : "");

                dataNode.AddAttribute("critics", episode.CriticsRate.ToString());
                dataNode.AddAttribute("speed", episode.ViewersRate.ToString());
            }

            movieNode.AppendChild(dataNode);

            return movieNode;
        }

        public ITVTDatabase LoadXML(string filename, ITVTDatabase database)
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









        private void ConvertOldMovieData(TVTProgramme movie, int version)
        {
            if (version <= 2) //Alte BlitzMax-Datenbank
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
