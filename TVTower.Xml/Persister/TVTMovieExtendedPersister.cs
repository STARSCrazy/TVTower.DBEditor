using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTMovieExtendedPersister<T> : TVTMoviePersister<T>
		where T : TVTMovieExtended
	{
		public override void Load( XmlNode xmlNode, T movieExt, ITVTDatabase database )
		{
			base.Load( xmlNode, movieExt, database );

			foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
			{
				switch ( movieChild.Name )
				{
					case "original_title":
						movieExt.OriginalTitleDE = movieChild.GetElementValue();
						break;
					case "original_title_de":
						movieExt.OriginalTitleDE = movieChild.GetElementValue();
						break;
					case "original_title_en":
						movieExt.OriginalTitleEN = movieChild.GetElementValue();
						break;

					case "description_tmdb":
						movieExt.DescriptionMovieDB = movieChild.GetElementValue();
						break;

					case "data":
						movieExt.MainGenreRaw = movieChild.GetAttributeInteger( "genre" );
						movieExt.SubGenreRaw = movieChild.GetAttributeInteger( "subgenre" );
						break;

					//TODO
					//public int PriceRateOld { get; set; }
					//public int CriticRateOld { get; set; }
					//public int SpeedRateOld { get; set; }
					//public int BoxOfficeRateOld { get; set; }


					//public long Budget { get; set; }
					//public long Revenue { get; set; }
				}
			}
		}
	}
}
;