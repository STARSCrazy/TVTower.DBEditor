using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTower.Entities;
using CodeKnight.Core;

namespace TVTower.DBEditorGUI.EntityForms
{
    public abstract class EntityForm<T> : UserControl
        where T : IIdEntity
    {
        public EntityForm()
        {
        }

        public abstract void LoadEntity( T entity );
    }
}
