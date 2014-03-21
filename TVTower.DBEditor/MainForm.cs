﻿using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TVTower.Entities;
using TVTower.Import;
using TVTower.Xml;

namespace TVTower.DBEditor
{
	public partial class MainForm : Form
	{
		//private SortedBindingList<TVTMovieExtended> movieDataList;
		//private SortedBindingList<TVTPerson> personDataList;
		private TVTBindingListDatabase<TVTMovieExtended> database;

		private Thread WorkerThread;
		private System.Windows.Forms.Timer timer;
		private MovieImporter importer;

		public MainForm()
		{
			InitializeComponent();

			database = new TVTBindingListDatabase<TVTMovieExtended>();
			database.Initialize();

			var movieDataList = new SortedBindingList<TVTMovieExtended>();

			// Allow new parts to be added, but not removed once committed.        
			movieDataList.AllowNew = true;
			movieDataList.AllowRemove = true;

			// Raise ListChanged events when new parts are added.
			movieDataList.RaiseListChangedEvents = true;

			movieDataList.AllowEdit = true;

			movieDataGrid.DataSource = movieDataList;
			database.MovieData = movieDataList;



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
				var movie1 = movieDataGrid.SelectedRows[0].DataBoundItem as TVTMovieExtended;
				var movie2 = movieDataGrid.SelectedRows[1].DataBoundItem as TVTMovieExtended;

				TVTMovieExtended imported = null;
				TVTMovieExtended fake = null;

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

				imported.TitleDE = fake.TitleDE;
				imported.DescriptionDE = fake.DescriptionDE;

				imported.PriceRateOld = fake.PriceRate;
				imported.CriticRateOld = fake.CriticsRate;
				imported.SpeedRateOld = fake.SpeedRateOld;
				imported.BoxOfficeRateOld = fake.BoxOfficeRateOld;

				foreach ( var actor in imported.Actors )
				{
					if ( string.IsNullOrEmpty( actor.Info ) )
						actor.Info = fake.Actors.Select( x => x.Name ).ToContentString( " | " );
					else
						actor.Info = actor.Info + " | " + fake.Actors;
				}


				var director = imported.Director;
				if ( string.IsNullOrEmpty( director.Info ) )
					director.Info = fake.Director.Name;
				else
					director.Info = director.Info + " | " + fake.Director.Name;

				database.MovieData.Remove( fake );
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
				var tempDatabase = new TVTBindingListDatabase<TVTMovieExtended>();
				tempDatabase.Initialize();
				persister.LoadXML( openFileDialog.FileName, tempDatabase );
				database.AddPeople( tempDatabase.GetAllPeople() );
				database.AddMovies( tempDatabase.GetAllMovies() );
			}
		}

		private void miImportDB_Click( object sender, EventArgs e )
		{
			openFileDialog.InitialDirectory = Environment.CurrentDirectory;

			if ( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				XmlPersister persister = new XmlPersister();
				var tempDatabase = new TVTBindingListDatabase<TVTMovieExtended>();
				tempDatabase.Initialize();
				persister.LoadXML( openFileDialog.FileName, tempDatabase );
				database.AddPeople( tempDatabase.GetAllPeople() );
				database.AddMovies( tempDatabase.GetAllMovies() );
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
				var movie1 = movieDataGrid.SelectedRows[0].DataBoundItem as TVTMovieExtended;
				var movie2 = movieDataGrid.SelectedRows[1].DataBoundItem as TVTMovieExtended;

				TVTMovieExtended imported = null;
				TVTMovieExtended fake = null;

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

				imported.TitleDE = fake.TitleDE;
				imported.DescriptionDE = fake.DescriptionDE;

				imported.PriceRateOld = fake.PriceRate;
				imported.CriticRateOld = fake.CriticsRate;
				imported.SpeedRateOld = fake.SpeedRateOld;
				imported.BoxOfficeRateOld = fake.BoxOfficeRateOld;

				foreach ( var actor in imported.Actors )
				{
					if ( string.IsNullOrEmpty( actor.Info ) )
						actor.Info = fake.Actors.Select( x => x.Name ).ToContentString( " | " );
					else
						actor.Info = actor.Info + " | " + fake.Actors;
				}


				var director = imported.Director;
				if ( string.IsNullOrEmpty( director.Info ) )
					director.Info = fake.Director.Name;
				else
					director.Info = director.Info + " | " + fake.Director.Name;

				database.MovieData.Remove( fake );
			}
		}

		private void btnSaveAll_Click( object sender, EventArgs e )
		{
			miSaveAll_Click( sender, e );
		}
	}
}
