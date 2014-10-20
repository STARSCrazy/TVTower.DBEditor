using TVTower.Entities;
namespace TVTower.DBEditorGUI.EntityForms
{
    partial class PersonForm : EntityForm<TVTPerson>
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cFirstName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.titleDELabel = new System.Windows.Forms.Label();
            this.cLastName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cFirstName
            // 
            this.cFirstName.Location = new System.Drawing.Point(9, 32);
            this.cFirstName.Name = "cFirstName";
            this.cFirstName.Size = new System.Drawing.Size(169, 20);
            this.cFirstName.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cLastName);
            this.groupBox1.Controls.Add(this.titleDELabel);
            this.groupBox1.Controls.Add(this.cFirstName);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // titleDELabel
            // 
            this.titleDELabel.AutoSize = true;
            this.titleDELabel.Location = new System.Drawing.Point(6, 16);
            this.titleDELabel.Name = "titleDELabel";
            this.titleDELabel.Size = new System.Drawing.Size(35, 13);
            this.titleDELabel.TabIndex = 1;
            this.titleDELabel.Text = "label1";
            // 
            // cLastName
            // 
            this.cLastName.Location = new System.Drawing.Point(184, 32);
            this.cLastName.Name = "cLastName";
            this.cLastName.Size = new System.Drawing.Size(169, 20);
            this.cLastName.TabIndex = 2;
            // 
            // PersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PersonForm";
            this.Size = new System.Drawing.Size(800, 350);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox cFirstName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label titleDELabel;
        private System.Windows.Forms.TextBox cLastName;
    }
}
