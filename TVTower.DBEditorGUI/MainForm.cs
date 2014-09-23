using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TVTower.DBEditorGUI.EntityForms;
using TVTower.DBEditorGUI;
using TVTower.DBEditorGUI.Properties;
using TVTower.Entities;
using TVTower.DBEditorGUI.ListViewDefinitions;
using TVTower.DBEditorGUI.Controls;

namespace DBEditorGUI
{
    public partial class MainForm : Form
    {
        public Dictionary<ListViewKey, ListView> ListViews { get; set; }

        public AdvertisingForm advertisingForm = new AdvertisingForm();

        public MainForm()
        {
            InitializeComponent();

            ListViews = new Dictionary<ListViewKey, ListView>();
            
            InitializeListView( ListViewKey.Advertisings, new AdvertisingListViewDefinition() );
        }

        public void RefreshAllViews()
        {
            foreach ( var listViewKV in ListViews )
            {
                var key = listViewKV.Key;
                var listView = listViewKV.Value;

                listView.Items.Clear();

                var advertisings = TVTEditorApplication.Instance.InternalDatabase.GetAllAdvertisings();

                foreach ( var entity in advertisings )
                {
                    var item = new ListViewItem();
                    item.Tag = entity;
                    item.Text = entity.TitleDE;
                    item.SubItems.Clear();

                    var colIndex = 0;

                    foreach ( var column in listView.Columns )
                    {                        
                        if ( column is TVTGenericColumnHeader<TVTAdvertising> )
                        {
                            var colAd = column as TVTGenericColumnHeader<TVTAdvertising>;

                            ListViewItem.ListViewSubItem subItem = null;

                            if ( item.SubItems.Count >= colIndex + 1 )
                                subItem = item.SubItems[0];
                            else
                            {
                                subItem = new ListViewItem.ListViewSubItem();
                                item.SubItems.Add( subItem );
                            }

                            subItem.Text = colAd.GetValueString( entity );                            
                            
                            colIndex++;
                        }
                    }

                    listView.Items.Add( item );
                }
            }
        }

        private void InitializeListView( ListViewKey key, IListViewDefinition listViewDefinition )
        {
            var listView = CreateListView( key, listViewDefinition.GetColumnDefinition() );
            ListViews.Add( ListViewKey.Advertisings, listView );

            var tabPage = new TabPage();
            tabPage.Name = key.ToString() + "TabPage";
            tabPage.Padding = new Padding( 3 );
            tabPage.Text = key.ToString();
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Controls.Add( listView );

            tabControlListViews.Controls.Add( tabPage );
        }

        private ListView CreateListView( ListViewKey key, List<TVTColumnHeader> columns )
        {
            var view = new ListView();
            view.Dock = DockStyle.Fill;
            view.Name = key.ToString() + "View";
            view.UseCompatibleStateImageBehavior = false;
            view.View = View.Details;
            view.FullRowSelect = true;

            if ( columns != null )
                view.Columns.AddRange( columns.ToArray() );

            return view;
        }

        private void connectToDatabaseMenuItem_Click( object sender, EventArgs e )
        {
            TVTEditorApplication.Instance.ConnectToMySQLDatabase( Settings.Default.DBConnection );
            RefreshAllViews();
        }
    }
}
