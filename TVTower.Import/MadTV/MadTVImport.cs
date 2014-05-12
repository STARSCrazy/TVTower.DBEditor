using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TVTower.Importer.MadTV
{
	public class MadTVImport
	{
		private static Encoding encodingCache;

		public static Encoding GetMadTVEncoding()
		{
			if ( encodingCache == null )
				encodingCache = MadTVEncoding.GetEncoding(); //(UTF7Encoding)new UTF7Encoding().Clone();
			return encodingCache;
		}

		//nameFilePath = "SOURCE1.RSC"
		//namesEntryPoint = 478040
		//valuesFilePath = "SOURCE1.RSC"
		//valuesEntryPoint = 439856
		public void ImportMovies( string nameFilePath, int namesEntryPoint, string valuesFilePath, int valuesEntryPoint )
		{
			List<MadTVMovie> result = new List<MadTVMovie>();

			using ( FileStream file = new FileStream( nameFilePath, FileMode.Open, FileAccess.Read ) )
			{
				var positionIndex = namesEntryPoint;
				file.Position = positionIndex;
				byte[] buffer = new byte[200];
				file.Read( buffer, 0, buffer.Length );

				while ( true )
				{
					var encoding = GetMadTVEncoding();

					var movie = new MadTVMovie();

					var titleLength = (int)buffer[0];
					movie.Title = encoding.GetString( buffer.SubArray( 1, titleLength ) );
					var descLength = (int)buffer[titleLength + 1];
					movie.Description = encoding.GetString( buffer.SubArray( titleLength + 2, descLength ) );

					if ( movie.Title.Contains( "~" ) || movie.Description.Contains( "~" ) )
						break;

					result.Add( movie );

					positionIndex = positionIndex + movie.Title.Length + movie.Description.Length + 2;
					file.Position = positionIndex;
					file.Read( buffer, 0, buffer.Length );
				}
			}

			using ( FileStream file = new FileStream( valuesFilePath, FileMode.Open, FileAccess.Read ) )
			{
				file.Position = valuesEntryPoint;
				byte[] buffer = new byte[23];
				file.Read( buffer, 0, buffer.Length );
				int index = 0;

				while ( MadTVMovieImporter.ValidBuffer( buffer ) )
				{
					if ( result.Count <= index )
						break;

					var currentMovie = result[index];
					MadTVMovieImporter.FillMovieFromBuffer( buffer, currentMovie );

					file.Read( buffer, 0, buffer.Length );
					index++;
				}
			}

			var kul = result.FirstOrDefault( x => x.Title.StartsWith( "Im " ) );
		}



		//static void WriteMovies()
		//{
		//    using ( FileStream file = new FileStream( "test2.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write ) )
		//    {
		//        byte[] data = new byte[23];

		//        data[0] = 2;	//Genre 1-10
		//        data[1] = 0;	//leer (35 #53)
		//        data[2] = 4;	//Pause 1-5
		//        data[3] = 0;	//leer (35 #53)
		//        data[4] = 3;	//Blocks 1-7
		//        data[5] = 0;	//leer
		//        data[6] = 57 + 64;	//Grade    Quality: 32-63   Auktion: +64
		//        data[7] = 0;	//Rated    Default: 0       X-Rated: 8
		//        data[8] = 0;	//leer (Cost) 10 #16
		//        data[9] = 0;	//leer (Cost) 09 #09
		//        data[10] = 0;	//leer (Cost) 05 #05
		//        data[11] = 0;	//leer (Cost) 00 #00
		//        data[12] = 0;	//leer 1
		//        data[13] = 0;	//leer 2
		//        data[14] = 0;	//leer 3
		//        data[15] = 0;	//leer 4
		//        data[16] = 0;	//leer 5
		//        data[17] = 0;	//leer 6
		//        data[18] = 0;	//leer 7
		//        data[19] = 0;	//Country 0-9
		//        data[20] = 80;	//Year 0-99
		//        data[21] = 4;	//Critics 0-10
		//        data[22] = 10;	//Box 0-10

		//        file.Write( data, 0, data.Length );
		//    }
		//}
	}


}
