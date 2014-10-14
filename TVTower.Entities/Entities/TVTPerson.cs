using System.Collections.Generic;
using CodeKnight.Core;

namespace TVTower.Entities
{
	public class TVTPerson : TVTEntity
	{
		public string FullName { get { return string.Format( "{0} {1}", FirstName, LastName ); } }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NickName { get; set; }

		public string FakeFullName { get { return string.Format( "{0} {1}", FakeFirstName, FakeLastName ); } }
		public string FakeFirstName { get; set; }
		public string FakeLastName { get; set; }
		public string FakeNickName { get; set; }

		public int TmdbId { get; set; }
		public string ImdbId { get; set; }
		public string ImageUrl { get; set; }

		public List<TVTPersonFunction> Functions { get; set; }

		public TVTPersonGender Gender { get; set; } //Als Enum?		
		public string Birthday { get; set; }
		public string Deathday { get; set; }
		//public string PlaceOfBirth { get; set; }
		public string Country { get; set; }

		public int Prominence { get; set; } // 1 = Top-Promi, 2 = Promi, 3 = Unbekannte Person

		public int Skill { get; set; }		//0 - 100	Für Regisseur, Musiker und Intellektueller: Wie gut kann er sein Handwerk. Für Schauspieler: Kinokasse +		Kritik +	Tempo +
		public int Fame { get; set; }					//0 - 100	Kinokasse ++							Wie berühmt ist die Person?
		public int Scandalizing { get; set; }			//0 - 100	Besonders Interessant für Shows und Sonderevents
		public float PriceMod { get; set; }

		public int Power { get; set; }					//0 - 100	Kinokasse +		Tempo +++		Bonus bei manchen Genre (wie Action)
		public int Humor { get; set; }					//0 - 100	Kinokasse +		Tempo ++		Bonus bei manchen Genre (wie Komödie)
		public int Charisma { get; set; }				//0 - 100	Kinokasse +		Kritik ++		Bonus bei manchen Genre (wie Liebe, Drama, Komödie)
		public int Appearance { get; set; }				//0 - 100	Kinokasse +++ 	Tempo +			Bonus bei manchen Genre (wie Erotik, Liebe, Action)
		//public int CharacterSkill { get; set; }			//0 - 100	Kinokasse +		Kritik +++		Bonus bei manchen Genre (wie Drama)

		public TVTProgrammeGenre TopGenre1 { get; set; }
		public TVTProgrammeGenre TopGenre2 { get; set; }

		public int ProgrammeCount { get; set; }

		public TVTPerson()
		{
			Functions = new List<TVTPersonFunction>();
		}

		public override void GenerateGuid()
		{
			Id = "P" + UniqueIdGenerator.GetInstance().GetBase32UniqueId( 8 ).Insert( 4, "_" );
		}

		public override TVTDataStatus RefreshStatus()
		{
			var baseStatus = base.RefreshStatus();
			if ( baseStatus == TVTDataStatus.Incorrect )
				return baseStatus;

			if ( DataType != TVTDataType.Fictitious )
			{
				if ( string.IsNullOrEmpty( FakeLastName ) || LastName == FakeLastName )
				{
					DataStatus = TVTDataStatus.NoFakes;
					return DataStatus;
				}
			}

			if ( DataStatus == TVTDataStatus.Complete )
			{
				DataStatus = TVTDataStatus.Approved;
				Approved = true; //TODO
				return DataStatus;
			}

			return DataStatus;
		}
	}
}
