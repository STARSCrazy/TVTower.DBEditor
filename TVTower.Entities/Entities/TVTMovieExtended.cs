
namespace TVTower.Entities
{
	public class TVTMovieExtended : TVTMovie
	{
		public TVTMovieExtended()
			: base()
		{
		}

		public string OriginalTitleDE { get; set; }

		public string OriginalTitleEN { get; set; }

		public string DescriptionMovieDB { get; set; }


		public int MainGenreRaw { get; set; }

		public int SubGenreRaw { get; set; }

		public int GenreOldVersion { get; set; }


		public int PriceRateOld { get; set; }

		public int CriticRateOld { get; set; }

		public int SpeedRateOld { get; set; }

		public int BoxOfficeRateOld { get; set; }


		public long Budget { get; set; }

		public long Revenue { get; set; }
	}
}
