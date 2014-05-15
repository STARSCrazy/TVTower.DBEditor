using System.Collections.Generic;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTSeriesMoviePersister : TVTMoviePersister
	{
		TVTDataContent defaultStatus = TVTDataContent.Fake;

		public override void Load( XmlNode xmlNode, TVTProgramme movie, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			base.Load( xmlNode, movie, database, dbVersion, dataStructure );

            movie.ProgrammeType = TVTProgrammeType.Series;
			movie.Episodes = new List<TVTEpisode>();

			var episodePersister = new TVTEpisodePersister<TVTEpisode>();

			foreach ( XmlLinkedNode movieChild in xmlNode.ChildNodes )
			{
				switch ( movieChild.Name )
				{
					case "episode":
						var episode = new TVTEpisode();
						episode.EpisodeIndex = movieChild.GetAttributeInteger( "index" );
						episodePersister.Load( movieChild, episode, database, dbVersion, dataStructure );
						movie.Episodes.Add( episode );
						break;
				}
			}
		}
	}
}
;