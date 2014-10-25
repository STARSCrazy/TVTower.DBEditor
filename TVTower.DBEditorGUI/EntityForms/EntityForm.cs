using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeKnight.Core;
using TVTower.DBEditorGUI.Events;

namespace TVTower.DBEditorGUI.EntityForms
{
    public partial class EntityForm : UserControl, IFormControl
    {
        public event EntitySaveEventHandler EntitySave;

        public EntityForm()
        {
            InitializeComponent();
        }

        public Control ToControl()
        {
            return this;
        }

        protected virtual void OnSaveEntity( EntitySaveEventArgs e )
        {
            if ( EntitySave != null )
                EntitySave( this, e );
        }
    }
}
