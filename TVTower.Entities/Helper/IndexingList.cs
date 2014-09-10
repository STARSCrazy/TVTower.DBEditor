using System.Collections.Generic;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class IndexingList<T> : List<T> where T : IIndexEntity
	{
		public new void Add( T item )
		{
			item.Index = Count;
			base.Add( item );
		}
	}
}
