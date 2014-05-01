using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVTower.Importer.MadTV
{
    public static class MadTVMovieImporter
    {
        public static void FillMovieFromBuffer(byte[] buffer, MadTVMovie movie = null)
        {
            if (movie == null)
                movie = new MadTVMovie();

            movie.Genre = buffer[0];
            movie.Pause = buffer[2];
            movie.Blocks = buffer[4];

            var gradeTemp = (int)buffer[6];
            if (gradeTemp >= 64)
            {
                movie.Auction = true;
                gradeTemp = gradeTemp - 64;
            }
            movie.Quality = gradeTemp - 31;

            movie.XRated = (buffer[7] == 8);

            movie.Country = buffer[19];
            movie.Year = buffer[20];
            movie.Critics = buffer[21];
            movie.Box = buffer[22];
        }

        public static bool ValidBuffer(byte[] buffer)
        {
            if (!ValidBetween(buffer[0], 1, 10))
                return false;
            if (!ValidBetween(buffer[2], 0, 5)) //Eigentlich 1-5
                return false;
            if (!ValidBetween(buffer[4], 0, 7)) //Eigentlich 1-7
                return false;
            if (!ValidBetween(buffer[6], 0, 127)) //Eigentlich 32-127
                return false;
            if (!ValidBetween(buffer[7], 0, 8))
                return false;
            if (!ValidBetween(buffer[12], 0, 0))
                return false;
            if (!ValidBetween(buffer[13], 0, 0))
                return false;
            if (!ValidBetween(buffer[14], 0, 0))
                return false;
            if (!ValidBetween(buffer[15], 0, 0))
                return false;
            if (!ValidBetween(buffer[16], 0, 0))
                return false;
            if (!ValidBetween(buffer[17], 0, 0))
                return false;
            if (!ValidBetween(buffer[18], 0, 0))
                return false;
            if (!ValidBetween(buffer[19], 0, 9))
                return false;
            if (!ValidBetween(buffer[20], 0, 99))
                return false;
            if (!ValidBetween(buffer[21], 0, 10))
                return false;
            if (!ValidBetween(buffer[22], 0, 10))
                return false;

            return true;
        }

        private static bool ValidBetween(int value, int min, int max)
        {
            return (value >= min && value <= max);
        }
    }
}
