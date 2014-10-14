using System;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class TVTStaff : IIdEntity, IIndexEntity
	{
		public string Id { get; set; }
		public int Index { get; set; }
		public TVTPersonFunction Function { get; set; }
		public TVTPerson Person { get; set; }

		public TVTStaff()
		{
			Id = "S" + UniqueIdGenerator.GetInstance().GetBase32UniqueId( 9 ).Insert( 4, "_" );
		}

		public TVTStaff( TVTPerson person, TVTPersonFunction function, int? index = null )
		{
			if ( person == null )
				throw new ArgumentNullException();

			Id = "S" + UniqueIdGenerator.GetInstance().GetBase32UniqueId( 9 ).Insert( 4, "_" );
			Person = person;
			Function = function;
			if ( index.HasValue )
				Index = index.Value;
		}

		public int SortIndex()
		{
			return ((int)Function * 100) + Index;
		}
	}
}
