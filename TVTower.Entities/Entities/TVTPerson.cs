using System.Linq;
using System.Collections.Generic;

namespace TVTower.Entities
{
	public class TVTPerson : TVTEntity
	{
		public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NickName { get; set; }

		public string FakeFullName { get { return string.Format("{0} {1}", FakeFirstName, FakeLastName); } }
		public string FakeFirstName { get; set; }
		public string FakeLastName { get; set; }
		public string FakeNickName { get; set; }

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

        public void ConvertFullname(string fullname)
        {
            string firstName = null;
            string lastName = null;
            ConvertFullnameInternal(fullname, out firstName, out lastName);

            FirstName = firstName;
            LastName = lastName;
        }

        public void ConvertFakeFullname(string fullname)
        {
            string firstName = null;
            string lastName = null;
            ConvertFullnameInternal(fullname, out firstName, out lastName);

            FakeFirstName = firstName;
            FakeLastName = lastName;
        }

        private void ConvertFullnameInternal(string fullname, out string firstName, out string lastName)
        {
            var count = fullname.Count(x => x == ' ');

            if (count == 0)
            {
                firstName = null;
                lastName = fullname;
            }
            else if (count == 1)
            {
                var index = fullname.IndexOf(' ');
                firstName = fullname.Substring(0, index);
                lastName = fullname.Substring(index + 1);
            }
            //else
            //{
            //    var index = fullname.LastIndexOf(' ');
            //    firstName = fullname.Substring(0, index);
            //    lastName = fullname.Substring(index + 1);
            //}
            else
            {
                int diff = 0;
                int lastIndex = 0;
                int index = 0;
                string sub = fullname;
                while (true)
                {
                    if (lastIndex > 0)
                        sub = sub.Substring(0, lastIndex);
                    index = sub.LastIndexOf(' ');
                    diff = lastIndex - index - 1;
                    if (diff > 3 || lastIndex == index || (lastIndex > 0 && sub.Substring(index+1, diff).Contains('.')))
                        break;
                    lastIndex = index;
                }
                if (lastIndex <= -1)
                    lastIndex = fullname.IndexOf(' ');

                firstName = fullname.Substring(0, lastIndex);
                lastName = fullname.Substring(lastIndex + 1);
            }
        }
	}
}
