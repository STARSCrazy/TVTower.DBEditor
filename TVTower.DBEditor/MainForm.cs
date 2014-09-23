using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TVTower.Entities;
using TVTower.Import;
using TVTower.Xml;
using TVTower.Importer.MadTV;

namespace TVTower.DBEditor
{
	public partial class MainForm : Form
	{
		//private SortedBindingList<TVTMovieExtended> movieDataList;
		//private SortedBindingList<TVTPerson> personDataList;
		private TVTBindingListDatabaseOld database;

		private Thread WorkerThread;
		private System.Windows.Forms.Timer timer;
		private MovieImporter importer;

		public MainForm()
		{
			InitializeComponent();

			database = new TVTBindingListDatabaseOld();
			database.Initialize();

			var movieDataList = new SortedBindingList<TVTProgramme>();

			// Allow new parts to be added, but not removed once committed.        
			movieDataList.AllowNew = true;
			movieDataList.AllowRemove = true;

			// Raise ListChanged events when new parts are added.
			movieDataList.RaiseListChangedEvents = true;

			movieDataList.AllowEdit = true;

			movieDataGrid.DataSource = movieDataList;
			database.ProgrammeData = movieDataList;



			var personDataList = new SortedBindingList<TVTPerson>();

			// Allow new parts to be added, but not removed once committed.        
			personDataList.AllowNew = true;
			personDataList.AllowRemove = true;

			// Raise ListChanged events when new parts are added.
			personDataList.RaiseListChangedEvents = true;

			personDataList.AllowEdit = true;

			actorDataGrid.DataSource = personDataList;
			database.PersonData = personDataList;
		}





