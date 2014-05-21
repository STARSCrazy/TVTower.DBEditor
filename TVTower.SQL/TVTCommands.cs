using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using TVTower.Entities;
using TVTower.Entities.Helper;
using TVTower.Converter;
using CodeKnight.Core;

namespace TVTower.SQL
{
    public class TVTCommands
    {
        public static List<MovieOldV2> LoadMoviesOldV2(MySqlConnection connection)
        {
            var result = new List<MovieOldV2>();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvt_filme";
            var Reader = command.ExecuteReader();
            try
            {                
                while (Reader.Read())
                {
                    var reader = new SQLReaderOldV2(Reader);
                    var movie = new MovieOldV2();

                    movie.id = reader.GetInt("id");
                    movie.title = reader.GetString("title");
                    movie.titleEnglish = reader.GetString("titleEnglish");
                    movie.description = reader.GetString("description");
                    movie.descriptionEnglish = reader.GetString("descriptionEnglish");
                    movie.actors = reader.GetString("actors");
                    movie.director = reader.GetString("director");
                    movie.price = reader.GetInt("price");
                    movie.year = reader.GetInt("year");
                    movie.country = reader.GetString("country");
                    movie.genre = reader.GetInt("genre");
                    movie.blocks = reader.GetInt("blocks");
                    movie.critics = reader.GetInt("critics");
                    movie.outcome = reader.GetInt("outcome");
                    movie.speed = reader.GetInt("speed");
                    movie.xrated = reader.GetBool("xrated");
                    movie.parentID = reader.GetInt("parentID");
                    movie.episode = reader.GetInt("episode");
                    movie.approved = reader.GetBool("approved");
                    movie.time = reader.GetInt("time");
                    movie.creatorID = reader.GetString("creatorID");
                    movie.editorID = reader.GetString("editorID");
                    movie.deleted = reader.GetBool("deleted");
                    movie.custom = reader.GetBool("custom");

                    result.Add(movie);
                }
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                    Reader.Close();
            }

            //var fakes = new Dictionary<string, string>();

            //Fakes
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvt_convert WHERE type = 'title'";
            Reader = command.ExecuteReader();
            try
            {
                while (Reader.Read())
                {
                    var oldValue = Reader.GetString("old");
                    var newValue = Reader.GetString("new");

                    var foundMovies = result.Where(x => x.title == oldValue);
                    foreach (var movie in foundMovies)
                    {
                        movie.titleFake = newValue;
                    }

                    foundMovies = result.Where(x => x.titleEnglish == oldValue);
                    foreach (var movie in foundMovies)
                    {
                        movie.titleEnglishFake = newValue;
                    }

                    foundMovies = result.Where(x => string.IsNullOrEmpty(x.titleFake) && x.title.Contains(oldValue));
                    foreach (var movie in foundMovies)
                    {
                        movie.titleFake = movie.title.Replace(oldValue, newValue);
                    }

                    foundMovies = result.Where(x => string.IsNullOrEmpty(x.titleEnglishFake) && x.titleEnglish.Contains(oldValue));
                    foreach (var movie in foundMovies)
                    {
                        movie.titleEnglishFake = movie.titleEnglish.Replace(oldValue, newValue);
                    }
                }
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                    Reader.Close();
            }

            return result;
        }

        public static List<AdvertisingOldV2> LoadAdsOldV2(MySqlConnection connection)
        {
            var result = new List<AdvertisingOldV2>();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvtower.tvt_werbevertraege";
            var Reader = command.ExecuteReader();
            try
            {
                while (Reader.Read())
                {
                    var reader = new SQLReaderOldV2(Reader);
                    var ad = new AdvertisingOldV2();

                    ad.id = reader.GetInt("id");
                    ad.title = reader.GetString("title");
                    ad.titleEnglish = reader.GetString("titleEnglish");
                    ad.description = reader.GetString("description");
                    ad.descriptionEnglish = reader.GetString("descriptionEnglish");

                    ad.minAudience = reader.GetInt("minAudience");
                    ad.minImage = reader.GetInt("minImage");
                    ad.repetitions = reader.GetInt("repetitions");
                    ad.fixedPrice = reader.GetInt("fixedPrice");
                    ad.fixedProfit = reader.GetInt("fixedProfit");
                    ad.fixedPenalty = reader.GetInt("fixedPenalty");
                    ad.profit = reader.GetInt("profit");
                    ad.penalty = reader.GetInt("penalty");
                    ad.targetgroup = reader.GetInt("targetgroup");
                    ad.duration = reader.GetInt("duration");
                    ad.approved = reader.GetBool("approved");
                    ad.creatorID = reader.GetInt("creatorID");
                    ad.editorID = reader.GetInt("editorID");
                    ad.custom = reader.GetBool("custom");
                    ad.deleted = reader.GetBool("deleted");

                    result.Add(ad);
                }
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                    Reader.Close();
            }

            return result;
        }

