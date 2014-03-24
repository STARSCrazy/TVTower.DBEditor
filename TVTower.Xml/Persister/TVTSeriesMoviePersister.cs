using System.Collections.Generic;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTSeriesMoviePersister<T> : TVTMoviePersister<T>
		where T : TVTMovie
	{
		TVTDataStatus defaultStatus = TVTDataStatus.Fake;

		public override void Load( XmlNode xmlNode, T movie, ITVTDatabase database )
		{
			base.Load( xmlNode, movie, database );

			movie.IsSeries = true;
			movie.Episodes = new List<TVTEpisode>();

			var episodePersister = new TVTEpisodePersister<TVTEpisode>();

			foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
			{
				switch ( movieChild.Name )
				{
					case "episode":
						var episode = new TVTEpisode();
						episode.EpisodeNumber = movieChild.GetAttributeInteger( "number" );
						episodePersister.Load( movieChild, episode, database );
						movie.Episodes.Add( episode );
						break;
				}
			}
		}
	}
}
;