using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using TVTower.Entities;
using TVTower.Entities.Helper;
using TVTower.Converter;
using CodeKnight.Core;
using System.Reflection;

namespace TVTower.SQL
{

    public class TVTCommandsV3
    {
        private static string GetListToSeperatedString<T>(List<T> list)
        {
            if (list != null)
                return list.ToContentString();
            else
                return null;
        }

        public static void AddEntityBaseSQLDefinition<T>(SQLDefinition<T> definition)
            where T : ITVTNames
        {
            definition.Add(x => x.Id);
            definition.Add(x => x.TitleDE);
            definition.Add(x => x.TitleEN);
            definition.Add(x => x.FakeTitleDE);
            definition.Add(x => x.FakeTitleEN);
            definition.Add(x => x.DescriptionDE);
            definition.Add(x => x.DescriptionEN);
        }

        public static SQLDefinition<TVTProgramme> GetProgrammeSQLDefinition()
        {
            var definition = new SQLDefinition<TVTProgramme>();

            AddEntityBaseSQLDefinition(definition);

            definition.Add(x => x.FakeDescriptionDE);
            definition.Add(x => x.FakeDescriptionEN);

            definition.Add(x => x.ProgrammeType);
            definition.Add(x => x.Country);
            definition.Add(x => x.Year);

            definition.Add(x => x.MainGenre);
            definition.Add(x => x.SubGenre);

            definition.Add(x => x.Blocks);
            definition.Add(x => x.LiveHour);
            definition.Add(x => x.DistributionChannel);

            definition.Add(x => x.Flags);
            definition.Add(x => x.TargetGroups);
            definition.Add(x => x.ProPressureGroups);
            definition.Add(x => x.ContraPressureGroups);

            definition.Add(x => x.ImdbId);
            definition.Add(x => x.TmdbId);
            definition.Add(x => x.RottenTomatoesId);
            definition.Add(x => x.ImageUrl);

            definition.Add(x => x.Director, null, "_id");
            definition.Add(x => x.Participants, "participant1_id", null, 0);
            definition.Add(x => x.Participants, "participant2_id", null, 1);
            definition.Add(x => x.Participants, "participant3_id", null, 2);

            definition.Add(x => x.BettyBonus);
            definition.Add(x => x.PriceMod);
            definition.Add(x => x.CriticsRate);
            definition.Add(x => x.ViewersRate);
            definition.Add(x => x.BoxOfficeRate);

            //Zusatzinfos
            AdditionalFields2(definition);

            return definition;
        }

        public static SQLDefinition<TVTAdvertising> GetAdvertisingSQLDefinition()
        {
            var definition = new SQLDefinition<TVTAdvertising>();

            AddEntityBaseSQLDefinition(definition);

            definition.Add(x => x.FakeDescriptionDE);
            definition.Add(x => x.FakeDescriptionEN);

            definition.Add(x => x.Infomercial);
            definition.Add(x => x.Quality);

            definition.Add(x => x.FlexibleProfit);
            definition.Add(x => x.MinAudience);
            definition.Add(x => x.MinImage);
            definition.Add(x => x.Repetitions);
            definition.Add(x => x.Duration);
            definition.Add(x => x.Profit);
            definition.Add(x => x.Penalty);
            definition.Add(x => x.TargetGroup);

            definition.Add(x => x.AllowedGenres);
            definition.Add(x => x.ProhibitedGenres);
            definition.Add(x => x.AllowedProgrammeTypes);
            definition.Add(x => x.ProhibitedProgrammeTypes);

            definition.Add(x => x.ProPressureGroups);
            definition.Add(x => x.ContraPressureGroups);

            //Zusatzinfos
            AdditionalFields2(definition);

            return definition;
        }

        public static SQLDefinition<TVTPerson> GetPersonSQLDefinition()
        {
            var definition = new SQLDefinition<TVTPerson>();

            //AddEntityBaseSQLDefinition(definition);

            definition.Add(x => x.Id);

            definition.Add(x => x.FirstName);
            definition.Add(x => x.LastName);
            definition.Add(x => x.NickName);

            definition.Add(x => x.FakeFirstName);
            definition.Add(x => x.FakeLastName);
            definition.Add(x => x.FakeNickName);

            definition.Add(x => x.TmdbId);
            definition.Add(x => x.ImdbId);
            definition.Add(x => x.ImageUrl);
            
            definition.Add(new SQLDefinitionFieldList<TVTPersonFunction>(PInfo<TVTPerson>.Info(x => x.Functions, false)));
            //definition.Add(x => x.Functions);
            definition.Add(x => x.Gender);

            definition.Add(x => x.Birthday);
            definition.Add(x => x.Deathday);
            definition.Add(x => x.Country);

            definition.Add(x => x.Prominence);
            definition.Add(x => x.Skill);
            definition.Add(x => x.Fame);
            definition.Add(x => x.Scandalizing);
            definition.Add(x => x.PriceFactor);

            definition.Add(x => x.Power);
            definition.Add(x => x.Humor);
            definition.Add(x => x.Charisma);
            definition.Add(x => x.Appearance);

            definition.Add(x => x.TopGenre1);
            definition.Add(x => x.TopGenre2);

            definition.Add(x => x.ProgrammeCount);

            //Zusatzinfos
            AdditionalFields2(definition);

            return definition;
        }