		private void btnExcelExport_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "ExportDatabaseFull.xml", DatabaseVersion.V3, DataStructure.Full );
		}

		private void btnMerge_Click( object sender, EventArgs e )
		{
			if ( movieDataGrid.SelectedRows.Count == 2 )
			{
				var movie1 = movieDataGrid.SelectedRows[0].DataBoundItem as TVTProgramme;
				var movie2 = movieDataGrid.SelectedRows[1].DataBoundItem as TVTProgramme;

				TVTProgramme imported = null;
				TVTProgramme fake = null;

				if ( movie1.TmdbId > 0 )
				{
					imported = movie1;
					fake = movie2;
				}

				if ( movie2.TmdbId > 0 )
				{
					if ( imported != null )
						throw new Exception();

					imported = movie2;
					fake = movie1;
				}

				if ( imported == null )
					throw new Exception();

				if ( fake == null )
					throw new Exception();

				imported.FakeTitleDE = fake.FakeTitleDE;
				imported.DescriptionDE = fake.DescriptionDE;

				imported.MovieAdditional.PriceRateOld = fake.PriceMod;
				imported.MovieAdditional.CriticRateOld = fake.CriticsRate;
				imported.MovieAdditional.SpeedRateOld = fake.ViewersRate;
				imported.MovieAdditional.BoxOfficeRateOld = fake.BoxOfficeRate;

				//foreach ( var actor in imported.Participants )
				//{
				//    if ( string.IsNullOrEmpty( actor.Info ) )
				//        actor.Info = fake.Participants.Select( x => x.FakeFullName ).ToContentString( " | " );
				//    else
				//        actor.Info = actor.Info + " | " + fake.Participants;
				//}


				//var director = imported.Director;
				//if ( string.IsNullOrEmpty( director.Info ) )
				//    director.Info = fake.Director.FakeFullName;
				//else
				//    director.Info = director.Info + " | " + fake.Director.FakeFullName;

				database.ProgrammeData.Remove( fake );
			}
		}

		private void btnSaveOldFormat_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "ExportDatabaseFakeDataOldFormat.xml", DatabaseVersion.V2, DataStructure.FakeData );
		}

		private void miLoadDB_Click( object sender, EventArgs e )
		{
			openFileDialog.InitialDirectory = Environment.CurrentDirectory;

			if ( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				database.Initialize();

				XmlPersister persister = new XmlPersister();
				var tempDatabase = new TVTBindingListDatabaseOld();
				tempDatabase.Initialize();
				persister.LoadXML( openFileDialog.FileName, tempDatabase );
				database.AddPeople( tempDatabase.GetAllPeople() );
				database.AddProgrammes( tempDatabase.GetAllProgrammes( true ) );
			}
		}

		private void miImportDB_Click( object sender, EventArgs e )
		{
			openFileDialog.InitialDirectory = Environment.CurrentDirectory;

			if ( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				XmlPersister persister = new XmlPersister();
				var tempDatabase = new TVTBindingListDatabaseOld();
				tempDatabase.Initialize();
				persister.LoadXML( openFileDialog.FileName, tempDatabase );
				database.AddPeople( tempDatabase.GetAllPeople() );
				database.AddProgrammes( tempDatabase.GetAllProgrammes( true ) );
			}
		}

		private void miImportFromWebDB_Click( object sender, EventArgs e )
		{
			importer = new MovieImporter();
			importer.Database = database;
			importer.Start();
		}

		private void miSaveAll_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "TVTDatabaseV3Full.xml", DatabaseVersion.V3, DataStructure.Full );
		}

		private void miExportV3Original_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "ExportTVTDatabaseV3Original.xml", DatabaseVersion.V3, DataStructure.OriginalData );
		}

		private void miExportV3Fake_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "ExportTVTDatabaseV3Fake.xml", DatabaseVersion.V3, DataStructure.FakeData );
		}

		private void miExportV2Original_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "ExportTVTDatabaseV2Original.xml", DatabaseVersion.V2, DataStructure.OriginalData );
		}

		private void miExportV2Fake_Click( object sender, EventArgs e )
		{
			XmlPersister persister = new XmlPersister();
			persister.SaveXML( database, "ExportTVTDatabaseV2Fake.xml", DatabaseVersion.V2, DataStructure.FakeData );
		}

		private void miCloseWindow_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void btnMerge_Click_1( object sender, EventArgs e )
		{
			if ( movieDataGrid.SelectedRows.Count == 2 )
			{
				var movie1 = movieDataGrid.SelectedRows[0].DataBoundItem as TVTProgramme;
				var movie2 = movieDataGrid.SelectedRows[1].DataBoundItem as TVTProgramme;

				TVTProgramme imported = null;
				TVTProgramme fake = null;

				if ( movie1.TmdbId > 0 )
				{
					imported = movie1;
					fake = movie2;
				}

				if ( movie2.TmdbId > 0 )
				{
					if ( imported != null )
						throw new Exception();

					imported = movie2;
					fake = movie1;
				}

				if ( imported == null )
					throw new Exception();

				if ( fake == null )
					throw new Exception();

				imported.FakeTitleDE = fake.FakeTitleDE;
				imported.DescriptionDE = fake.DescriptionDE;

				imported.MovieAdditional.PriceRateOld = fake.PriceMod;
				imported.MovieAdditional.CriticRateOld = fake.CriticsRate;
				imported.MovieAdditional.SpeedRateOld = fake.ViewersRate;
				imported.MovieAdditional.BoxOfficeRateOld = fake.BoxOfficeRate;

				//foreach ( var actor in imported.Participants )
				//{
				//    if ( string.IsNullOrEmpty( actor.Info ) )
				//        actor.Info = fake.Participants.Select( x => x.FakeFullName ).ToContentString( " | " );
				//    else
				//        actor.Info = actor.Info + " | " + fake.Participants;
				//}


				//var director = imported.Director;
				//if ( string.IsNullOrEmpty( director.Info ) )
				//    director.Info = fake.Director.FakeFullName;
				//else
				//    director.Info = director.Info + " | " + fake.Director.FakeFullName;

				database.ProgrammeData.Remove( fake );
			}
		}

		private void btnSaveAll_Click( object sender, EventArgs e )
		{
			miSaveAll_Click( sender, e );
		}

		private void miImportFromMadTVDB_Click( object sender, EventArgs e )
		{
			var import = new MadTVImport();
			import.ImportMovies( "SOURCE1.RSC", 478040, "SOURCE1.RSC", 439856 );
		}
	}
}
