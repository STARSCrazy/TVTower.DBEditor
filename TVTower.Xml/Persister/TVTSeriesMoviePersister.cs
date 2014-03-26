using System.Collections.Generic;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTSeriesMoviePersister : TVTMoviePersister
	{
		TVTDataStatus defaultStatus = TVTDataStatus.Fake;

		public override void Load( XmlNode xmlNode, TVTMovie movie, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			base.Load( xmlNode, movie, database, dbVersion, dataStructure );

			movie.IsSeries = true;
			movie.Episodes = new List<TVTEpisode>();

			var episodePersister = new TVTEpisodePersister<TVTEpisode>();

			foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
			{
				switch ( movieChild.Name )
				{
					case "episode":
						var episode = new TVTEpisode();
						episode.Name = new TVTNameAndDescription();
						episode.EpisodeNumber = movieChild.GetAttributeInteger( "number" );
						episodePersister.Load( movieChild, episode, database, dbVersion, dataStructure );
						movie.Episodes.Add( episode );
						break;
				}
			}
		}
	}
}
;