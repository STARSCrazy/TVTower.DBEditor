using System.Xml;
using TVTower.Entities;

namespace TVTower.XmlV2.Persister
{
	public class TVTEntityPersisterV2<T>
		where T : ITVTEntity
	{
		public virtual void Load( XmlNode xmlNode, T entity, ITVTDatabase database )
		{

		}

		public virtual void Save( XmlNode xmlNode, T entity, DatabaseVersion dbVersion, DataStructure dataStructure )
		{
			//if ( (int)dbVersion > 2 )
			//{
			//    xmlNode.AddAttribute( "id", entity.Id.ToString() );
			//    xmlNode.AddAttribute( "type", entity.DataContent.ToString() );
			//}
		}
	}
}
