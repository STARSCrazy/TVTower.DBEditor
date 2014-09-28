using System;
using System.Collections.Generic;
using System.Xml;
using TVTower.Converter;
using TVTower.Entities;

namespace TVTower.Xml
{
    //public enum DatabaseVersion
    //{
    //    V2 = 2,
    //    V3 = 3
    //}

    //public enum DataStructure
    //{
    //    Full,
    //    FakeData,
    //    OriginalData
    //}

    public class XmlPersisterV2
    {
        public int TempIdCounter = 0;

        public bool NodeIsEmpty( XmlNode node )
        {
            return ( node.Attributes.Count == 0 && !node.HasChildNodes );
        }

        public MovieOldV2 LoadMovieBase( XmlNode xmlNode, bool isFake )
        {
            var result = new MovieOldV2();
            result.id = TempIdCounter++;

            foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
            {
                switch ( movieChild.Name )
                {
                    case "title":
                        if ( isFake )
                            result.titleFake = movieChild.GetElementValue();
                        else
                            result.title = movieChild.GetElementValue();
                        break;
                    case "description":
                        result.description = movieChild.GetElementValue();
                        break;
                    case "data":
                        result.actors = movieChild.GetAttribute( "actors" );
                        result.director = movieChild.GetAttribute( "director" );
                        result.country = movieChild.GetAttribute( "country" );
                        result.year = movieChild.GetAttributeInteger( "year" );
                        result.genre = movieChild.GetAttributeInteger( "genre" );
                        result.blocks = movieChild.GetAttributeInteger( "blocks" );
                        result.time = movieChild.GetAttributeInteger( "time" );
                        result.price = movieChild.GetAttributeInteger( "price" );
                        result.critics = movieChild.GetAttributeInteger( "critics" );
                        result.speed = movieChild.GetAttributeInteger( "speed" );
                        result.outcome = movieChild.GetAttributeInteger( "outcome" );
                        result.xrated = movieChild.GetAttributeInteger( "xrated" ) == 1;
                        result.deleted = false;
                        result.custom = false;
                        break;
                }
            }

            return result;
        }

        public List<MovieOldV2> LoadMovie( XmlNode xmlNode, bool isFake )
        {
            var result = new List<MovieOldV2>();
            MovieOldV2 currentMovie = LoadMovieBase( xmlNode, isFake );
            result.Add( currentMovie );

            foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
            {
                switch ( movieChild.Name )
                {
                    case "episode":
                        var episode = LoadMovieBase( movieChild, isFake );
                        episode.episode = movieChild.GetAttributeInteger( "number" );
                        episode.parentID = currentMovie.id;
                        result.Add( episode );
                        break;
                }
            }

            return result;
        }

        public NewsOldV2 LoadNewsBase( XmlNode xmlNode, bool isFake )
        {
            var result = new NewsOldV2();
            result.id = TempIdCounter++;

            foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
            {
                switch ( movieChild.Name )
                {
                    case "title":
                        if ( isFake )
                            result.titleFake = movieChild.GetElementValue();
                        else
                            result.title = movieChild.GetElementValue();
                        break;
                        break;
                    case "description":
                        result.description = movieChild.GetElementValue();
                        break;
                    case "data":
                        result.genre = movieChild.GetAttributeInteger( "genre" );
                        result.price = movieChild.GetAttributeInteger( "price" );
                        result.topicality = movieChild.GetAttributeInteger( "topicality" );
                        break;
                }
            }

            return result;
        }

        public List<NewsOldV2> LoadNews( XmlNode xmlNode, bool isFake )
        {
            var result = new List<NewsOldV2>();
            NewsOldV2 currentMovie = LoadNewsBase( xmlNode, isFake );
            result.Add( currentMovie );

            foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
            {
                switch ( movieChild.Name )
                {
                    case "episode":
                        var episode = LoadNewsBase( movieChild, isFake );
                        episode.episode = movieChild.GetAttributeInteger( "number" );
                        episode.parentID = currentMovie.id;
                        result.Add( episode );
                        break;
                }
            }
            return result;
        }

