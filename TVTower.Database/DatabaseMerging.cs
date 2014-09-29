using System;
using System.Linq;
using CodeKnight.Core;
using TVTower.Entities;

namespace TVTower.Database
{
	public class DatabaseMerging
	{
		public ITVTDatabase LeadingDatabase { get; set; }

		public DatabaseMerging( ITVTDatabase leadingDatabase )
		{
			LeadingDatabase = leadingDatabase;
		}

		public TVTProgramme FindProgrammeMatchWithV2( TVTProgramme other )
		{
			var allProgrammes = LeadingDatabase.GetAllProgrammes( true, true );

			var results1 = allProgrammes.Where( x => x.FakeTitleDE == other.FakeTitleDE || x.TitleDE == other.FakeTitleDE );
			if ( results1.Count() == 1 )
				return results1.First();
			else
			{
				var director = other.Staff != null ? other.Staff.FirstOrDefault( y => y.Function == TVTPersonFunction.Director ) : null;
				var direcLastName = director != null ? director.Person.FakeLastName : "unpassend";

				var results2 = allProgrammes.Where( new Func<TVTProgramme, bool>( x =>
				{
					var xI = x.GetInheritedEntity();
					var otherI = other.GetInheritedEntity();

					if ( xI.Year != otherI.Year )
						return false;

					if ( xI.MainGenre != otherI.MainGenre )
						return false;

					if ( xI.CriticsRate == otherI.CriticsRate && xI.ViewersRate == otherI.ViewersRate && xI.BoxOfficeRate == otherI.BoxOfficeRate )
						return true;

					var directorTemp = xI.Staff != null ? xI.Staff.FirstOrDefault( z => z.Function == TVTPersonFunction.Director ) : null;
					if ( directorTemp != null )
					{
						if ( directorTemp.Person.FakeLastName != direcLastName && directorTemp.Person.LastName != direcLastName )
							return false;
					}
					else
						return false;

					return true;
				} ) );

				if ( results2.Count() == 0 )
					System.Diagnostics.Trace.WriteLine( "Not found: " + other.FakeTitleDE );
				else if ( results2.Count() == 1 )
					return results2.First();
				else
					System.Diagnostics.Trace.WriteLine( "Found more then one: " + other.FakeTitleDE );
			}

			return null;
		}

		public TVTNews FindNewsMatchWithV2( TVTNews other )
		{
			var allNews = LeadingDatabase.GetAllNews();

			var results1 = allNews.Where( x => x.TitleDE == other.TitleDE );
			if ( results1.Count() == 1 )
				return results1.First();
			else
			{

				var results2 = allNews.Where( new Func<TVTNews, bool>( x =>
				{
					//if ( x.Year != other.Year )
					//    return false;

					if ( x.Genre == other.Genre && x.Topicality == other.Topicality && x.NewsType == other.NewsType && x.Price == other.Price )
					{
						if ( x.NewsType == TVTNewsType.FollowingNews && !string.IsNullOrEmpty( x.NewsThreadId ) && !string.IsNullOrEmpty( other.NewsThreadId ) )
						{
							//var thread1 = x.NewsThreadInitial;
							//var thread2 = allNews.FirstOrDefault( x2 => x2.NewsThreadId == other.NewsThreadId && x2.NewsType == TVTNewsType.InitialNews );

							if ( x.NewsThreadInitial != null && other.NewsThreadInitial != null )
							{
								if ( x.NewsThreadInitial.TitleDE == other.NewsThreadInitial.TitleDE )
									return true;
							}
							else
								return true;
						}
						else
							return true;
					}


					//if ( x.CriticsRate == other.CriticsRate && xI.ViewersRate == otherI.ViewersRate && xI.BoxOfficeRate == otherI.BoxOfficeRate )
					//    return true;

					//var directorTemp = xI.Staff != null ? xI.Staff.FirstOrDefault( z => z.Function == TVTPersonFunction.Director ) : null;
					//if ( directorTemp != null )
					//{
					//    if ( directorTemp.Person.FakeLastName != direcLastName && directorTemp.Person.LastName != direcLastName )
					//        return false;
					//}
					//else
					//    return false;
					return false;
				} ) );

				if ( results2.Count() == 0 )
					System.Diagnostics.Trace.WriteLine( "Not found: " + other.TitleDE );
				else if ( results2.Count() == 1 )
					return results2.First();
				else
					System.Diagnostics.Trace.WriteLine( "Found more then one: " + other.TitleDE );
			}

			return null;
		}

		public void CopyPropertyValues<T>( T target, T source ) where T : IIdEntity
		{
			var type = typeof( T );
			foreach ( var property in type.GetProperties() )
			{
				if ( property.Name != "Id" )
				{
					if ( property.CanWrite && property.CanRead )
					{
						property.SetValue( target, property.GetValue( source, null ), null );
					}
				}
			}
		}
	}
}
