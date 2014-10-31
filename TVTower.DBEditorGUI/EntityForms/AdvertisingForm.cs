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
using TVTower.DBEditorGUI.Events;
using TVTower.DBEditorGUI.Util;

namespace TVTower.DBEditorGUI.EntityForms
{
    public partial class AdvertisingForm : EntityForm, IEntityForm<TVTAdvertising>
    {
        public TVTAdvertising CurrentEntity { get; set; }

        public AdvertisingForm()
        {
            InitializeComponent();

            cbTargetGroups.AddItem( "(keine)", TVTTargetGroup.All );
            cbTargetGroups.AddItem( "Kinder", TVTTargetGroup.Children );
            cbTargetGroups.AddItem( "Jugendliche", TVTTargetGroup.Teenagers );
            cbTargetGroups.AddItem( "Hausfrauen", TVTTargetGroup.HouseWifes );
            cbTargetGroups.AddItem( "Arbeitnehmer", TVTTargetGroup.Employees );
            cbTargetGroups.AddItem( "Arbeitslose", TVTTargetGroup.Unemployed );
            cbTargetGroups.AddItem( "Manager", TVTTargetGroup.Manager );
            cbTargetGroups.AddItem( "Rentner", TVTTargetGroup.Pensioners );
            cbTargetGroups.AddItem( "Frauen", TVTTargetGroup.Women );
            cbTargetGroups.AddItem( "Männer", TVTTargetGroup.Men );

            var genres = new List<CheckedListBoxItem>();
            genres.Add( new CheckedListBoxItem( "Action", TVTProgrammeGenre.Action ) );
            genres.Add( new CheckedListBoxItem( "Abenteuer", TVTProgrammeGenre.Adventure ) );
            genres.Add( new CheckedListBoxItem( "Krimi", TVTProgrammeGenre.Crime ) );
            genres.Add( new CheckedListBoxItem( "Komödie", TVTProgrammeGenre.Comedy ) );
            genres.Add( new CheckedListBoxItem( "Dokumentation", TVTProgrammeGenre.Documentary ) );
            genres.Add( new CheckedListBoxItem( "Erotik", TVTProgrammeGenre.Erotic ) );
            genres.Add( new CheckedListBoxItem( "Familie", TVTProgrammeGenre.Family ) );
            genres.Add( new CheckedListBoxItem( "Fantasy", TVTProgrammeGenre.Fantasy ) );
            genres.Add( new CheckedListBoxItem( "Geschichte", TVTProgrammeGenre.History ) );
            genres.Add( new CheckedListBoxItem( "Horror", TVTProgrammeGenre.Horror ) );
            genres.Add( new CheckedListBoxItem( "Monumental", TVTProgrammeGenre.Monumental ) );
            genres.Add( new CheckedListBoxItem( "Mystery", TVTProgrammeGenre.Mystery ) );
            genres.Add( new CheckedListBoxItem( "Liebesfilm", TVTProgrammeGenre.Romance ) );
            genres.Add( new CheckedListBoxItem( "Sci-Fi", TVTProgrammeGenre.SciFi ) );
            genres.Add( new CheckedListBoxItem( "Thriller", TVTProgrammeGenre.Thriller ) );
            genres.Add( new CheckedListBoxItem( "Western", TVTProgrammeGenre.Western ) );

            genres.Add( new CheckedListBoxItem( "Show", TVTProgrammeGenre.Undefined_Show ) );
            genres.Add( new CheckedListBoxItem( "Event", TVTProgrammeGenre.Undefined_Event ) );

            cAllowedGenres.Items.Clear();
            cAllowedGenres.Items.AddRange( genres.ToArray() );

            cProhibitedGenres.Items.Clear();
            cProhibitedGenres.Items.AddRange( genres.ToArray() );
        }

        public void LoadEntity( TVTAdvertising entity )
        {
            CurrentEntity = entity;

            cTitleDE.Text = entity.TitleDE;
            cDescriptionDE.Text = entity.DescriptionDE;

            cTitleEN.Text = entity.TitleEN;
            cDescriptionEN.Text = entity.DescriptionEN;

            cValidFrom.Value = entity.ValidFrom < 1985 ? 1985 : entity.ValidFrom;
            cValidTill.Value = entity.ValidTill < 1985 ? 2100 : entity.ValidTill;

            rbFix.Checked = ( entity.FixPrice );
            rbDynamic.Checked = ( !entity.FixPrice );

            cMinImage.Value = entity.MinImage;
            cMinAudience.Value = (decimal)entity.MinAudience;

            cRepetitions.Value = entity.Repetitions;
            cDuration.Value = entity.Duration;
            cProfit.Value = entity.Profit;
            cPenalty.Value = entity.Penalty;

            cbTargetGroups.SetItem( (int)entity.TargetGroup );

            cAllowedGenres.CheckItems( entity.AllowedGenres );
            cProhibitedGenres.CheckItems( entity.ProhibitedGenres );
        }

        public void SaveEntity()
        {
            CurrentEntity.TitleDE = cTitleDE.Text;
            CurrentEntity.DescriptionDE = cDescriptionDE.Text;

            CurrentEntity.TitleEN = cTitleEN.Text;
            CurrentEntity.DescriptionEN = cDescriptionEN.Text;

            CurrentEntity.ValidFrom = Convert.ToInt32( cValidFrom.Value );
            CurrentEntity.ValidTill = Convert.ToInt32( cValidTill.Value );

            CurrentEntity.MinImage = Convert.ToInt32( cMinImage.Value );

            CurrentEntity.FixPrice = rbFix.Checked;
            CurrentEntity.Repetitions = Convert.ToInt32( cRepetitions.Value );
            CurrentEntity.Duration = Convert.ToInt32( cDuration.Value );
            CurrentEntity.Profit = Convert.ToInt32( cProfit.Value );
            CurrentEntity.Penalty = Convert.ToInt32( cPenalty.Value );

            CurrentEntity.TargetGroup = cbTargetGroups.GetSelectedItemsAs<TVTTargetGroup>();

            CurrentEntity.AllowedGenres = cAllowedGenres.GetCheckedItemsAs<TVTProgrammeGenre>();
            CurrentEntity.ProhibitedGenres = cProhibitedGenres.GetCheckedItemsAs<TVTProgrammeGenre>();

            CurrentEntity.IsChanged = true;
            OnSaveEntity( new EntitySaveEventArgs( CurrentEntity ) );
        }

        private void btnSave_Click( object sender, EventArgs e )
        {
            SaveEntity();
        }



    }
}
