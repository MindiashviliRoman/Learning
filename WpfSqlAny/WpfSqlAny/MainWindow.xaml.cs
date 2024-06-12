using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
using WpfSqlAny.Windows;

namespace WpfSqlAny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDbAdapter _adapter;

        public MainWindow()
        {
            InitializeComponent();

            //Subscribing as example from C# code. But it can be added from xaml
            (this.toolBarGrid.Children[0] as Button).Click += Create_Click;
            (this.toolBarGrid.Children[1] as Button).Click += Connect_Click;

            tabl.MouseDoubleClick += CatalogsRow_MouseDoubleClick;

            InitDB();
        }

        private void InitDB()
        {
            _adapter = new SqlLiteAdapter();
            _adapter.Init("MySQL.sqlite");

            ChangeStatusField(ConnectionStatusType.Disconnected);

            _adapter.StatusChanged += ChangeStatusField;
            //_adapter.DataUpdated += UpdatedData;
            RefreshCatalogsFromDB(_adapter.GetTablesNames());
        }

        private void RefreshCatalogsFromDB(DataTable dt)
        {
            tabl.ItemsSource = dt.AsDataView();
            tabl.IsReadOnly = true;
        }

        private void CatalogsRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                    e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            //var index = row.GetIndex();
            var selectedTableWindow = new SelectedTableWindow();
            selectedTableWindow.ShowWithUpdateInfo(_adapter, ((DataRowView)row.Item)[0].ToString());
        }

        private void ChangeStatusField(ConnectionStatusType statusType)
        {
            status.Text = statusType.ToString();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if(_adapter == null)
            {
                App.ErrorMessage("Not exist adapter instance");
                return;
            }

            var createTavleWindow = new CreatingTableWindow();
            createTavleWindow.ShowWithCallback(CreateTableWithName);
        }

        private void CreateTableWithName(string name)
        {
            if (_adapter == null)
            {
                App.ErrorMessage("Not exist adapter instance");
                return;
            }
            _adapter.CreateTable(name);
            RefreshCatalogsFromDB(_adapter.GetTablesNames());
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (_adapter == null)
            {
                App.ErrorMessage("Not exist adapter instance");
                return;
            }
            _adapter.ConnectToDB();
        }

        private void ReadFromTable_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            //if (dbConn.State != ConnectionState.Open)
            //{
            //    MessageBox.Show("Open connection with database");
            //    return;
            //}

            //DeleteColumn(tabName, textBoxColumnName.Text);
            //ReadFromTable(tabName);
        }

        private void UpdatedData(DataTable data)
        {
            tabl.ItemsSource = null;
            tabl.IsReadOnly = false;
            if (data.Rows.Count == 0)
            {
                tabl.ItemsSource = data.AsDataView();
                tabl.IsReadOnly = true;
            }
            else
                App.ErrorMessage("Database is empty");
        }

        //private void CreateTable(object sender, RoutedEventArgs e)
        //{
        //    if (dbConn.State != ConnectionState.Open)
        //    {
        //        MessageBox.Show("Open connection with database");
        //        return;
        //    }
        //    AddToDBWindow toDBWindow = new AddToDBWindow();
        //    toDBWindow.Show();

        //}






        //private void ClearTable(string tName)
        //{
        //    try
        //    {
        //        sqlCmd.CommandText = "DELETE FROM " + tName;
        //        sqlCmd.ExecuteNonQuery();
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        //private void DeleteTable(string tName)
        //{
        //    try
        //    {
        //        sqlCmd.CommandText = "DROP TABLE " + tName;
        //        sqlCmd.ExecuteNonQuery();
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        //private void DeleteColumn(string tName, string colName, string sTypeCol = "TEXT")
        //{
        //    try
        //    {
        //        List<FieldParam> fieldParams = GetFieldParams(dbConn, tName);
        //        if (fieldParams != null)
        //        {
        //            bool isColNameExist = false;
        //            for (int i = 0; i < fieldParams.Count; i++)
        //            {
        //                if (fieldParams[i].Name == colName)
        //                {
        //                    fieldParams.RemoveAt(i);
        //                    isColNameExist = true;
        //                    break;
        //                }
        //            }
        //            if (isColNameExist)
        //            {
        //                string dublTabName = CreateDublOfTableByFields(tName, fieldParams);

        //                //Copy info from old Table to new
        //                string s1 = "INSERT INTO " + dublTabName + "(" + fieldParams[0].Name;
        //                string s2 = " SELECT " + fieldParams[0].Name;

        //                for (int i = 1; i < fieldParams.Count; i++)
        //                {// to last element
        //                    //sqlCmd.CommandText = "INSERT INTO " + dublTabName + "("+ fieldParams[i].Name +")" + " SELECT " + fieldParams[i].Name +" FROM " + tName;
        //                    s1 += ", " + fieldParams[i].Name;
        //                    s2 += ", " + fieldParams[i].Name;
        //                }
        //                sqlCmd.CommandText = s1 + ")" + s2 + " FROM " + tName;
        //                sqlCmd.ExecuteNonQuery();

        //                sqlCmd.CommandText = "DROP TABLE " + tName;
        //                sqlCmd.ExecuteNonQuery();
        //                //                sqlCmd.CommandText = "PRAGMA legacy_alter_table=OFF";
        //                //                sqlCmd.ExecuteNonQuery();

        //                sqlCmd.CommandText = "ALTER TABLE " + dublTabName + " RENAME TO " + tName;
        //                sqlCmd.ExecuteNonQuery();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Table not compaund column with name " + "\"" + colName + "\"");
        //            }
        //        }

        //    }
        //    catch (SQLiteException ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}



        //private string CreateDublOfTableByFields(string tName, List<FieldParam> paramsOfTable)
        //{
        //    string resultName = "Tmp_" + tName;
        //    sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS " + resultName;// + " (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT, comment TEXT)";
        //    if (paramsOfTable.Count > 0)
        //    {
        //        if (paramsOfTable[0].IsPrimaryKey)
        //        {
        //            sqlCmd.CommandText += " (" + paramsOfTable[0].Name + " " + paramsOfTable[0].DType + " PRIMARY KEY AUTOINCREMENT";
        //        }
        //        else
        //        {
        //            sqlCmd.CommandText += " (" + paramsOfTable[0].Name + " " + paramsOfTable[0].DType;
        //        }
        //        for (int i = 1; i < paramsOfTable.Count; i++)
        //        {
        //            if (paramsOfTable[i].IsPrimaryKey)
        //            {
        //                sqlCmd.CommandText += ", " + paramsOfTable[i].Name + " " + paramsOfTable[i].DType + " PRIMARY KEY AUTOINCREMENT";
        //            }
        //            else
        //            {
        //                sqlCmd.CommandText += ", " + paramsOfTable[i].Name + " " + paramsOfTable[i].DType;
        //            }
        //        }
        //        sqlCmd.CommandText += ")";
        //    }
        //    sqlCmd.ExecuteNonQuery();
        //    return resultName;
        //}

        //public List<FieldParam> GetFieldParams(SQLiteConnection conn, string tName = "")
        //{
        //    if (conn == null)
        //    {
        //        conn = dbConn;
        //    }
        //    if (tName == "")
        //    {
        //        tName = tabName;
        //    }
        //    List<FieldParam> result = new List<FieldParam>();
        //    using (SQLiteCommand cmdSQL = dbConn.CreateCommand())
        //    {
        //        cmdSQL.CommandText = "select * from " + tName;
        //        SQLiteDataReader dr = cmdSQL.ExecuteReader();
        //        var schmTbl = dr.GetSchemaTable();

        //        //Getting primaryKey name of column
        //        string primaryKeyName = "";
        //        bool flgPrimKeyExists = false;
        //        var primKeys = (
        //            from T in schmTbl.AsEnumerable()
        //            where T.Field<bool>("IsKey")
        //            select T.Field<string>("ColumnName")).ToArray();
        //        if (primKeys.Length > 0)
        //        {
        //            primaryKeyName = primKeys[0];
        //            flgPrimKeyExists = true;
        //        }

        //        //Getting Fields params
        //        for (var i = 0; i < dr.FieldCount; i++)
        //        {
        //            FieldParam curParams = new FieldParam();
        //            curParams.Name = dr.GetName(i);
        //            curParams.DType = dr.GetDataTypeName(i);//dr.GetFieldType(i).ToString();
        //            curParams.IsPrimaryKey = flgPrimKeyExists && curParams.Name == primaryKeyName;

        //            result.CreateTable(curParams);
        //        }
        //        dr.Close();
        //    }


        //    return result;
        //}

    }
}