        private static List<T> ReadGeneric<T>(MySqlConnection connection, string commandText, SQLDefinition<T> definition)
        {
            var result = new List<T>();

            var command = connection.CreateCommand();
            command.CommandText = commandText;
            var Reader = command.ExecuteReader();
            try
            {
                while (Reader.Read())
                {
                    var entity = Activator.CreateInstance<T>();

                    var enumerator = definition.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        var field = enumerator.Current;
                        field.Read(Reader, entity);
                    }

                    result.Add(entity);
                }
            }
            finally
            {
                if (Reader != null && !Reader.IsClosed)
                    Reader.Close();
            }

            return result;
        }

        public static List<TVTProgramme> ReadProgrammes(MySqlConnection connection)
        {
            return ReadGeneric<TVTProgramme>(connection, "SELECT * FROM tvt_programmes", GetProgrammeSQLDefinition());
        }

        public static List<TVTAdvertising> ReadAdvertisings(MySqlConnection connection)
        {
            return ReadGeneric<TVTAdvertising>(connection, "SELECT * FROM tvt_advertisings ORDER BY title_de, fake_title_de", GetAdvertisingSQLDefinition());
        }

        public static List<TVTPerson> ReadPeople(MySqlConnection connection)
        {
            return ReadGeneric<TVTPerson>(connection, "SELECT * FROM tvt_people", GetPersonSQLDefinition());
        }

        private static void AdditionalFields2<T>(SQLDefinition<T> definition)
            where T : TVTEntity
        {
            definition.Add(x => x.DataType);
            definition.Add(x => x.DataStatus);
            definition.Add(x => x.Approved);
            definition.Add(x => x.AdditionalInfo);

            definition.Add(x => x.CreatorId);
            definition.Add(x => x.EditorId);
        }

