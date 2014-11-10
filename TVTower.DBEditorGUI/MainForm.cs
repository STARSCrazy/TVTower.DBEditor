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
using CodeKnight.Core;
using TVTower.DBEditorGUI.Events;

namespace DBEditorGUI
{
    public partial class MainForm : Form
    {
        public Dictionary<ListViewKey, ListView> ListViews { get; set; }

        public AdvertisingForm DefaultAdvertisingForm = new AdvertisingForm();
        public PersonForm DefaultPersonForm = new PersonForm();

        public TabPage CurrentFormTab = null;

        public MainForm()
        {
            InitializeComponent();

            ListViews = new Dictionary<ListViewKey, ListView>();
            
            InitializeListView( ListViewKey.Advertisings, new AdvertisingListViewDefinition() );
            InitializeListView( ListViewKey.People, new PersonListViewDefinition() );

            CurrentFormTab = new TabPage();
            CurrentFormTab.Name = "CurrentFormTabPage";
            CurrentFormTab.Padding = new Padding( 3 );
            CurrentFormTab.Text = "Aktuell";
            CurrentFormTab.UseVisualStyleBackColor = true;

            tabControlForms.Controls.Add( CurrentFormTab );
        }

        public void RefreshAllViews()
        {
            foreach ( var listViewKV in ListViews )
            {
                var key = listViewKV.Key;
                var listView = listViewKV.Value;

                if ( key == ListViewKey.Advertisings )
                {
                    var advertisings = TVTEditorApplication.Instance.InternalDatabase.GetAllAdvertisings();
                    LoadViewItems( listView, advertisings, x => x.TitleDE );
                }
                else if ( key == ListViewKey.People )
                {
                    var people = TVTEditorApplication.Instance.InternalDatabase.GetAllPeople();
                    LoadViewItems( listView, people, x => x.FullName );
                }
            }
        }

        public void LoadViewItems<T>( ListView listView, IEnumerable<T> entities, Func<T, string> textMethod )
            where T : IIdEntity
        {
            listView.Items.Clear();

            foreach ( var entity in entities )
            {
                var item = new ListViewItem();

                UpdateViewItem( listView, item, entity, textMethod );

                listView.Items.Add( item );
            }
        }

        private void UpdateViewItem<T>( ListView listView, ListViewItem item, T entity, Func<T, string> textMethod )
            where T : IIdEntity
        {
            item.Tag = entity;
            item.Text = textMethod.Invoke( entity );
            item.SubItems.Clear();            

            var colIndex = 0;

            foreach ( var column in listView.Columns )
            {
                if ( column is TVTGenericColumnHeader<T> )
                {
                    var colAd = column as TVTGenericColumnHeader<T>;

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
        }

        private void InitializeListView( ListViewKey key, IListViewDefinition listViewDefinition )
        {
            var listView = CreateListView( key, listViewDefinition.GetColumnDefinition() );
            ListViews.Add( key, listView );

            var tabPage = new TabPage();
            tabPage.Name = key.ToString() + "TabPage";
            tabPage.Padding = new Padding( 3 );
            tabPage.Text = key.ToString();
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Controls.Add( listView );

            tabControlListViews.Controls.Add( tabPage );
        }

        private void OpenForm(ListViewKey key, ITVTEntity entity)
        {
            switch (key)
            {
                case ListViewKey.Advertisings:
                    OpenFormInternal<TVTAdvertising>( DefaultAdvertisingForm, key, (TVTAdvertising)entity );
                    break;
                case ListViewKey.People:
                    OpenFormInternal<TVTPerson>( DefaultPersonForm, key, (TVTPerson)entity );
                    break;
            }
        }

        private void OpenFormInternal<T>( IEntityForm<T> form, ListViewKey key, T entity )
            where T : IIdEntity
        {            
            form.LoadEntity( entity );
            form.EntitySave += new EntitySaveEventHandler( form_EntitySave );

            if ( CurrentFormTab.Controls.Count == 0 || CurrentFormTab.Controls[0] != form )
            {
                form.Dock = DockStyle.Fill;
                form.Name = key.ToString() + "Form";

                CurrentFormTab.Controls.Clear();
                CurrentFormTab.Controls.Add( form.ToControl() );
            }
        }

        private void RefreshViewItem<T>( ListViewKey key, T entity, Func<T, string> textMethod )
            where T : IIdEntity
        {
            var listview = ListViews[key];
            var item = listview.Items.Cast<ListViewItem>().FirstOrDefault( x => object.Equals(x.Tag, entity) );
            if ( item != null )
            {
                UpdateViewItem( listview, item, entity, textMethod );
            }
        }

        void form_EntitySave( object sender, EntitySaveEventArgs e )
        {
            if ( e.Entity is TVTAdvertising )
            {
                RefreshViewItem<TVTAdvertising>( ListViewKey.Advertisings, e.Entity as TVTAdvertising, x => x.TitleDE ); //TODO: Vereinheitlichen: VOr allem Textmethod
            }

            //TVTEditorApplication.Instance.MYSQLDatabase.SaveAdvertising( (TVTAdvertising)e.Entity );
            //TVTEditorApplication.Instance.MYSQLDatabase.WriteChangesToDatabase( TVTEditorApplication.Instance.InternalDatabase );
        }

        private ListView CreateListView( ListViewKey key, List<TVTColumnHeader> columns )
        {
            var view = new ListView();
            view.Dock = DockStyle.Fill;
            view.Name = key.ToString() + "View";
            view.UseCompatibleStateImageBehavior = false;
            view.View = View.Details;
            view.FullRowSelect = true;
            view.FullRowSelect = true;
            view.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler( view_ItemSelectionChanged );
            view.Tag = key;

            if ( columns != null )
                view.Columns.AddRange( columns.ToArray() );

            return view;
        }

        void view_ItemSelectionChanged( object sender, ListViewItemSelectionChangedEventArgs e )
        {
            if ( e.IsSelected && e.Item != null && e.Item.Tag is ITVTEntity )
            {
                var key = (ListViewKey)((ListView)sender).Tag;

                var entity = (ITVTEntity)e.Item.Tag;

                OpenForm(key, entity);
            }
        }

        private void connectToDatabaseMenuItem_Click( object sender, EventArgs e )
        {
            TVTEditorApplication.Instance.ConnectToMySQLDatabase( Settings.Default.DBConnection );
            RefreshAllViews();
        }

        private void loadXMLMenuItem_Click( object sender, EventArgs e )
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "XML-Datei (*.xml)|*.xml|Alle Dateien (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = true;
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    TVTEditorApplication.Instance.LoadXMLFile( dialog.FileName );
                    RefreshAllViews();
                break;
            }
        }

        private void saveXMLMenuItem_Click( object sender, EventArgs e )
        {
            TVTEditorApplication.Instance.SaveXMLFile();
        }
    }
}
