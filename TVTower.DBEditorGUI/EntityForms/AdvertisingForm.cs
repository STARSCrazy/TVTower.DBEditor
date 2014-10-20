﻿using System;
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
    public partial class AdvertisingForm : EntityForm<TVTAdvertising>
    {
        public AdvertisingForm()
        {
            InitializeComponent();
        }

        public override void LoadEntity( TVTAdvertising entity )
        {
            cTitleDE.Text = entity.TitleDE;
        }
    }
}
