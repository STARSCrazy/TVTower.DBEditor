﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TVTower.Converter
{
	public class NameFaker
	{
		private List<Syllable> Syllables;
        private List<Fix> Fixes;

		public void InitializeData()
		{
			Syllables = new List<Syllable>();
			Syllables.Add( new Syllable( "john", "djohnn", 1 ) );
			Syllables.Add( new Syllable( "kurt", "gurt", 1 ) );
			Syllables.Add( new Syllable( "guy", "guij", 1 ) );
			Syllables.Add( new Syllable( "brian", "Praian", 1 ) );


			Syllables.Add( new Syllable( "ton", "don", 2 ) );
			Syllables.Add( new Syllable( "don", "ton", 2 ) );
			Syllables.Add( new Syllable( "har", "aar", 2 ) );
			Syllables.Add( new Syllable( "mann", "mahn", 2 ) );
			Syllables.Add( new Syllable( "man", "mann", 2 ) );
			Syllables.Add( new Syllable( "nann", "man", 2 ) );
			Syllables.Add( new Syllable( "nan", "nann", 2 ) );

            Syllables.Add(new Syllable("hoff", "off"));

            
            Syllables.Add( new Syllable( "chr", "gr" ) );
			Syllables.Add( new Syllable( "ste", "shti" ) );
			Syllables.Add( new Syllable( "ven", "phen" ) );
			Syllables.Add( new Syllable( "iel", "yl" ) );
			Syllables.Add( new Syllable( "berg", "borg" ) );
			Syllables.Add( new Syllable( "stan", "sthen" ) );
			Syllables.Add( new Syllable( "ley", "li" ) );
			Syllables.Add( new Syllable( "kub", "cup" ) );
			Syllables.Add( new Syllable( "ick", "igg" ) );
			Syllables.Add( new Syllable( "rom", "ron" ) );
			Syllables.Add( new Syllable( "pol", "boll" ) );
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
			Syllables.Add( new Syllable( "how", "ouw" ) );
			Syllables.Add( new Syllable( "ard", "art" ) );
			Syllables.Add( new Syllable( "haw", "hav" ) );
			Syllables.Add( new Syllable( "vks", "vkz" ) );
			Syllables.Add( new Syllable( "mar", "mer" ) );
			Syllables.Add( new Syllable( "die", "dy" ) );
			Syllables.Add( new Syllable( "bsch", "psh" ) );
			Syllables.Add( new Syllable( "sch", "sh" ) );
			Syllables.Add( new Syllable( "ü", "jü" ) );
			Syllables.Add( new Syllable( "dav", "tav" ) );
			Syllables.Add( new Syllable( "vid", "vit" ) );
			Syllables.Add( new Syllable( "lyn", "lien" ) );
			Syllables.Add( new Syllable( "nch", "ntsh" ) );
			Syllables.Add( new Syllable( "hus", "hass" ) );
			Syllables.Add( new Syllable( "voh", "froh" ) );
			Syllables.Add( new Syllable( "rer", "rehr" ) );
			Syllables.Add( new Syllable( "rid", "ritt" ) );
			Syllables.Add( new Syllable( "ott", "odt" ) );
			Syllables.Add( new Syllable( "car", "gar" ) );
			Syllables.Add( new Syllable( "pen", "ben" ) );
			Syllables.Add( new Syllable( "ter", "der" ) );
			Syllables.Add( new Syllable( "ner", "nr" ) );
			Syllables.Add( new Syllable( "ald", "alt" ) );
			Syllables.Add( new Syllable( "rei", "ray" ) );			
			Syllables.Add( new Syllable( "greg", "kreg" ) );
			Syllables.Add( new Syllable( "bon", "pon" ) );
			Syllables.Add( new Syllable( "bom", "pom" ) );
			Syllables.Add( new Syllable( "pal", "bal" ) );
			Syllables.Add( new Syllable( "sp", "sb" ) );
			Syllables.Add( new Syllable( "co", "go" ) );
			Syllables.Add( new Syllable( "leo", "lio" ) );
			Syllables.Add( new Syllable( "ser", "zer" ) );
			Syllables.Add( new Syllable( "gio", "tshio" ) );
			Syllables.Add( new Syllable( "cis", "zis" ) );
			Syllables.Add( new Syllable( "ord", "ortt" ) );
            Syllables.Add(new Syllable("mac", "mak"));
            Syllables.Add(new Syllable("mc", "mak"));
            Syllables.Add(new Syllable("ith", "idt"));
            Syllables.Add(new Syllable("smi", "shmi"));
            Syllables.Add(new Syllable("tee", "tii"));
            Syllables.Add(new Syllable("gin", "kin"));
            Syllables.Add(new Syllable("nis", "iyss"));
            Syllables.Add(new Syllable("hep", "hib"));
            Syllables.Add(new Syllable("burn", "bourne"));
            Syllables.Add(new Syllable("eal", "iil"));
            Syllables.Add(new Syllable("alo", "aolu"));
            Syllables.Add(new Syllable("iak", "ijac"));
            Syllables.Add(new Syllable("nus", "nos"));
            Syllables.Add(new Syllable("rez", "riecz"));


            Syllables.Add( new Syllable( "son", "sun" ) );
            Syllables.Add( new Syllable( "man", "men" ) );
            Syllables.Add( new Syllable( "ell", "iel" ) );
            Syllables.Add( new Syllable( "and", "ent" ) );
            Syllables.Add( new Syllable( "ber", "pier" ) );

            Syllables.Add( new Syllable( "ing", "jing" ) );
            Syllables.Add( new Syllable( "ill", "jell" ) );
            Syllables.Add( new Syllable( "lan", "lon" ) );
            Syllables.Add( new Syllable( "lle", "lhe" ) );
            Syllables.Add( new Syllable( "ton", "turn" ) );
            Syllables.Add( new Syllable( "ers", "ehs" ) );
            Syllables.Add( new Syllable( "ter", "tjier" ) );
            Syllables.Add( new Syllable( "der", "ter", 4 ) );
            Syllables.Add( new Syllable( "har", "er" ) );
            Syllables.Add( new Syllable( "ger", "kehr" ) );
            Syllables.Add( new Syllable( "ner", "mehr" ) );
            Syllables.Add( new Syllable( "her", "hear" ) );
            Syllables.Add( new Syllable( "all", "ehl" ) );
            Syllables.Add( new Syllable( "che", "ke" ) );
            Syllables.Add( new Syllable( "ler", "leer" ) );
            Syllables.Add( new Syllable( "ann", "amn" ) );
            Syllables.Add( new Syllable( "ste", "staeh" ) );
            Syllables.Add( new Syllable( "pla", "bla" ) );

            Syllables.Add(new Syllable("ran", "rehn"));
            Syllables.Add(new Syllable("nne", "nmee"));
            Syllables.Add(new Syllable("lin", "lijin"));
            Syllables.Add(new Syllable("one", "ohme"));
            Syllables.Add(new Syllable("don", "tjon"));
            Syllables.Add(new Syllable("kin", "gin"));
            Syllables.Add(new Syllable("lli", "lai"));
            Syllables.Add(new Syllable("ore", "jore"));
            Syllables.Add(new Syllable("ins", "ims"));
            


//ste = 30
//ran = 25
//nne = 25
//lin = 23
//one = 23
//don = 22
//kin = 22
//lli = 22
//ore = 21
//ins = 21
            
            


			Syllables.Add( new Syllable( "ne", "ner", 3, 3 ) );

			Syllables.Add( new Syllable( "ma", "na", 5 ) );            
			//Syllables.Add( new Syllable( "ne", "neh", 4 ) );
			//

			//Syllables.Add( new Syllable( "b", "p", 5 ) );
			//Syllables.Add( new Syllable( "g", "k", 5 ) );
			//Syllables.Add( new Syllable( "h", "", 5 ) );
			//Syllables.Add( new Syllable( "i", "j", 5 ) );
			//Syllables.Add( new Syllable( "j", "y", 5 ) );
			//Syllables.Add( new Syllable( "n", "m", 5 ) );
			//Syllables.Add( new Syllable( "q", "k", 5 ) );
			//Syllables.Add( new Syllable( "w", "v", 5 ) );


            Fixes = new List<Fix>();
            Fixes.Add(new Prefix("Don"));
            Fixes.Add(new Prefix("Mac"));
            Fixes.Add(new Prefix("O'"));
            Fixes.Add(new Prefix("Nic"));
            Fixes.Add(new Prefix("Il-"));
            Fixes.Add(new Prefix("von "));
            Fixes.Add(new Prefix("van "));
            Fixes.Add(new Prefix("be "));

            Fixes = new List<Fix>();
            Fixes.Add(new Suffix("son"));
            Fixes.Add(new Suffix("san"));
            Fixes.Add(new Suffix("rol"));
            Fixes.Add(new Suffix("er"));
            Fixes.Add(new Suffix("bald"));
		}

		public string Fake( string name )
		{
            var start = name;

			if ( ReplaceIntern( ref name, 1, true ) > 0 )
				return name;

			var changed2 = ReplaceIntern( ref name, 2, true );
            if (changed2 >= 3)
                return name;

            var changed3 = changed2 + ReplaceIntern(ref name, 3, false);
            if (changed3 >= 3)
                return name;

            var changed4 = 0;

            if (changed3 < 3)
                changed4 = changed3 + ReplaceIntern(ref name, 4, false);

            if (changed4 < 2)
                ReplaceIntern(ref name, 5, false);

            if (start == name)
            {
                var random = new Random();
                var fix = Fixes[random.Next(0, Fixes.Count -1)];
                return fix.AddTo(name);
            }

			return name;
		}

		private int ReplaceIntern( ref string name, int level, bool onlyOne )
		{
			var result = 0;
			var lc = name.ToLower();
			foreach ( var syl in Syllables.Where( x => x.Level == level && x.Part == 2 ) )
			{
				var key = syl.Key;
				if ( lc.Contains( key ) )
				{
					var index = lc.IndexOf( key );
					var value = syl.Value[0];
					if ( Char.IsUpper( name[index] ) )
					{
						key = key[0].ToString().ToUpper() + key.Substring( 1 );
						value = value[0].ToString().ToUpper() + value.Substring( 1 );
					}

					name = name.Replace( key, value );
					result++;
					if ( onlyOne )
                        return result;
					else
						lc = name.ToLower();
				}
			}

			foreach ( var syl in Syllables.Where( x => x.Level == level && x.Part == 3 ) )
			{
				var key = syl.Key;
				if ( lc.EndsWith( key ) )
				{
					var index = lc.LastIndexOf( key );
					var value = syl.Value[0];
					if ( Char.IsUpper( name[index] ) )
					{
						key = key[0].ToString().ToUpper() + key.Substring( 1 );
						value = value[0].ToString().ToUpper() + value.Substring( 1 );
					}

					name = name.Replace( key, value );
					result++;
					if ( onlyOne )
                        return result;
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
		public int Part;

		public Syllable( string key, string value, int level = 3, int part = 2 )
		{
			Part = part;
			Level = level;
			Key = key;
			Value = new List<string>();
			Value.Add( value );
		}
	}

    public class Fix
    {
        public string Value;

        public Fix(string value)
        {
            this.Value = value;
        }

        public virtual string AddTo(string addTo)
        {
            return addTo;
        }
    }

    public class Prefix : Fix
    {
        public Prefix(string value) : base(value)
        {
        }

        public override string AddTo(string addTo)
        {
            return Value + addTo;
        }
    }

    public class Suffix : Fix
    {
        public Suffix(string value)
            : base(value)
        {
        }

        public override string AddTo(string addTo)
        {
            return addTo + Value;
        }
    }
}
