using System;
using System.Collections.Generic;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class TVTNewsEffect : IIdEntity
	{
		public Guid Id { get; set; }
		public TVTNewsEffectType Type { get; set; }
		public List<string> EffectParameters { get; set; }

		public TVTNewsEffect()
		{
			Id = Guid.NewGuid();
			EffectParameters = new List<string>();
		}

		public TVTNewsEffect( TVTNewsEffectType type, string param1 = null, string param2 = null, string param3 = null )
		{
			Id = Guid.NewGuid();
			this.Type = type;
			EffectParameters = new List<string>();

			if ( !string.IsNullOrEmpty( param1 ) )
				EffectParameters.Add( param1 );

			if ( !string.IsNullOrEmpty( param2 ) )
				EffectParameters.Add( param2 );

			if ( !string.IsNullOrEmpty( param3 ) )
				EffectParameters.Add( param3 );
		}
	}
}
