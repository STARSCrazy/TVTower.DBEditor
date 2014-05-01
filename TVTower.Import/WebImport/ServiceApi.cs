using System;
using System.Collections.Generic;
using System.IO;
using CherryTomato;
using WatTmdb.V3;

namespace TVTower.Import
{
	public static class ServiceApi
	{
		public static Tmdb TmdbApi = null;
		public static Tomato TomatoApi = null;

		public static void InitializeApis()
		{
			var serviceCodes = new Dictionary<string, string>();

			try
			{
				using ( StreamReader sr = new StreamReader( "servicecodes.key" ) )
				{
					while ( !sr.EndOfStream )
					{
						String line = sr.ReadLine();
						var split = line.Split( '=' );
						serviceCodes.Add( split[0], split[1] );
					}
				}
			}
			catch ( Exception e )
			{
				Console.WriteLine( "The file could not be read:" );
				Console.WriteLine( e.Message );
			}

			RequestChecker.TmDbReadyOrWait();
			TmdbApi = new Tmdb( serviceCodes["Tmdb"], "de" );

			RequestChecker.TomatoReadyOrWait();
			TomatoApi = new Tomato( serviceCodes["Tomato"] );
		}
	}
}
