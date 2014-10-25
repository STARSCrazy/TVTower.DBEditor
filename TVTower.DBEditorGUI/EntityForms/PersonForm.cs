using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTower.Entities;

namespace TVTower.DBEditorGUI.EntityForms
{

    public partial class PersonForm : EntityForm, IEntityForm<TVTPerson>
    {
        public PersonForm()
        {
            InitializeComponent();
        }
        public void LoadEntity( TVTPerson entity )
        {
            cFirstName.Text = entity.FirstName;
            cLastName.Text = entity.LastName;
        }
    }
}
