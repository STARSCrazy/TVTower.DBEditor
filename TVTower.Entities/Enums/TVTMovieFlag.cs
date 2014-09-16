
namespace TVTower.Entities
{
	public enum TVTMovieFlag
	{
		Live = 1,			//Genereller Quotenbonus!
		Animation = 2,		//Bonus bei Kindern / Jugendlichen. Malues bei Rentnern / Managern.
		Culture = 4,		//Bonus bei Betty und bei Managern
		Cult = 8,			//Verringert die Nachteile des Filmalters. Bonus bei Rentnern. Höhere Serientreue bei Serien.
		Trash = 16,			//Bonus bei Arbeitslosen und Hausfrauen. Malus bei Arbeitnehmern und Managern. Trash läuft morgens und mittags gut => Bonus!
		BMovie,			    //Nochmal deutlich verringerter Preis. Verringert die Nachteile des Filmalters. Bonus bei Jugendlichen. Malus bei allen anderen Zielgruppen. Bonus in der Nacht!
		XRated,				//Kleiner Bonus für Jugendliche, Arbeitnehmer, Arbeitslose, (Männer). Kleiner Malus für Kinder, Hausfrauen, Rentner, (Frauen).
		Paid,				//Call-In-Shows
		Series,				//Ist ne Serie! Vielleicht besser als den ProgrammeType... so kann auch ne Reportage ne Serie sein.

		//TG_Children,
		//TG_Teenagers,
		//TG_HouseWifes,
		//TG_Employees,
		//TG_Unemployed,
		//TG_Manager,
		//TG_Pensioners,
		//TG_Women,
		//TG_Men,

		//PG_SmokerLobby,
		//PG_AntiSmoker,
		//PG_ArmsLobby,
		//PG_Pacifists,
		//PG_Capitalists,
		//PG_Communists,


		//		Music,
		//		Sport,								
		//		YellowPress,								
	}
}

//Live = Großer Quotenbonus, feste Sendezeit.
//Animation = Bonus bei Kindern und Jugendlichen. Malus bei Rentnern und Managern.
//Kultur = Bonus bei Managern. Malus bei allen anderen Gruppen vor allem Kindern und Jugendlichen. Gefällt Betty.
//Kult-Film = Verringert die Nachteile des Filmalters. Weniger Abnutzung. Höhere Serientreue bei Serien.
//Trash = etwas geringerer Preis, Bonus im Morgen-, Mittag- und Nachtprogramm. Malus bei Managern. Ablehnung bei Betty. Senkt Image im Abendprogramm.
//B-Movie = Sehr geringerer Preis. Malus bei allen anderen Zielgruppen (außer Jugendlichen). Bonus im Nachtprogramm. Senkt Image im Abendprogramm.
//FSK18 = Nur zwischen 23 und 6 Uhr erlaubt. Kleiner Bonus für Arbeitnehmer und Arbeitslose. Großer Malus für Kinder. Kleiner Malus Hausfrauen und Rentner.
//Call-In = Show hat ein Gewinnspiel
//Füllprogramm = Winziger Preis, keine Abnutzung, geringe Quote (im Nachtprogramm)   ist typisches Dauerschleifenprogramm in der Nacht: Chatrooms, Kaminfeuer, Landschaften, Zugfahrten, SMS-Spiele

//Mit Wiederholungen, Trash, B-Movies, Füllprogramm und FSK18-Filmen kann man somit das Nachtprogramm gestalten.