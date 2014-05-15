using System.Linq;
using TVTower.Entities;

namespace TVTower.Converter
{
	public static class PersonConverter
	{
		public static void ConvertFullname( TVTPerson person, string fullname )
		{
			string firstName = null;
			string lastName = null;
			ConvertFullnameInternal( fullname, out firstName, out lastName );

			person.FirstName = firstName;
			person.LastName = lastName;
		}

		public static void ConvertFakeFullname( TVTPerson person, string fullname )
		{
			string firstName = null;
			string lastName = null;
			ConvertFullnameInternal( fullname, out firstName, out lastName );

			person.FakeFirstName = firstName;
			person.FakeLastName = lastName;
		}

		private static void ConvertFullnameInternal( string fullname, out string firstName, out string lastName )
		{
			var count = fullname.Count( x => x == ' ' );

			if ( count == 0 )
			{
				firstName = null;
				lastName = fullname;
			}
			else if ( count == 1 )
			{
				var index = fullname.IndexOf( ' ' );
				firstName = fullname.Substring( 0, index );
				lastName = fullname.Substring( index + 1 );
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
				while ( true )
				{
					if ( lastIndex > 0 )
						sub = sub.Substring( 0, lastIndex );
					index = sub.LastIndexOf( ' ' );
					diff = lastIndex - index - 1;
					if ( diff > 3 || lastIndex == index || (lastIndex > 0 && sub.Substring( index + 1, diff ).Contains( '.' )) )
						break;
					lastIndex = index;
				}
				if ( lastIndex <= -1 )
					lastIndex = fullname.IndexOf( ' ' );

				firstName = fullname.Substring( 0, lastIndex );
				lastName = fullname.Substring( lastIndex + 1 );
			}
		}
	}
}
