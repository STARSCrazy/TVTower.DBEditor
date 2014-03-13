using System.Collections.Generic;
using System.Linq;

namespace TVTower.Import
{
	public static class MovieStatistics
	{
		public static Dictionary<int, List<long>> BoxOfficeBudget = new Dictionary<int, List<long>>(); //Jahr , Betrag
		public static Dictionary<int, List<long>> BoxOfficeRevenue = new Dictionary<int, List<long>>(); //Jahr , Betrag

		public static void Add( int year, long budget, long revenue )
		{
			if ( !BoxOfficeBudget.ContainsKey( year ) )
				BoxOfficeBudget.Add( year, new List<long>() );

			if ( !BoxOfficeRevenue.ContainsKey( year ) )
				BoxOfficeRevenue.Add( year, new List<long>() );

			if ( budget > 0 )
				BoxOfficeBudget[year].Add( budget );

			if ( revenue > 0 )
				BoxOfficeRevenue[year].Add( revenue );
		}

		public static long? AverageRevenue( int year )
		{
			var allRelevant = new List<long>();

			if ( BoxOfficeBudget.ContainsKey( year - 2 ) )
				allRelevant.AddRange( BoxOfficeBudget[year - 2] );
			if ( BoxOfficeBudget.ContainsKey( year - 1 ) )
				allRelevant.AddRange( BoxOfficeBudget[year - 1] );
			if ( BoxOfficeBudget.ContainsKey( year ) )
				allRelevant.AddRange( BoxOfficeBudget[year] );
			if ( BoxOfficeBudget.ContainsKey( year + 1 ) )
				allRelevant.AddRange( BoxOfficeBudget[year + 1] );

			if ( allRelevant.Count >= 3 )
			{
				return allRelevant.Sum( x => x ) / allRelevant.Count;
			}
			else
				return null;
		}

		public static long? TopRevenue( int year )
		{
			var allRelevant = new List<long>();

			if ( BoxOfficeRevenue.ContainsKey( year - 2 ) )
				allRelevant.AddRange( BoxOfficeRevenue[year - 2] );
			if ( BoxOfficeRevenue.ContainsKey( year - 1 ) )
				allRelevant.AddRange( BoxOfficeRevenue[year - 1] );
			if ( BoxOfficeRevenue.ContainsKey( year ) )
				allRelevant.AddRange( BoxOfficeRevenue[year] );
			if ( BoxOfficeRevenue.ContainsKey( year + 1 ) )
				allRelevant.AddRange( BoxOfficeRevenue[year + 1] );

			if ( allRelevant.Count < 3 )
			{
				if ( BoxOfficeRevenue.ContainsKey( year + 2 ) )
					allRelevant.AddRange( BoxOfficeRevenue[year + 2] );
			}

			if ( allRelevant.Count >= 3 )
			{
				return allRelevant.OrderByDescending( x => x ).First();
			}
			else
				return null;
		}
	}
}
