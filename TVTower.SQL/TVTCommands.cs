using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using TVTower.Entities;
using TVTower.Entities.Helper;

namespace TVTower.SQL
{
    public class TVTCommands
    {
        public static void LoadMovies(ITVTDatabase database, MySqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM tvt_filme";                        
            var Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                var movie = new TVTMovie();
                movie.TitleDE = Reader["title"].ToString();
                movie.TitleEN = Reader["titleEnglish"].ToString();
                movie.DescriptionDE = Reader["description"].ToString();
                movie.DescriptionEN = Reader["descriptionEnglish"].ToString();
                movie.Actors = database.GetPersonsByNameOrCreate(Reader["actors"].ToString(), TVTDataStatus.Original, TVTPersonFunction.Actor);
                movie.Director = database.GetPersonByNameOrCreate(Reader["director"].ToString(), TVTDataStatus.Original, TVTPersonFunction.Director);
                movie.PriceRate = int.Parse(Reader["price"].ToString());
                movie.Year = int.Parse(Reader["year"].ToString());
                movie.Country = Reader["country"].ToString();
                movie.MainGenre = GetGenre(int.Parse(Reader["genre"].ToString()));
                movie.Blocks = int.Parse(Reader["blocks"].ToString());
                movie.CriticsRate = int.Parse(Reader["critics"].ToString());
                movie.BoxOfficeRate = int.Parse(Reader["outcome"].ToString());
                movie.ViewersRate = int.Parse(Reader["speed"].ToString());
                if (int.Parse(Reader["xrated"].ToString()) != 0)
                {
                    movie.Flags.Add(TVTMovieFlag.FSK18);
                }

                Reader["parentID"].ToString();
                Reader["episode"].ToString();
                Reader["approved"].ToString();
                Reader["time"].ToString();
   
                database.AddMovie(movie);
            }

            var t = 1;
        }

        private static TVTGenre GetGenre(int genreNumber)
        {
            return (TVTGenre)genreNumber;
        }
    }
}
