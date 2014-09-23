using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeKnight.Core;

namespace TVTower.DBEditorGUI.Controls
{
    public class TVTGenericColumnHeader<T> : TVTColumnHeader where T : IIdEntity
    {
        public Func<T, object> GetValueFunc { get; set; }

        public TVTGenericColumnHeader() : base()
        {
        }

        public TVTGenericColumnHeader(int imageIndex) : base(imageIndex)
        {
        }

        public TVTGenericColumnHeader( string imageKey )
            : base( imageKey )
        {
        }

        public virtual object GetValue(T entity)
        {
            return GetValueFunc(entity);
        }

        public virtual string GetValueString( T entity )
        {
            var result = GetValueFunc( entity );
            if ( result != null )
                return result.ToString();
            else
                return null;
        }
    }
}
