using System;
using System.Linq;
using System.Collections.Generic;

namespace TVTower.Faker
{
	public class FakeName
	{
		private List<Syllable> Syllables;

		public void InitializeData()
		{
			Syllables = new List<Syllable>();
			Syllables.Add( new Syllable( "john", "djohnn", 1 ) );

			Syllables.Add( new Syllable( "ton", "don", 2 ) );
			Syllables.Add( new Syllable( "don", "ton", 2 ) );
			Syllables.Add( new Syllable( "har", "aar", 2 ) );

			Syllables.Add( new Syllable( "ste", "shti" ) );
			Syllables.Add( new Syllable( "ven", "phen" ) );
			Syllables.Add( new Syllable( "sp", "sb" ) );
			Syllables.Add( new Syllable( "iel", "yl" ) );
			Syllables.Add( new Syllable( "berg", "borg" ) );
			Syllables.Add( new Syllable( "stan", "sthen" ) );
			Syllables.Add( new Syllable( "ley", "li" ) );
			Syllables.Add( new Syllable( "kub", "cup" ) );
			Syllables.Add( new Syllable( "ick", "igg" ) );
			Syllables.Add( new Syllable( "rom", "ron" ) );
			Syllables.Add( new Syllable( "man", "mann" ) );
			Syllables.Add( new Syllable( "nan", "nann" ) );
			Syllables.Add( new Syllable( "pol", "bhol" ) );
			Syllables.Add( new Syllable( "ski", "sghi" ) );
			Syllables.Add( new Syllable( "hen", "han" ) );
			Syllables.Add( new Syllable( "ry", "rih" ) );
			Syllables.Add( new Syllable( "cut", "katt" ) );
			Syllables.Add( new Syllable( "ama", "amha" ) );
			Syllables.Add( new Syllable( "ran", "runn" ) );
			Syllables.Add( new Syllable( "alf", "halph" ) );
			Syllables.Add( new Syllable( "red", "rit" ) );
			Syllables.Add( new Syllable( "tch", "dsh" ) );
			Syllables.Add( new Syllable( "ock", "ogg" ) );
			Syllables.Add( new Syllable( "hit", "it" ) );
			Syllables.Add( new Syllable( "co", "go" ) );
			Syllables.Add( new Syllable( "how", "ouw" ) );
			Syllables.Add( new Syllable( "ard", "art" ) );
			Syllables.Add( new Syllable( "haw", "hav" ) );
			Syllables.Add( new Syllable( "vks", "vkz" ) );
			Syllables.Add( new Syllable( "mar", "mer" ) );
			Syllables.Add( new Syllable( "die", "dy" ) );
			Syllables.Add( new Syllable( "bsch", "psh" ) );
			Syllables.Add( new Syllable( "sch", "sh" ) );
			Syllables.Add( new Syllable( "ü", "jü" ) );
			Syllables.Add( new Syllable( "dav", "jav" ) );
			Syllables.Add( new Syllable( "vid", "vith" ) );
			Syllables.Add( new Syllable( "lyn", "liehn" ) );
			Syllables.Add( new Syllable( "nch", "ntsh" ) );
			Syllables.Add( new Syllable( "hus", "hass" ) );
			Syllables.Add( new Syllable( "voh", "froh" ) );
			Syllables.Add( new Syllable( "rer", "rehr" ) );
			Syllables.Add( new Syllable( "rid", "ritt" ) );
			Syllables.Add( new Syllable( "ott", "odt" ) );
			Syllables.Add( new Syllable( "car", "kar" ) );
			Syllables.Add( new Syllable( "pen", "bhen" ) );
			Syllables.Add( new Syllable( "ter", "tor" ) );
			Syllables.Add( new Syllable( "ner", "mjer" ) );			
			Syllables.Add( new Syllable( "ald", "alt" ) );
			Syllables.Add( new Syllable( "rei", "raj" ) );
		}

		public string Fake( string name )
		{
			if ( ReplaceIntern( ref name, 1, true ) )
				return name;

			ReplaceIntern( ref name, 2, true );
			ReplaceIntern( ref name, 3, false );
				
			return name;
		}

		private bool ReplaceIntern( ref string name, int level, bool onlyOne)
		{
			var result = false;
			var lc = name.ToLower();
			foreach ( var syl in Syllables.Where( x => x.Level == level ) )
			{
				var key = syl.Key;
				if ( lc.Contains( key ) )
				{
					var index = lc.IndexOf( key );
					var value = syl.Value[0];
					if ( Char.IsUpper( name[index] ) )
					{
						key = key[index].ToString().ToUpper() + key.Substring( 1 );
						value = value[index].ToString().ToUpper() + value.Substring( 1 );
					}

					name = name.Replace( key, value );
					result = true;
					if ( onlyOne )
						return true;
					else
						lc = name.ToLower();
				}
			}

			return result;
		}
	}

	public class Syllable
	{
		public int Level;
		public string Key;
		public List<string> Value;

		public Syllable( string key, string value, int level = 3 )
		{
			Level = level;
			Key = key;
			Value = new List<string>();
			Value.Add( value );
		}
	}
}
