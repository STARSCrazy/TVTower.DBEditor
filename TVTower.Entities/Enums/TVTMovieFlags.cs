
namespace TVTower.Entities
{
	public enum TVTMovieFlags
	{
		TG_Children,
		TG_Teenagers,
		TG_HouseWifes,
		TG_Employees,
		TG_Unemployed,
		TG_Manager,
		TG_Pensioners,
		TG_Women,
		TG_Men,

		PG_SmokerLobby,
		PG_AntiSmoker,
		PG_ArmsLobby,
		PG_Pacifists,
		PG_Capitalists,
		PG_Communists,

		Live,			//Genereller Quotenbonus!
		Music,
		Sport,
		Animation,		//Bonus bei Kindern / Jugendlichen. Malues bei Rentnern / Managern.
		Culture,		//Bonus bei Betty und bei Managern
		Cult,			//Verringert die Nachteile des Filmalters. Bonus bei Rentnern. Höhere Serientreue bei Serien.
		Trash,			//Bonus bei Arbeitslosen und Hausfrauen. Malus bei Arbeitnehmern und Managern. Trash läuft morgens und mittags gut => Bonus!
		YellowPress,
		BMovie,			//Nochmal deutlich verringerter Preis. Verringert die Nachteile des Filmalters. Bonus bei Jugendlichen. Malus bei allen anderen Zielgruppen. Bonus in der Nacht!
		FSK18,			//Kleiner Bonus für Jugendliche, Arbeitnehmer, Arbeitslose, (Männer). Kleiner Malus für Kinder, Hausfrauen, Rentner, (Frauen).
		Paid,			//Call-In-Shows

		Series			//Ist ne Serie!
	}
}
