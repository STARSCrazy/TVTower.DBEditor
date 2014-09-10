using System;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class TVTStaff : IIdEntity, IIndexEntity
	{
		public Guid Id { get; set; }
		public int Index { get; set; }
		public TVTPersonFunction Function { get; set; }
		public TVTPerson Person { get; set; }

		public TVTStaff()
		{
			Id = Guid.NewGuid();
		}

		public TVTStaff(TVTPerson person, TVTPersonFunction function)
		{
			if ( person == null )
				throw new ArgumentNullException();

			Id = Guid.NewGuid();
			Person = person;
			Function = function;
		}

		public int SortIndex()
		{
			return ((int)Function * 100) + Index;
		}
	}
}
