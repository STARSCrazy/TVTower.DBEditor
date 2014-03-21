namespace TVTower.DBEditor
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miLoadDB = new System.Windows.Forms.ToolStripMenuItem();
			this.miImportDB = new System.Windows.Forms.ToolStripMenuItem();
			this.miImportFromWebDB = new System.Windows.Forms.ToolStripMenuItem();
			this.miSaveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.exportDatenbankV3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miExportV3Original = new System.Windows.Forms.ToolStripMenuItem();
			this.miExportV3Fake = new System.Windows.Forms.ToolStripMenuItem();
			this.exportDatenbankV2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miExportV2Original = new System.Windows.Forms.ToolStripMenuItem();
			this.miExportV2Fake = new System.Windows.Forms.ToolStripMenuItem();
			this.miCloseWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnSaveAll = new System.Windows.Forms.ToolStripButton();
			this.btnMerge = new System.Windows.Forms.ToolStripButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.actorDataGrid = new System.Windows.Forms.DataGridView();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.movieDataGrid = new System.Windows.Forms.DataGridView();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.actorDataGrid)).BeginInit();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.movieDataGrid)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			this.openFileDialog.Filter = "xml|*.xml";
			this.openFileDialog.RestoreDirectory = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(982, 24);
			this.menuStrip1.TabIndex = 11;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// dateiToolStripMenuItem
			// 
			this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadDB,
            this.miImportDB,
            this.miImportFromWebDB,
            this.toolStripSeparator1,
            this.miSaveAll,
            this.toolStripSeparator2,
            this.exportDatenbankV3ToolStripMenuItem,
            this.exportDatenbankV2ToolStripMenuItem,
            this.toolStripSeparator3,
            this.miCloseWindow});
			this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
			this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.dateiToolStripMenuItem.Text = "Datei";
			// 
			// miLoadDB
			// 
			this.miLoadDB.Name = "miLoadDB";
			this.miLoadDB.Size = new System.Drawing.Size(252, 22);
			this.miLoadDB.Text = "Lade Datenbank";
			this.miLoadDB.Click += new System.EventHandler(this.miLoadDB_Click);
			// 
			// miImportDB
			// 
			this.miImportDB.Name = "miImportDB";
			this.miImportDB.Size = new System.Drawing.Size(252, 22);
			this.miImportDB.Text = "Importiere Datenbank";
			this.miImportDB.Click += new System.EventHandler(this.miImportDB_Click);
			// 
			// miImportFromWebDB
			// 
			this.miImportFromWebDB.Name = "miImportFromWebDB";
			this.miImportFromWebDB.Size = new System.Drawing.Size(252, 22);
			this.miImportFromWebDB.Text = "Importiere aus Web-Datenbanken";
			this.miImportFromWebDB.Click += new System.EventHandler(this.miImportFromWebDB_Click);
			// 
			// miSaveAll
			// 
			this.miSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("miSaveAll.Image")));
			this.miSaveAll.Name = "miSaveAll";
			this.miSaveAll.Size = new System.Drawing.Size(252, 22);
			this.miSaveAll.Text = "Speichern (alle Daten)";
			this.miSaveAll.Click += new System.EventHandler(this.miSaveAll_Click);
			// 
			// exportDatenbankV3ToolStripMenuItem
			// 
			this.exportDatenbankV3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExportV3Original,
            this.miExportV3Fake});
			this.exportDatenbankV3ToolStripMenuItem.Name = "exportDatenbankV3ToolStripMenuItem";
			this.exportDatenbankV3ToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
			this.exportDatenbankV3ToolStripMenuItem.Text = "Export Datenbank V3";
			// 
			// miExportV3Original
			// 
			this.miExportV3Original.Name = "miExportV3Original";
			this.miExportV3Original.Size = new System.Drawing.Size(152, 22);
			this.miExportV3Original.Text = "Original-Daten";
			this.miExportV3Original.Click += new System.EventHandler(this.miExportV3Original_Click);
			// 
			// miExportV3Fake
			// 
			this.miExportV3Fake.Name = "miExportV3Fake";
			this.miExportV3Fake.Size = new System.Drawing.Size(152, 22);
			this.miExportV3Fake.Text = "Fake-Daten";
			this.miExportV3Fake.Click += new System.EventHandler(this.miExportV3Fake_Click);
			// 
			// exportDatenbankV2ToolStripMenuItem
			// 
			this.exportDatenbankV2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExportV2Original,
            this.miExportV2Fake});
			this.exportDatenbankV2ToolStripMenuItem.Name = "exportDatenbankV2ToolStripMenuItem";
			this.exportDatenbankV2ToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
			this.exportDatenbankV2ToolStripMenuItem.Text = "Export Datenbank V2";
			// 
			// miExportV2Original
			// 
			this.miExportV2Original.Name = "miExportV2Original";
			this.miExportV2Original.Size = new System.Drawing.Size(152, 22);
			this.miExportV2Original.Text = "Original-Daten";
			this.miExportV2Original.Click += new System.EventHandler(this.miExportV2Original_Click);
			// 
			// miExportV2Fake
			// 
			this.miExportV2Fake.Name = "miExportV2Fake";
			this.miExportV2Fake.Size = new System.Drawing.Size(152, 22);
			this.miExportV2Fake.Text = "Fake-Daten";
			this.miExportV2Fake.Click += new System.EventHandler(this.miExportV2Fake_Click);
			// 
			// miCloseWindow
			// 
			this.miCloseWindow.Name = "miCloseWindow";
			this.miCloseWindow.Size = new System.Drawing.Size(252, 22);
			this.miCloseWindow.Text = "Schließen";
			this.miCloseWindow.Click += new System.EventHandler(this.miCloseWindow_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveAll,
            this.btnMerge});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(982, 25);
			this.toolStrip1.TabIndex = 12;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnSaveAll
			// 
			this.btnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAll.Image")));
			this.btnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSaveAll.Name = "btnSaveAll";
			this.btnSaveAll.Size = new System.Drawing.Size(23, 22);
			this.btnSaveAll.Text = "toolStripButton1";
			this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
			// 
			// btnMerge
			// 
			this.btnMerge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnMerge.Image = ((System.Drawing.Image)(resources.GetObject("btnMerge.Image")));
			this.btnMerge.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnMerge.Name = "btnMerge";
			this.btnMerge.Size = new System.Drawing.Size(23, 22);
			this.btnMerge.Text = "toolStripButton2";
			this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click_1);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.actorDataGrid);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(950, 486);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Personen";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// actorDataGrid
			// 
			this.actorDataGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
			this.actorDataGrid.AllowUserToOrderColumns = true;
			this.actorDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.actorDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.actorDataGrid.Location = new System.Drawing.Point(3, 3);
			this.actorDataGrid.Name = "actorDataGrid";
			this.actorDataGrid.Size = new System.Drawing.Size(944, 480);
			this.actorDataGrid.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.movieDataGrid);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(950, 604);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Filme";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// movieDataGrid
			// 
			this.movieDataGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
			this.movieDataGrid.AllowUserToOrderColumns = true;
			this.movieDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.movieDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.movieDataGrid.Location = new System.Drawing.Point(3, 3);
			this.movieDataGrid.Name = "movieDataGrid";
			this.movieDataGrid.Size = new System.Drawing.Size(944, 598);
			this.movieDataGrid.TabIndex = 2;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 52);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(958, 630);
			this.tabControl1.TabIndex = 5;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(249, 6);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(249, 6);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(249, 6);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(982, 694);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "TVTower Editor";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.actorDataGrid)).EndInit();
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.movieDataGrid)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miLoadDB;
		private System.Windows.Forms.ToolStripMenuItem miSaveAll;
		private System.Windows.Forms.ToolStripMenuItem exportDatenbankV3ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miExportV3Original;
		private System.Windows.Forms.ToolStripMenuItem miExportV3Fake;
		private System.Windows.Forms.ToolStripMenuItem exportDatenbankV2ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miExportV2Original;
		private System.Windows.Forms.ToolStripMenuItem miExportV2Fake;
		private System.Windows.Forms.ToolStripMenuItem miCloseWindow;
		private System.Windows.Forms.ToolStripMenuItem miImportDB;
		private System.Windows.Forms.ToolStripMenuItem miImportFromWebDB;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnSaveAll;
		private System.Windows.Forms.ToolStripButton btnMerge;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView actorDataGrid;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView movieDataGrid;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	}
}

