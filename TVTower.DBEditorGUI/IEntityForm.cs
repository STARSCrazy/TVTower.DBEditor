using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVTower.Entities;
using CodeKnight.Core;
using System.Windows.Forms;
using TVTower.DBEditorGUI.Events;

namespace TVTower.DBEditorGUI
{
    public delegate void EntitySaveEventHandler( object sender, EntitySaveEventArgs e );

    public interface IEntityForm<T> : IFormControl
        where T : IIdEntity
    {
        void LoadEntity( T entity );

        event EntitySaveEventHandler EntitySave;
    }
}
