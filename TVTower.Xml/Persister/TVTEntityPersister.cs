using System;
using System.Xml;
using TVTower.Entities;

namespace TVTower.Xml.Persister
{
	public class TVTEntityPersister<T>
		where T : TVTEntity
	{
		public virtual void Load( XmlNode xmlNode, T entity, ITVTDatabase database, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			if ( (int)dbVersion > 2 )
			{
				foreach ( var attrib in xmlNode.Attributes )
				{
					if ( attrib is XmlLinkedNode )
					{
						var attribLinked = (XmlLinkedNode)attrib;

						switch ( attribLinked.Name )
						{
							case "id":
								entity.Id = Guid.Parse( attribLinked.GetElementValue() );
								break;
                            case "type":
								entity.DataContent = (TVTDataContent)Enum.Parse( typeof( TVTDataContent ), attribLinked.GetElementValue() );
								break;
						}
					}
				}
			}
		}

		public virtual void Save( XmlNode xmlNode, T entity, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			if ( (int)dbVersion > 2 )
			{
				xmlNode.AddAttribute( "id", entity.Id.ToString() );
				xmlNode.AddAttribute( "type", entity.DataContent.ToString() );
			}
		}
	}
}
