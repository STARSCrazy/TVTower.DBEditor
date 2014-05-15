using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using TVTower.Entities;
using TVTower.Entities.Helper;
using TVTower.Converter;

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
            }

            return result;
        }

        private static TVTGenre GetGenreConvert(int genreNumber)
        {
            return (TVTGenre)genreNumber;
        }
    }
}
