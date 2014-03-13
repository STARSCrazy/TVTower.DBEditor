
namespace TVTower.Import
{
	public static class RequestChecker
	{
		static int tmdbCount = 0;
		static int rtCount = 0;

		public static void TmDbReadyOrWait()
		{
			if ( tmdbCount >= 9 )
			{
				System.Threading.Thread.Sleep( 10100 ); //30 Requests in 10 Sekunden
				tmdbCount = 0;
			}
			else if ( tmdbCount > 0 && tmdbCount % 5 == 0 )
			{
				System.Threading.Thread.Sleep( 5000 ); //30 Requests in 10 Sekunden
			}
			else
				System.Threading.Thread.Sleep( 1000 );

			tmdbCount++;
		}

		public static void TomatoReadyOrWait()
		{
			if ( rtCount >= 9 )
			{
				System.Threading.Thread.Sleep( 10100 ); //30 Requests in 10 Sekunden
				rtCount = 0;
			}
			else if ( rtCount > 0 && rtCount % 5 == 0 )
			{
				System.Threading.Thread.Sleep( 2000 ); //30 Requests in 10 Sekunden
			}
			else
				System.Threading.Thread.Sleep( 1000 );

			rtCount++;
		}
	}
}
