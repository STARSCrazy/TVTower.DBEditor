using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeKnight.Core;

namespace TVTower.DBEditorGUI.Events
{
    public class EntitySaveEventArgs : EventArgs
    {
        public IIdEntity Entity { get; private set; }

        public EntitySaveEventArgs( IIdEntity entity )
        {
            Entity = entity;
        }        
    }
}
