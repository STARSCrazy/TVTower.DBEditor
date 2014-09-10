using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TVTower.SQL;
using TVTower.Entities;

namespace DBEditorGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var database = new TVTBindingListDatabase();
            //database.Initialize();

            //using ( var connection = TVTSQLSession.GetSession() )
            //{
            //    var movies = TVTCommandsV2.LoadMoviesOldV2( connection );

            //    OldV2Converter.Convert( movies, database );

            //    TVTCommandsV2.LoadFakesForPeople( connection, database.GetAllPeople() );

            //    OldV2Converter.RefreshMovieDescriptions( database );
            //    OldV2Converter.FakePersonNames( database );

            //    var ads = TVTCommandsV2.LoadAdsOldV2( connection );
            //    OldV2Converter.Convert( ads, database );

            //    var news = TVTCommandsV2.LoadNewsOldV2( connection );
            //    OldV2Converter.Convert( news, database );

            //    database.RefreshPersonProgrammeCount();
            //    database.RefreshReferences();
            //    database.RefreshStatus();
            //}
            //TVTCommandsV3.Read<TVTAdvertising>()

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainForm() );
        }
    }
}
