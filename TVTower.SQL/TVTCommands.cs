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

            return result;
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
                content.Add("valid_until", programme.ValidUntilYear);

                content.Add("main_genre", programme.MainGenre);
                content.Add("sub_genre", programme.SubGenre);

                content.Add("blocks", programme.Blocks);
                content.Add("live_hour", programme.LiveHour);

                content.Add("flags", programme.Flags.ToString()); //TODO
                content.Add("target_groups", programme.TargetGroups.ToString());
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

                content.Add("Image_url", programme.ImageUrl);


                //Episode                
                content.Add("title_de", programme.TitleDE);
                content.Add("title_en", programme.TitleEN);
                content.Add("fake_de", programme.FakeTitleDE);
                content.Add("fake_en", programme.FakeTitleEN);

                content.Add("description_de", programme.DescriptionDE);
                content.Add("description_en", programme.DescriptionEN);

                content.Add("episodeIndex", programme.EpisodeIndex);
                if (programme.SeriesMaster != null && programme.SeriesMaster.IsAlive)
                    content.Add("series_id", programme.SeriesMaster.TargetGeneric.Id);
                else
                    content.Add("series_id", null);

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
                content.Add("dataType", programme.DataType);
                content.Add("dataContent", programme.DataContent);
                content.Add("dataStatusDE", programme.DataStatusDE);
                content.Add("dataStatusEN", programme.DataStatusEN);
                content.Add("approvedDE", programme.ApprovedDE);
                content.Add("approvedEN", programme.ApprovedEN);
                content.Add("incorrect", programme.Incorrect);
                content.Add("additionalInfo", programme.AdditionalInfo);


                var command = connection.CreateCommand();
                
                command.CommandText = "INSERT INTO tvt_movies (" + content.Keys.ToContentString(",") + ") VALUES (" + content.ForEach(x => "?" + x, null).Keys.ToContentString(",") + ")";
                foreach (var kvp in content)
                {
                    command.Parameters.AddWithValue("?" + kvp.Key, kvp.Value);
                }
                
                command.ExecuteNonQuery();
            }
        }
    }
}