        public static void LoadFakesForPeople(MySqlConnection connection, IEnumerable<TVTPerson> people)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvt_convert WHERE type = 'actor'";
            var Reader = command.ExecuteReader();
            try
            {
                while (Reader.Read())
                {
                    var oldValue = Reader.GetString("old");
                    var newValue = Reader.GetString("new");

                    var foundPeople = people.Where(x => x.FullName == oldValue);
                    foreach (var person in foundPeople)
                    {
                        PersonConverter.ConvertFakeFullname(person, newValue);
                    }

                    foundPeople = people.Where(x => x.FakeFullName == " " && x.FullName.Contains(oldValue));
                    foreach (var person in foundPeople)
                    {
                        PersonConverter.ConvertFakeFullname(person, newValue);
                    }
                }
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                    Reader.Close();
            }            
        }

        public static void InsertProgrammes(MySqlConnection connection, IEnumerable<TVTProgramme> programmes)
        {
            foreach (var programme in programmes)
            {
                var content = new Dictionary<string, object>();
                content.Add("id", programme.Id);

                content.Add("programme_type", programme.ProgrammeType);
                content.Add("country", programme.Country);
                content.Add("year", programme.Year);

                content.Add("main_genre", programme.MainGenre);
                content.Add("sub_genre", programme.SubGenre);

                content.Add("blocks", programme.Blocks);
                content.Add("live_hour", programme.LiveHour);
                content.Add("distribution_channel", programme.DistributionChannel);                

                content.Add("flags", programme.Flags.ToContentString()); //TODO
                content.Add("target_groups", programme.TargetGroups.ToContentString());
                if (programme.ProPressureGroups != null)
                    content.Add("pro_pressure_groups", programme.ProPressureGroups.ToContentString());
                else
                    content.Add("pro_pressure_groups", null);

                if (programme.ContraPressureGroups != null)
                    content.Add("contra_pressure_groups", programme.ContraPressureGroups.ToContentString());
                else
                    content.Add("contra_pressure_groups", null);

                content.Add("imdb_id", programme.ImdbId);
                content.Add("tmdb_id", programme.TmdbId);
                content.Add("rotten_tomatoes_id", programme.RottenTomatoesId);

                content.Add("image_url", programme.ImageUrl);


                //Episode                
                content.Add("title_de", programme.TitleDE);
                content.Add("title_en", programme.TitleEN);
                content.Add("fake_de", programme.FakeTitleDE);
                content.Add("fake_en", programme.FakeTitleEN);

                content.Add("description_de", programme.DescriptionDE);
                content.Add("description_en", programme.DescriptionEN);
                content.Add("fake_description_de", programme.FakeDescriptionDE);
                content.Add("fake_description_en", programme.FakeDescriptionEN);

                //content.Add("episodeIndex", programme.EpisodeIndex);
                //if (programme.SeriesMaster != null && programme.SeriesMaster.IsAlive)
                //    content.Add("series_id", programme.SeriesMaster.TargetGeneric.Id);
                //else
                //    content.Add("series_id", null);

                content.Add("director_id", programme.Director != null ? programme.Director.Id.ToString() : null);

                if (programme.Participants != null && programme.Participants.Count > 0)
                {
                    for (var i = 0; i < programme.Participants.Count; i++)
                    {
                        if (i >= 3)
                            break;

                        var participant = programme.Participants[i];
                        if (participant != null)
                            content.Add("participant" + (i + 1) + "_id", participant.Id);
                    }
                }

                content.Add("betty", programme.BettyBonus);
                content.Add("price", programme.PriceMod);
                content.Add("critics", programme.CriticsRate);
                content.Add("viewers", programme.ViewersRate);
                content.Add("boxoffice", programme.BoxOfficeRate);



                //Zusatzinfos
                AdditionalFields(content, programme);


                var command = connection.CreateCommand();
                
                command.CommandText = "INSERT INTO tvt_movies (" + content.Keys.ToContentString(",") + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(",") + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }
                
                command.ExecuteNonQuery();
            }
        }

        public static void InsertEpisodes(MySqlConnection connection, IEnumerable<TVTEpisode> episodes)
        {
            foreach (var episode in episodes)
            {
                var content = new Dictionary<string, object>();
                content.Add("id", episode.Id);

                //Episode                
                content.Add("title_de", episode.TitleDE);
                content.Add("title_en", episode.TitleEN);
                content.Add("fake_de", episode.FakeTitleDE);
                content.Add("fake_en", episode.FakeTitleEN);

                content.Add("description_de", episode.DescriptionDE);
                content.Add("description_en", episode.DescriptionEN);
                
                if (episode.SeriesMaster != null && episode.SeriesMaster.IsAlive)
                    content.Add("series_id", episode.SeriesMaster.TargetGeneric.Id);
                else
                    content.Add("series_id", -1);
                content.Add("episode_index", episode.EpisodeIndex);

                content.Add("director_id", episode.Director != null ? episode.Director.Id.ToString() : null);

                if (episode.Participants != null && episode.Participants.Count > 0)
                {
                    for (var i = 0; i < episode.Participants.Count; i++)
                    {
                        if (i >= 3)
                            break;

                        var participant = episode.Participants[i];
                        if (participant != null)
                            content.Add("participant" + (i + 1) + "_id", participant.Id);
                    }
                }

                content.Add("critics", episode.CriticsRate);
                content.Add("viewers", episode.ViewersRate);

                //Zusatzinfos
                AdditionalFields(content, episode);


                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO tvt_episodes (" + content.Keys.ToContentString(",") + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(",") + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        public static void InsertPeople(MySqlConnection connection, IEnumerable<TVTPerson> people)
        {
            foreach (var person in people)
            {
                var content = new Dictionary<string, object>();
                content.Add("id", person.Id);

                //Episode                
                content.Add("first_name", person.FirstName);
                content.Add("last_name", person.LastName);
                content.Add("nick_name", person.NickName);

                content.Add("fake_first_name", person.FakeFirstName);
                content.Add("fake_last_name", person.FakeLastName);
                content.Add("fake_nick_name", person.FakeNickName);

                content.Add("imdb_id", person.ImdbId);
                content.Add("tmdb_id", person.TmdbId);
                content.Add("image_url", person.ImageUrl);

                if (person.Functions != null)
                    content.Add("functions", person.Functions.ToContentString());
                else
                    content.Add("functions", null);

                content.Add("birthday", person.Birthday);
                content.Add("deathday", person.Deathday);                
                content.Add("country", person.Country);

                content.Add("fame", person.Fame);
                content.Add("price_factor", person.PriceFactor);

                content.Add("skill", person.Skill);
                content.Add("power", person.Power);
                content.Add("humor", person.Humor);
                content.Add("charisma", person.Charisma);
                content.Add("appearance", person.Appearance);

                content.Add("top_genre_1", person.TopGenre1);
                content.Add("top_genre_2", person.TopGenre2);

                content.Add("programme_count", person.ProgrammeCount);

 
                //Zusatzinfos
                AdditionalFields(content, person);


                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO tvt_people (" + content.Keys.ToContentString(",") + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(",") + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        public static void InsertAdvertisings(MySqlConnection connection, IEnumerable<TVTAdvertising> advertisings)
        {
            foreach (var ad in advertisings)
            {
                var content = new Dictionary<string, object>();
                content.Add("id", ad.Id);
                //Episode                
                content.Add("title_de", ad.TitleDE);
                content.Add("title_en", ad.TitleEN);
                content.Add("fake_de", ad.FakeTitleDE);
                content.Add("fake_en", ad.FakeTitleEN);

                content.Add("description_de", ad.DescriptionDE);
                content.Add("description_en", ad.DescriptionEN);

                content.Add("infomercial", ad.Infomercial);
                content.Add("quality", ad.Quality);

                content.Add("flexible_profit", ad.FlexibleProfit);
                content.Add("min_audience", ad.MinAudience);
                content.Add("min_image", ad.MinImage);
                content.Add("repetitions", ad.Repetitions);
                content.Add("duration", ad.Duration);
                content.Add("profit", ad.Profit);
                content.Add("penalty", ad.Penalty);
                content.Add("target_group", ad.TargetGroup);


                if (ad.AllowedGenres != null)
                    content.Add("allowed_genres", ad.AllowedGenres.ToContentString());
                else
                    content.Add("allowed_genres", null);

                if (ad.ProhibitedGenres != null)
                    content.Add("prohibited_genres", ad.ProhibitedGenres.ToContentString());
                else
                    content.Add("prohibited_genres", null);


                if (ad.AllowedProgrammeTypes != null)
                    content.Add("allowed_programme_types", ad.AllowedProgrammeTypes.ToContentString());
                else
                    content.Add("allowed_programme_types", null);

                if (ad.ProhibitedProgrammeTypes != null)
                    content.Add("prohibited_programme_types", ad.ProhibitedProgrammeTypes.ToContentString());
                else
                    content.Add("prohibited_programme_types", null);


                if (ad.ProPressureGroups != null)
                    content.Add("pro_pressure_groups", ad.ProPressureGroups.ToContentString());
                else
                    content.Add("pro_pressure_groups", null);

                if (ad.ContraPressureGroups != null)
                    content.Add("contra_pressure_groups", ad.ContraPressureGroups.ToContentString());
                else
                    content.Add("contra_pressure_groups", null);


                //Zusatzinfos
                AdditionalFields(content, ad);


                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO tvt_advertisings (" + content.Keys.ToContentString(",") + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(",") + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        private static void AdditionalFields(Dictionary<string, object> content, TVTEntity entity)
        {
            content.Add("data_type", entity.DataType);
            content.Add("data_status", entity.DataStatus);
            content.Add("approved", entity.Approved);
            content.Add("additional_info", entity.AdditionalInfo);
        }
    }
}
