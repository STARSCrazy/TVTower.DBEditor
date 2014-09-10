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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("gdfdf");
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewAdvertising = new System.Windows.Forms.ListView();
            this.colHeaderTitleDE = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToDatabaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
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
            this.mainSplitContainer.Panel1.Controls.Add(this.listViewAdvertising);
            this.mainSplitContainer.Panel1.Controls.Add(this.menuStrip1);
            this.mainSplitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.AccessibleName = "FormPanel";
            this.mainSplitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mainSplitContainer.Size = new System.Drawing.Size(1015, 664);
            this.mainSplitContainer.SplitterDistance = 318;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // listViewAdvertising
            // 
            this.listViewAdvertising.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeaderTitleDE});
            this.listViewAdvertising.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAdvertising.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listViewAdvertising.Location = new System.Drawing.Point(0, 24);
            this.listViewAdvertising.Name = "listViewAdvertising";
            this.listViewAdvertising.Size = new System.Drawing.Size(1015, 294);
            this.listViewAdvertising.TabIndex = 0;
            this.listViewAdvertising.UseCompatibleStateImageBehavior = false;
            this.listViewAdvertising.View = System.Windows.Forms.View.Details;
            // 
            // colHeaderTitleDE
            // 
            this.colHeaderTitleDE.Text = "Titel (de)";
            this.colHeaderTitleDE.Width = 170;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1015, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToDatabaseMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // connectToDatabaseMenuItem
            // 
            this.connectToDatabaseMenuItem.Name = "connectToDatabaseMenuItem";
            this.connectToDatabaseMenuItem.Size = new System.Drawing.Size(181, 22);
            this.connectToDatabaseMenuItem.Text = "Verbinde Datenbank";
            this.connectToDatabaseMenuItem.Click += new System.EventHandler(this.connectToDatabaseMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 664);
            this.Controls.Add(this.mainSplitContainer);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ListView listViewAdvertising;
        private System.Windows.Forms.ColumnHeader colHeaderTitleDE;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToDatabaseMenuItem;
    }
}