        public static void InsertProgrammes2(MySqlConnection connection, IEnumerable<TVTProgramme> programmes)
        {
            var definition = GetProgrammeSQLDefinition();

            var sqlCommandText = "INSERT INTO tvt_programmes (" + definition.GetFieldNames(',') + ") VALUES (" + definition.GetFieldNames(',', "?") + ")";

            foreach (var programme in programmes)
            {
                var command = connection.CreateCommand();
                command.CommandText = sqlCommandText;
                var enumerator = definition.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var field = enumerator.Current;
                    command.Parameters.AddWithValue("?" + field.FieldName, field.GetValue(programme));
                }
                command.ExecuteNonQuery();
            }            
        }

        //public static void InsertProgrammes(MySqlConnection connection, IEnumerable<TVTProgramme> programmes)
        //{
        //    foreach (var programme in programmes)
        //    {
        //        var content = new Dictionary<string, object>();
        //        content.Add("id", programme.Id);

        //        content.Add("programme_type", programme.ProgrammeType);
        //        content.Add("country", programme.Country);
        //        content.Add("year", programme.Year);

        //        content.Add("main_genre", programme.MainGenre);
        //        content.Add("sub_genre", programme.SubGenre);

        //        content.Add("blocks", programme.Blocks);
        //        content.Add("live_hour", programme.LiveHour);
        //        content.Add("distribution_channel", programme.DistributionChannel);                

        //        content.Add("flags", programme.Flags.ToContentString()); //TODO
        //        content.Add("target_groups", programme.TargetGroups.ToContentString());
        //        content.Add("pro_pressure_groups", GetListToSeperatedString(programme.ProPressureGroups));
        //        content.Add("contra_pressure_groups", GetListToSeperatedString(programme.ContraPressureGroups));
                
        //        content.Add("imdb_id", programme.ImdbId);
        //        content.Add("tmdb_id", programme.TmdbId);
        //        content.Add("rotten_tomatoes_id", programme.RottenTomatoesId);

        //        content.Add("image_url", programme.ImageUrl);


        //        //Episode                
        //        content.Add("title_de", programme.TitleDE);
        //        content.Add("title_en", programme.TitleEN);
        //        content.Add("fake_de", programme.FakeTitleDE);
        //        content.Add("fake_en", programme.FakeTitleEN);

        //        content.Add("description_de", programme.DescriptionDE);
        //        content.Add("description_en", programme.DescriptionEN);
        //        content.Add("fake_description_de", programme.FakeDescriptionDE);
        //        content.Add("fake_description_en", programme.FakeDescriptionEN);

        //        content.Add("director_id", programme.Director != null ? programme.Director.Id.ToString() : null);

        //        if (programme.Participants != null && programme.Participants.Count > 0)
        //        {
        //            for (var i = 0; i < programme.Participants.Count; i++)
        //            {
        //                if (i >= 3)
        //                    break;

        //                var participant = programme.Participants[i];
        //                if (participant != null)
        //                    content.Add("participant" + (i + 1) + "_id", participant.Id);
        //            }
        //        }

        //        content.Add("betty", programme.BettyBonus);
        //        content.Add("price", programme.PriceMod);
        //        content.Add("critics", programme.CriticsRate);
        //        content.Add("viewers", programme.ViewersRate);
        //        content.Add("boxoffice", programme.BoxOfficeRate);


        //        //Zusatzinfos
        //        AdditionalFields(content, programme);


        //        var command = connection.CreateCommand();
                
        //        command.CommandText = "INSERT INTO tvt_movies (" + content.Keys.ToContentString(',') + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(',') + ")";
        //        foreach (var kvp in content)
        //        {
        //            command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
        //        }
                
        //        command.ExecuteNonQuery();
        //    }
        //}

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

                command.CommandText = "INSERT INTO tvt_episodes (" + content.Keys.ToContentString(',') + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(',') + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        public static void InsertPeople2(MySqlConnection connection, IEnumerable<TVTPerson> people)
        {
            var definition = GetPersonSQLDefinition();

            var sqlCommandText = "INSERT INTO tvt_people (" + definition.GetFieldNames(',') + ") VALUES (" + definition.GetFieldNames(',', "?") + ")";

            foreach (var person in people)
            {
                var command = connection.CreateCommand();
                command.CommandText = sqlCommandText;
                var enumerator = definition.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var field = enumerator.Current;
                    command.Parameters.AddWithValue("?" + field.FieldName, field.GetValue(person));
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

                command.CommandText = "INSERT INTO tvt_people (" + content.Keys.ToContentString(',') + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(',') + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        

            public static void InsertAdvertisings2(MySqlConnection connection, IEnumerable<TVTAdvertising> advertisings)
            {
                var definition = GetAdvertisingSQLDefinition();

                var sqlCommandText = "INSERT INTO tvt_advertisings (" + definition.GetFieldNames(',') + ") VALUES (" + definition.GetFieldNames(',', "?") + ")";

                foreach (var advertising in advertisings)
                {
                    var command = connection.CreateCommand();
                    command.CommandText = sqlCommandText;
                    var enumerator = definition.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        var field = enumerator.Current;
                        command.Parameters.AddWithValue("?" + field.FieldName, field.GetValue(advertising));
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

                command.CommandText = "INSERT INTO tvt_advertisings (" + content.Keys.ToContentString(',') + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(',') + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }

                command.ExecuteNonQuery();
            }
        }

        public static void InsertNews(MySqlConnection connection, IEnumerable<TVTNews> newsEnum)
        {
            foreach (var news in newsEnum)
            {
                var content = new Dictionary<string, object>();
                content.Add("id", news.Id);
                //Episode                
                content.Add("title_de", news.TitleDE);
                content.Add("title_en", news.TitleEN);
                content.Add("description_de", news.DescriptionDE);
                content.Add("description_en", news.DescriptionEN);

                content.Add("type", news.NewsType);
                content.Add("handling", news.NewsHandling);
                content.Add("thread_id", news.NewsThreadId);
                content.Add("genre", news.Genre);
                content.Add("predecessor", news.Predecessor != null ? news.Predecessor.Id.ToString() : null);

                content.Add("price", news.Price);
                content.Add("topicality", news.Topicality);

                content.Add("fix_year", news.FixYear);
                content.Add("available_after_days", news.AvailableAfterXDays);
                content.Add("year_range_from", news.YearRangeFrom);
                content.Add("year_range_to", news.YearRangeTo);
                content.Add("hours_after_predecessor", news.MinHoursAfterPredecessor);
                content.Add("time_range_from", news.TimeRangeFrom);
                content.Add("time_range_to", news.TimeRangeTo);

                content.Add("resource_1_type", news.Resource1Type);
                content.Add("resource_2_type", news.Resource2Type);
                content.Add("resource_3_type", news.Resource3Type);
                content.Add("resource_4_type", news.Resource4Type);

                content.Add("effect", news.Effect);
                content.Add("effect_parameters", news.EffectParameters.ToContentString());

                if (news.ProPressureGroups != null)
                    content.Add("pro_pressure_groups", news.ProPressureGroups.ToContentString());
                else
                    content.Add("pro_pressure_groups", null);

                if (news.ContraPressureGroups != null)
                    content.Add("contra_pressure_groups", news.ContraPressureGroups.ToContentString());
                else
                    content.Add("contra_pressure_groups", null);


                //Zusatzinfos
                AdditionalFields(content, news);


                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO tvt_news (" + content.Keys.ToContentString(',') + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(',') + ")";
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

            content.Add("creator_id", entity.CreatorId);
            content.Add("editor_id", entity.EditorId);
        }
    }
}
