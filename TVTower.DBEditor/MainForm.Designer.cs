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
			this.btnImport = new System.Windows.Forms.Button();
			this.btnXMLExport = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnMerge = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.movieDataGrid = new System.Windows.Forms.DataGridView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.actorDataGrid = new System.Windows.Forms.DataGridView();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.rtConsole = new System.Windows.Forms.RichTextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.movieDataGrid)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.actorDataGrid)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnImport.Location = new System.Drawing.Point(895, 12);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 0;
			this.btnImport.Text = "Import";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnXMLExport
			// 
			this.btnXMLExport.Location = new System.Drawing.Point(321, 12);
			this.btnXMLExport.Name = "btnXMLExport";
			this.btnXMLExport.Size = new System.Drawing.Size(143, 23);
			this.btnXMLExport.TabIndex = 2;
			this.btnXMLExport.Text = "Speichern (alle Daten)";
			this.btnXMLExport.UseVisualStyleBackColor = true;
			this.btnXMLExport.Click += new System.EventHandler(this.btnExcelExport_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(12, 12);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(96, 23);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Daten aus File einlesen";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			this.openFileDialog.Filter = "xml|*.xml";
			this.openFileDialog.RestoreDirectory = true;
			// 
			// btnMerge
			// 
			this.btnMerge.Location = new System.Drawing.Point(505, 12);
			this.btnMerge.Name = "btnMerge";
			this.btnMerge.Size = new System.Drawing.Size(96, 23);
			this.btnMerge.TabIndex = 4;
			this.btnMerge.Text = "Merge";
			this.btnMerge.UseVisualStyleBackColor = true;
			this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Location = new System.Drawing.Point(12, 41);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(958, 641);
			this.tabControl1.TabIndex = 5;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.movieDataGrid);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(950, 615);
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
			this.movieDataGrid.Size = new System.Drawing.Size(944, 609);
			this.movieDataGrid.TabIndex = 2;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.actorDataGrid);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(950, 615);
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
			this.actorDataGrid.Size = new System.Drawing.Size(944, 609);
			this.actorDataGrid.TabIndex = 3;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.rtConsole);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(950, 615);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Konsole";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// rtConsole
			// 
			this.rtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtConsole.Location = new System.Drawing.Point(3, 3);
			this.rtConsole.Name = "rtConsole";
			this.rtConsole.Size = new System.Drawing.Size(944, 609);
			this.rtConsole.TabIndex = 0;
			this.rtConsole.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(982, 694);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnMerge);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnXMLExport);
			this.Controls.Add(this.btnImport);
			this.Name = "MainForm";
			this.Text = "TVTower Editor";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.movieDataGrid)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.actorDataGrid)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnXMLExport;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnMerge;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView movieDataGrid;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView actorDataGrid;
        private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.RichTextBox rtConsole;
	}
}

