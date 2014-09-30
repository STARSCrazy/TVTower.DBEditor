using System;
using System.Collections.Generic;
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

		public TVTProgramme FindProgrammeMatch( TVTProgramme other )
		{
			var allProgrammes = LeadingDatabase.GetAllProgrammes( true, true );

			var programme = allProgrammes.FirstOrDefault( x => x.Id == other.Id );
			if ( programme != null )
				return programme;

			if ( other.TmdbId > 0 )
			{
				programme = allProgrammes.FirstOrDefault( x => x.TmdbId == other.TmdbId );
				if ( programme != null )
					return programme;
			}

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

					if ( x.ProductType != otherI.ProductType )
						return false;

					if ( x.ProductType == TVTProductType.Episode )
					{
						if ( x.EpisodeIndex != otherI.EpisodeIndex )
							return false;
					}

					var directorTemp = xI.Staff != null ? xI.Staff.FirstOrDefault( z => z.Function == TVTPersonFunction.Director ) : null;
					if ( directorTemp != null && directorTemp.Person != null )
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
					if ( x.Genre == other.Genre && x.Topicality == other.Topicality && x.NewsType == other.NewsType && x.Price == other.Price )
					{
						if ( x.NewsType == TVTNewsType.FollowingNews && !string.IsNullOrEmpty( x.NewsThreadId ) && !string.IsNullOrEmpty( other.NewsThreadId ) )
						{
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

		public TVTAdvertising FindAdMatchWithV2( TVTAdvertising other )
		{
			var allAds = LeadingDatabase.GetAllAdvertisings();

			var results1 = allAds.Where( x => x.TitleDE == other.TitleDE );
			if ( results1.Count() == 1 )
				return results1.First();
			else
			{

				var results2 = allAds.Where( new Func<TVTAdvertising, bool>( x =>
				{
					//if ( x.Genre == other.Genre && x.Topicality == other.Topicality && x.NewsType == other.NewsType && x.Price == other.Price )
					//{
					//    if ( x.NewsType == TVTNewsType.FollowingNews && !string.IsNullOrEmpty( x.NewsThreadId ) && !string.IsNullOrEmpty( other.NewsThreadId ) )
					//    {
					//        if ( x.NewsThreadInitial != null && other.NewsThreadInitial != null )
					//        {
					//            if ( x.NewsThreadInitial.TitleDE == other.NewsThreadInitial.TitleDE )
					//                return true;
					//        }
					//        else
					//            return true;
					//    }
					//    else
					//        return true;
					//}

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

		public TVTPerson FindPersonMatch( TVTPerson other )
		{
			var allPeople = LeadingDatabase.GetAllPeople();

			var person = allPeople.FirstOrDefault( x => x.Id == other.Id );
			if ( person != null )
				return person;

			if ( other.TmdbId > 0 )
			{
				person = allPeople.FirstOrDefault( x => x.TmdbId == other.TmdbId );
				if ( person != null )
					return person;
			}

			var results1 = allPeople.Where( x => x.FullName == other.FullName || x.FakeFullName == other.FullName || x.FullName == other.FakeFullName || x.FakeFullName == other.FakeFullName );
			if ( results1.Count() == 1 )
				return results1.First();
			else
			{

				var results2 = allPeople.Where( new Func<TVTPerson, bool>( x =>
				{
					//if ( x.Genre == other.Genre && x.Topicality == other.Topicality && x.NewsType == other.NewsType && x.Price == other.Price )
					//{
					//    if ( x.NewsType == TVTNewsType.FollowingNews && !string.IsNullOrEmpty( x.NewsThreadId ) && !string.IsNullOrEmpty( other.NewsThreadId ) )
					//    {
					//        if ( x.NewsThreadInitial != null && other.NewsThreadInitial != null )
					//        {
					//            if ( x.NewsThreadInitial.TitleDE == other.NewsThreadInitial.TitleDE )
					//                return true;
					//        }
					//        else
					//            return true;
					//    }
					//    else
					//        return true;
					//}

					return false;
				} ) );

				if ( results2.Count() == 0 )
					System.Diagnostics.Trace.WriteLine( "Not found: " + other.FullName );
				else if ( results2.Count() == 1 )
					return results2.First();
				else
					System.Diagnostics.Trace.WriteLine( "Found more then one: " + other.FullName );
			}

			return null;
		}

		public void MergeProgrammeData( TVTProgramme target, TVTProgramme leading, bool mergeStaffOnlyWhenEmpty = false )
		{
			//new List<string>() { "Staff", "Children", "SeriesMaster", "MasterId" }

			var type = typeof( TVTProgramme );
			foreach ( var property in type.GetProperties() )
			{
				if ( property.Name == "Staff" )
				{
					if ( !mergeStaffOnlyWhenEmpty || (target.Staff == null || target.Staff.Count == 0) )
					{
						target.Staff.Clear();

						foreach ( var member in leading.Staff )
						{
							var person = FindPersonMatch( member.Person );

							if ( person == null )
							{
								if ( member.Person.FakeLastName != "-" && member.Person.FakeLastName != "XXX" )
								{
									person = new TVTPerson();
									person.GenerateGuid();
									CopyPropertyValues<TVTPerson>( person, member.Person );
									person.IsNew = true;
									person.IsChanged = true;
									LeadingDatabase.AddPerson( person );
								}
							}

							if ( person != null )
								target.Staff.Add( new TVTStaff( person, member.Function, member.Index ) );
						}
					}
				}
				else if ( property.Name == "Children" )
				{
					if ( target.ProductType == TVTProductType.Series || target.ProductType == TVTProductType.Bundle )
					{
						if ( target.Children != null )
							target.Children.Clear();
						else
							target.Children = new List<TVTProgramme>();

						foreach ( var child in leading.Children )
						{
							var episode = FindProgrammeMatch( child );

							if ( episode == null )
							{
								episode = new TVTProgramme();
								episode.GenerateGuid();
								MergeProgrammeData( episode, child );
								episode.IsNew = true;
								LeadingDatabase.AddProgramme( episode );
							}

							if ( episode != null )
							{
								episode.SeriesMaster = new WeakReference<TVTProgramme>( target );
								episode.MasterId = target.Id.ToString();
								episode.IsChanged = true;
								target.Children.Add( episode );
							}
						}
					}
				}
				else if ( property.Name == "Children" || property.Name == "SeriesMaster" || property.Name == "MasterId" )
				{
					//TODO: Später mehr
				}
				else
				{
					if ( property.Name != "Id" )
					{
						if ( property.CanWrite && property.CanRead )
						{
							property.SetValue( target, property.GetValue( leading, null ), null );
						}
					}
				}
			}

			target.Children = null;
		}

		public void CopyPropertyValues<T>( T target, T source, List<string> dontCopyList = null ) where T : IIdEntity
		{
			var type = typeof( T );
			foreach ( var property in type.GetProperties() )
			{
				if ( dontCopyList != null && dontCopyList.Contains( property.Name ) )
					continue;

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
