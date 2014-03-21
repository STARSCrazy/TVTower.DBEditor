
using System.Collections.Generic;
namespace TVTower.Entities
{
	public class TVTPerson : TVTEntity
	{
		public string Name { get; set; }

		public string OriginalName { get; set; }

		public int TmdbId { get; set; }

		public string ImdbId { get; set; }

		public string ImageUrl { get; set; }

		public List<TVTPersonFunction> Functions { get; set; }

		public TVTPersonGender Gender { get; set; } //Als Enum?

		public string Info { get; set; }

		public string Birthday { get; set; }

		public string Deathday { get; set; }

		public string PlaceOfBirth { get; set; }

		public string OtherInfo { get; set; }

		public string Country { get; set; }

		public int MovieRegistrations { get; set; }

		public int ProfessionSkill { get; set; }		//0 - 100	Für Regisseur, Musiker und Intellektueller: Wie gut kann er sein Handwerk	
		public int Fame { get; set; }					//0 - 100	Kinokasse ++							Wie berühmt ist die Person?
		public int Success { get; set; }				//0 - 100	Kinokasse +		Kritik +	Tempo+		Wie erfolgreich ist diese Person?

		public int Power { get; set; }					//0 - 100	Kinokasse +		Tempo +++		Bonus bei manchen Genre (wie Action)
		public int Humor { get; set; }					//0 - 100	Kinokasse +		Tempo ++		Bonus bei manchen Genre (wie Komödie)
		public int Charisma { get; set; }				//0 - 100	Kinokasse +		Kritik ++		Bonus bei manchen Genre (wie Liebe, Drama, Komödie)
		public int EroticAura { get; set; }				//0 - 100	Kinokasse +++ 	Tempo +			Bonus bei manchen Genre (wie Erotik, Liebe, Action)
		public int CharacterSkill { get; set; }			//0 - 100	Kinokasse +		Kritik +++		Bonus bei manchen Genre (wie Drama)

		public int Scandalizing { get; set; }			//0 - 100	Besonders Interessant für Shows und Sonderevents
		public int PriceFactor { get; set; }

		public TVTGenre TopGenre1 { get; set; }
		public TVTGenre TopGenre2 { get; set; }

		public TVTPerson()
		{
			Functions = new List<TVTPersonFunction>();
		}
	}
}
