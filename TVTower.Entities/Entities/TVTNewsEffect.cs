using System.Collections.Generic;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class TVTNewsEffect : IIdEntity
	{
		public string Id { get; set; }
		public TVTNewsEffectType Type { get; set; }
		public List<string> EffectParameters { get; set; }
		public int Chance { get; set; }

		public TVTNewsEffect()
		{
			Id = "E" + UniqueIdGenerator.GetInstance().GetBase32UniqueId( 9 ).Insert( 4, "_" );
			EffectParameters = new List<string>();
			Chance = 100;
		}

		public TVTNewsEffect( TVTNewsEffectType type, string param1 = null, string param2 = null, string param3 = null )
		{
			Id = "E" + UniqueIdGenerator.GetInstance().GetBase32UniqueId( 9 ).Insert( 4, "_" );
			this.Type = type;
			EffectParameters = new List<string>();
			Chance = 100;

			if ( !string.IsNullOrEmpty( param1 ) )
				EffectParameters.Add( param1 );

			if ( !string.IsNullOrEmpty( param2 ) )
				EffectParameters.Add( param2 );

			if ( !string.IsNullOrEmpty( param3 ) )
				EffectParameters.Add( param3 );
		}
	}
}
