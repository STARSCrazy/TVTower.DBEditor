using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTEpisodePersister<T> : TVTEntityPersister<T>
		where T : TVTEpisode
	{
		public override void Load( XmlNode xmlNode, T episode, ITVTDatabase database )
		{
			base.Load( xmlNode, episode, database );

			foreach ( var movieChild in xmlNode.ChildNodes )
			{
				if ( movieChild is XmlLinkedNode )
				{
					var linkedNode = (XmlLinkedNode)movieChild;

					switch ( linkedNode.Name )
					{
						case "title":
							episode.TitleDE = linkedNode.GetElementValue();
							break;
						case "title_de":
							episode.TitleDE = linkedNode.GetElementValue();
							break;
						case "title_en":
							episode.TitleEN = linkedNode.GetElementValue();
							break;

						case "description":
							episode.DescriptionDE = linkedNode.GetElementValue();
							break;
						case "description_de":
							episode.DescriptionDE = linkedNode.GetElementValue();
							break;
						case "description_en":
							episode.DescriptionEN = linkedNode.GetElementValue();
							break;

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

		//public override void Save( XmlNode xmlNode, T episode, DatabaseVersion dbVersion, DataStructure dataStructure )
		//{
		//    base.Save( xmlNode, episode, dbVersion, dataStructure );

		//    switch ( dataStructure )
		//    {
		//        case DataStructure.Full:
		//            xmlNode.AddElement( "title_de", episode.TitleDE );
		//            xmlNode.AddElement( "title_en", episode.TitleEN );
		//            xmlNode.AddElement( "description_de", episode.DescriptionDE );
		//            xmlNode.AddElement( "description_en", episode.DescriptionEN );
		//            break;
		//        case DataStructure.FakeData:
		//            if ( dbVersion == DatabaseVersion.V2 )
		//            {
		//                xmlNode.AddElement( "title", episode.TitleDE );
		//                xmlNode.AddElement( "description", episode.DescriptionDE );
		//            }
		//            else
		//            {
		//                xmlNode.AddElement( "title_de", episode.TitleDE );
		//                xmlNode.AddElement( "title_en", episode.TitleEN );
		//                xmlNode.AddElement( "description_de", episode.DescriptionDE );
		//                xmlNode.AddElement( "description_en", episode.DescriptionEN );
		//            }
		//            break;
		//        case DataStructure.OriginalData:
		//            break;
		//    }
		//}
	}
}
;