        public AdvertisingOldV2 LoadAd( XmlNode xmlNode, bool isFake )
        {
            var result = new AdvertisingOldV2();
            result.id = TempIdCounter++;

            foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
            {
                switch ( movieChild.Name )
                {
                    case "title":
                        if ( isFake )
                            result.titleFake = movieChild.GetElementValue();
                        else
                            result.title = movieChild.GetElementValue();
                        break;
                        break;
                    case "description":
                        result.description = movieChild.GetElementValue();
                        break;
                    case "data":
                        result.minAudience = movieChild.GetAttributeInteger( "minaudience" );
                        result.minImage = movieChild.GetAttributeInteger( "minimage" );
                        result.repetitions = movieChild.GetAttributeInteger( "repetitions" );
                        result.profit = movieChild.GetAttributeInteger( "profit" );
                        result.penalty = movieChild.GetAttributeInteger( "penalty" );
                        result.targetgroup = movieChild.GetAttributeInteger( "targetgroup" );
                        result.duration = movieChild.GetAttributeInteger( "duration" );
                        break;
                }
            }

            return result;
        }

        public ITVTDatabase LoadXML( string filename, ITVTDatabase database, TVTDataRoot dataRoot )
        {
            var result = database;
            //int version = 0;
            //DatabaseVersion dbVersion = DatabaseVersion.V2;
            //TVTDataContent defaultType = TVTDataContent.FakeWithRefId;

            var doc = new XmlDocument();

            doc.Load( filename );

            var versionElement = doc.GetElementsByTagName( "version" );
            if ( versionElement[0].HasAttribute( "value" ) )
            {
                var version = versionElement[0].GetAttributeInteger( "value" );
                if ( version != 2 )
                    throw new NotSupportedException( "Only database version '2' is supported." );
            }

            {
                var movies = new List<MovieOldV2>();
                var allMovies = doc.GetElementsByTagName( "allmovies" );

                foreach ( XmlNode xmlMovie in allMovies )
                {
                    foreach ( XmlNode childNode in xmlMovie.ChildNodes )
                    {
                        switch ( childNode.Name )
                        {
                            case "movie":
                                movies.AddRange( LoadMovie( childNode, true ) );
                                break;
                            default:
                                throw new NotSupportedException( "Only 'movie'-tags are supported." );
                        }
                    }
                }
                OldV2Converter.Convert( movies, database, dataRoot );
            }

            {
                var series = new List<MovieOldV2>();
                var allSeries = doc.GetElementsByTagName( "allseries" );

                foreach ( XmlNode xmlSeries in allSeries )
                {
                    foreach ( XmlNode childNode in xmlSeries.ChildNodes )
                    {
                        switch ( childNode.Name )
                        {
                            case "serie":
                                series.AddRange( LoadMovie( childNode, true ) );
                                break;
                            default:
                                throw new NotSupportedException( "Only 'serie'-tags are supported." );
                        }
                    }
                }
                OldV2Converter.Convert( series, database, dataRoot );
            }

            {
                var news = new List<NewsOldV2>();
                var allNews = doc.GetElementsByTagName( "allnews" );

                foreach ( XmlNode xmlNews in allNews )
                {
                    foreach ( XmlNode childNode in xmlNews.ChildNodes )
                    {
                        switch ( childNode.Name )
                        {
                            case "news":
                                news.AddRange( LoadNews( childNode, true ) );
                                break;
                            default:
                                throw new NotSupportedException( "Only 'news'-tags are supported." );
                        }
                    }
                }
                OldV2Converter.Convert( news, database, dataRoot );
            }

            {
                var ads = new List<AdvertisingOldV2>();
                var allNews = doc.GetElementsByTagName( "allads" );

                foreach ( XmlNode xmlNews in allNews )
                {
                    foreach ( XmlNode childNode in xmlNews.ChildNodes )
                    {
                        switch ( childNode.Name )
                        {
                            case "ad":
                                ads.Add( LoadAd( childNode, true ) );
                                break;
                            default:
                                throw new NotSupportedException( "Only 'news'-tags are supported." );
                        }
                    }
                }
                OldV2Converter.Convert( ads, database, dataRoot );
            }


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









        //private void ConvertOldMovieData( TVTProgramme movie, int version )
        //{
        //    if ( version <= 2 ) //Alte BlitzMax-Datenbank
        //    {
        //        movie.MovieAdditional.GenreOldVersion = movie.MovieAdditional.MainGenreRaw;
        //        OldV2Converter.ConvertGenreAndFlags( movie, null );
        //    }
        //    else
        //    {
        //        movie.MainGenre = (TVTProgrammeGenre)movie.MovieAdditional.MainGenreRaw;
        //        movie.SubGenre = (TVTProgrammeGenre)movie.MovieAdditional.SubGenreRaw;
        //    }
        //}


    }
}
