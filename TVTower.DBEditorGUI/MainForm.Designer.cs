namespace DBEditorGUI
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
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToSQLDatabaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadXMLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlListViews = new TVTower.DBEditorGUI.Controls.TVTTabControl();
            this.tabControlForms = new TVTower.DBEditorGUI.Controls.TVTTabControl();
            this.saveXMLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.tabControlListViews);
            this.mainSplitContainer.Panel1.Controls.Add(this.menuStrip1);
            this.mainSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.AccessibleName = "FormPanel";
            this.mainSplitContainer.Panel2.Controls.Add(this.tabControlForms);
            this.mainSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainSplitContainer.Size = new System.Drawing.Size(1184, 762);
            this.mainSplitContainer.SplitterDistance = 364;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadXMLMenuItem,
            this.saveXMLMenuItem,
            this.connectToSQLDatabaseMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // connectToSQLDatabaseMenuItem
            // 
            this.connectToSQLDatabaseMenuItem.Enabled = false;
            this.connectToSQLDatabaseMenuItem.Name = "connectToSQLDatabaseMenuItem";
            this.connectToSQLDatabaseMenuItem.Size = new System.Drawing.Size(228, 22);
            this.connectToSQLDatabaseMenuItem.Text = "Verbinde mit SQL-Datenbank";
            this.connectToSQLDatabaseMenuItem.Click += new System.EventHandler(this.connectToDatabaseMenuItem_Click);
            // 
            // loadXMLMenuItem
            // 
            this.loadXMLMenuItem.Name = "loadXMLMenuItem";
            this.loadXMLMenuItem.Size = new System.Drawing.Size(228, 22);
            this.loadXMLMenuItem.Text = "Öffne XML-Datenbank";
            this.loadXMLMenuItem.Click += new System.EventHandler(this.loadXMLMenuItem_Click);
            // 
            // tabControlListViews
            // 
            this.tabControlListViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlListViews.Location = new System.Drawing.Point(0, 24);
            this.tabControlListViews.Name = "tabControlListViews";
            this.tabControlListViews.SelectedIndex = 0;
            this.tabControlListViews.Size = new System.Drawing.Size(1184, 340);
            this.tabControlListViews.TabIndex = 2;
            // 
            // tabControlForms
            // 
            this.tabControlForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlForms.Location = new System.Drawing.Point(0, 0);
            this.tabControlForms.Multiline = true;
            this.tabControlForms.Name = "tabControlForms";
            this.tabControlForms.SelectedIndex = 0;
            this.tabControlForms.Size = new System.Drawing.Size(1184, 394);
            this.tabControlForms.TabIndex = 3;
            // 
            // saveXMLMenuItem
            // 
            this.saveXMLMenuItem.Name = "saveXMLMenuItem";
            this.saveXMLMenuItem.Size = new System.Drawing.Size(228, 22);
            this.saveXMLMenuItem.Text = "Speichere XML-Datenbank";
            this.saveXMLMenuItem.Click += new System.EventHandler(this.saveXMLMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.mainSplitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TVTower Editor";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.PerformLayout();
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToSQLDatabaseMenuItem;
        private TVTower.DBEditorGUI.Controls.TVTTabControl tabControlListViews;
        private TVTower.DBEditorGUI.Controls.TVTTabControl tabControlForms;
        private System.Windows.Forms.ToolStripMenuItem loadXMLMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveXMLMenuItem;
    }
}

