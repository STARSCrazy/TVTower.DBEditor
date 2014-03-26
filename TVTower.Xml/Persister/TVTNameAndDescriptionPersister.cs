using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTNameAndDescriptionPersister
	{
		public void Load( XmlNode xmlNode, TVTNameAndDescription nameDes, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			foreach ( var movieChild in xmlNode.ChildNodes )
			{
				if ( movieChild is XmlLinkedNode )
				{
					var linkedNode = (XmlLinkedNode)movieChild;

					switch ( linkedNode.Name )
					{
						case "title":
						case "title_de":
							if ( dataStructure == DataStructure.FakeData )
								nameDes.FakeTitleDE = linkedNode.GetElementValue();
							else
								nameDes.OriginalTitleDE = linkedNode.GetElementValue();
							break;
						case "title_en":
							if ( dataStructure == DataStructure.FakeData )
								nameDes.FakeTitleEN = linkedNode.GetElementValue();
							else
								nameDes.OriginalTitleEN = linkedNode.GetElementValue();
							break;
						case "description":
						case "description_de":
							if ( dataStructure == DataStructure.FakeData )
								nameDes.FakeDescriptionDE = linkedNode.GetElementValue();
							else
								nameDes.OriginalDescriptionDE = linkedNode.GetElementValue();
							break;
						case "description_en":
							if ( dataStructure == DataStructure.FakeData )
								nameDes.FakeDescriptionEN = linkedNode.GetElementValue();
							else
								nameDes.OriginalDescriptionEN = linkedNode.GetElementValue();
							break;
					}
				}
			}
		}

		public void Save( XmlNode xmlNode, TVTNameAndDescription nameDes, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			if ( dbVersion == DatabaseVersion.V2 )
			{
				if ( dataStructure == DataStructure.FakeData )
				{
					xmlNode.AddElement( "title", nameDes.FakeTitleDE );
					xmlNode.AddElement( "description", nameDes.FakeDescriptionDE );
				}
				else
				{
					xmlNode.AddElement( "title", nameDes.OriginalTitleDE );
					xmlNode.AddElement( "description", nameDes.OriginalDescriptionDE );
				}
			}
			else if ( dbVersion == DatabaseVersion.V3 )
			{
				xmlNode.AddElement( "title_de", nameDes.OriginalTitleDE );
				xmlNode.AddElement( "title_en", nameDes.OriginalTitleEN );
				xmlNode.AddElement( "description_de", nameDes.OriginalDescriptionDE );
				xmlNode.AddElement( "description_en", nameDes.OriginalDescriptionEN );
			}

			if ( dataStructure == DataStructure.Full )
			{
				var additionalNode = xmlNode.OwnerDocument.CreateElement( "name_full" );

				xmlNode.AddElement( "fake_title_de", nameDes.FakeTitleDE );
				xmlNode.AddElement( "fake_title_en", nameDes.FakeTitleEN );
				xmlNode.AddElement( "fake_description_de", nameDes.FakeDescriptionDE );
				xmlNode.AddElement( "fake_description_en", nameDes.FakeDescriptionDE );

				xmlNode.AddElement( "original_title_de", nameDes.OriginalTitleDE );
				xmlNode.AddElement( "original_title_en", nameDes.OriginalTitleEN );
				xmlNode.AddElement( "original_description_de", nameDes.OriginalDescriptionDE );
				xmlNode.AddElement( "original_description_en", nameDes.OriginalDescriptionDE );

				xmlNode.AddElement( "description_movie_db", nameDes.DescriptionMovieDB );

				xmlNode.AppendChild( additionalNode );
			}
		}
	}
}