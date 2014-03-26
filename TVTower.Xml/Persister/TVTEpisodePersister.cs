using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTEpisodePersister<T> : TVTEntityPersister<T>
		where T : TVTEpisode
	{
		public override void Load( XmlNode xmlNode, T episode, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			base.Load( xmlNode, episode, database, dbVersion, dataStructure );

			var nameParser = new TVTNameAndDescriptionPersister();
			nameParser.Load( xmlNode, episode.Name, database, dbVersion, dataStructure );

			foreach ( var movieChild in xmlNode.ChildNodes )
			{
				if ( movieChild is XmlLinkedNode )
				{
					var linkedNode = (XmlLinkedNode)movieChild;

					switch ( linkedNode.Name )
					{
						case "data":
							episode.BettyBonus = linkedNode.GetAttributeInteger( "betty" );
							episode.PriceRate = linkedNode.GetAttributeInteger( "price" );
							episode.CriticsRate = linkedNode.GetAttributeInteger( "critics" );
							episode.ViewersRate = linkedNode.GetAttributeInteger( "speed" );
							episode.BoxOfficeRate = linkedNode.GetAttributeInteger( "outcome" );
							break;
					}
				}
			}
		}

		public override void Save( XmlNode xmlNode, T episode, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			base.Save( xmlNode, episode, dbVersion, dataStructure );

			var nameParser = new TVTNameAndDescriptionPersister();
			nameParser.Save( xmlNode, episode.Name, dbVersion, dataStructure );

			if ( (int)dbVersion > 2 )
				xmlNode.AddAttribute( "betty", episode.BettyBonus.ToString() );

			xmlNode.AddAttribute( "price", episode.PriceRate.ToString() );
			xmlNode.AddAttribute( "critics", episode.CriticsRate.ToString() );
			xmlNode.AddAttribute( "speed", episode.ViewersRate.ToString() );
			xmlNode.AddAttribute( "outcome", episode.BoxOfficeRate.ToString() );
		}
	}
}