using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSqlAny.Logic;
using WpfSqlAny.Logic.SupportTypes;
using WpfSqlAny.Windows;

namespace WpfSqlAny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        #region SupportClass
        private class TablesProperies
        {
            public string Name { get; set; }
        }
        #endregion


        private IDbAdapter _adapter;
        private DataBaseType _selectedDatabaseType;

        public MainWindow()
        {
            InitializeComponent();

            //Subscribing as example from C# code. But it can be added from xaml
            (this.toolBarGrid.Children[1] as Button).Click += Connect_Click;
            (this.toolBarGrid.Children[2] as Button).Click += CreateTable_Click;

            tabl.MouseDoubleClick += CatalogsRow_MouseDoubleClick;

            var databaseTypeNames = Enum.GetNames(typeof(DataBaseType));
            DataBaseTypeComboBox.ItemsSource = databaseTypeNames;
            DataBaseTypeComboBox.DataContextChanged += DataBaseTypeComboBox_DataContextChanged;

            _selectedDatabaseType = (DataBaseType)DataBaseTypeComboBox.SelectedIndex;
        }

        private void DataBaseTypeComboBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _selectedDatabaseType = (DataBaseType)DataBaseTypeComboBox.SelectedIndex;
        }

        private void InitDB()
        {
            //_adapter = new SqlLiteAdapter();
            //_adapter.Init("MySQL.sqlite");
            _adapter = new PostgreAdapter();
            _adapter.Init("MySQL.sqlite");


            ChangeStatusField(ConnectionStatusType.Disconnected);

            _adapter.StatusChanged += ChangeStatusField;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            var createOrOpenWindow = new CreateOrOpenDbWindow(_selectedDatabaseType);
            createOrOpenWindow.ShowDialog();

            //InitDB();



            //if (_adapter == null)
            //{
            //    App.ErrorMessage("Not exist adapter instance");
            //    return;
            //}
            //_adapter.ConnectToDB();

            RefreshCatalogsFromDB(_adapter.GetTablesNames());
        }

        private void RefreshCatalogsFromDB(DataTable dt)
        {
            var dtView = dt.AsDataView();
            //tabl.ItemsSource = dtView;
            tabl.Items.Clear();
            tabl.Columns.Clear();
            tabl.IsReadOnly = true;

            var dataGridTemplate = new DataGridTemplateColumn();
            var factory = new FrameworkElementFactory(typeof(Button));
            factory.SetValue(Button.ContentProperty, "Delete Table");
            factory.AddHandler(Button.ClickEvent, new RoutedEventHandler((o, e) => OnDeleteTable(o, e)));
            var cellTemplate = new DataTemplate();
            cellTemplate.VisualTree = factory;
            dataGridTemplate.Header = "action";
            dataGridTemplate.CellTemplate = cellTemplate;

            var fieldNames = new string[dt.Columns.Count];
            for (var i = 0; i < dt.Columns.Count; i++)
            {
                var columnHeader = dt.Columns[i].ToString();
                Binding bind = new Binding("Name");
                bind.Mode = BindingMode.OneWay;
                //for (var j = 0; j < dtView.Count; j++)
                //{
                //    var rowView = dtView[j];
                //    var row = rowView.Row;
                //    tabl.Items.Add(row.ItemArray);
                //    bind.Source = row[dt.Columns[i]];
                //}

                var dgt = new DataGridTemplateColumn();
                var fef = new FrameworkElementFactory(typeof(TextBlock));
                fef.SetBinding(TextBlock.TextProperty, bind);


                dgt.Header = columnHeader;

                var dTemplate = new DataTemplate();
                
                dTemplate.VisualTree = fef;
                dgt.CellTemplate = dTemplate;
                tabl.Columns.Add(dgt);
                fieldNames[i] = dgt.Header.ToString();
            }

            List<object> items = new List<object>();
            for (var i = 0; i < dtView.Count; i++)
            {
                var rowView = dtView[i];
                var row = rowView.Row;

                var varObj = App.CreateTypeWithRandomStringFields(row.ItemArray, fieldNames);
                items.Add(varObj);
                //var vr = new { name = row.ItemArray[0], rootpage = row.ItemArray[2], sql = row.ItemArray[1] };
                //tabl.Items.Add(vr);
                //tabl.Items.Add(varObj);

                //tabl.Items.Add(new
                //{
                //    @type = row.ItemArray[0],
                //    name = row.ItemArray[1],
                //    tblname = row.ItemArray[2],
                //    rootpage = row.ItemArray[3],
                //    sql = row.ItemArray[4]
                //});
                tabl.Items.Add(new TablesProperies { Name = row.ItemArray[0].ToString() });
                //tabl.Items.Add(row.ItemArray[0]);
            }
            //tabl.ItemsSource = items;
            tabl.Columns.Add(dataGridTemplate);
        }

        private void OnDeleteTable(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }

            var btn = (Button)sender;

            var cell = ItemsControl.ContainerFromElement((DataGrid)btn.Parent,
                        e.OriginalSource as DependencyObject) as DataGridCell;

            //int rowIndex = tabl.Items.IndexOf(tabl.CurrentItem);
            var tableName = (cell.DataContext as TablesProperies).Name;//((DataRowView)row.Item)[0].ToString();
            _adapter.DeleteTable(tableName);
            RefreshCatalogsFromDB(_adapter.GetTablesNames());
        }

        private void CatalogsRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }

            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                    e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            var selectedTableWindow = new SelectedTableWindow();
            selectedTableWindow.ShowWithUpdateInfo(_adapter, (row.Item as TablesProperies).Name);
        }

        private void ChangeStatusField(ConnectionStatusType statusType)
        {
            status.Text = statusType.ToString();
        }

        private void CreateTable_Click(object sender, RoutedEventArgs e)
        {
            if(_adapter == null)
            {
                App.ErrorMessage("Not exist adapter instance");
                return;
            }

            var createTableWindow = new CreatingTableWindow();
            createTableWindow.ShowWithCallback(CreateTableWithName);
        }

        private void CreateTableWithName(string name)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }
            _adapter.CreateTable(name);
            RefreshCatalogsFromDB(_adapter.GetTablesNames());
        }

        private bool CheckConnectionErrors()
        {
            if (_adapter == null)
            {
                App.ErrorMessage("Not found link to dbAdapter");
                return false;
            }
            if (_adapter.CurrentStatus == ConnectionStatusType.Disconnected)
            {
                App.ErrorMessage("db not conneted");
                return false;
            }
            return true;
        }

        #region IDisposable
        public void Dispose()
        {
            (this.toolBarGrid.Children[1] as Button).Click -= Connect_Click;
            (this.toolBarGrid.Children[2] as Button).Click -= CreateTable_Click;

            tabl.MouseDoubleClick -= CatalogsRow_MouseDoubleClick;

            DataBaseTypeComboBox.DataContextChanged -= DataBaseTypeComboBox_DataContextChanged; ;
        }
        #endregion
    }
}
