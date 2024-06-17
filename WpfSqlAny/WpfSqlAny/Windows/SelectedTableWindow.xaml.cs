using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfSqlAny.Logic;
using WpfSqlAny.Logic.SupportTypes;

namespace WpfSqlAny.Windows
{
    /// <summary>
    /// Interaction logic for SelectedTableWindow.xaml
    /// </summary>
    public partial class SelectedTableWindow : Window, IRequestToTables
    {
        private IDbAdapter _dbAdapter;

        const string START_TABLE_NAME = "tableName";
        const string START_QUERY = "SELECT * FROM tableName";
        const int ADD_COUNT_ROW = 10;
        public SelectedTableWindow()
        {
            InitializeComponent();
        }

        private void OnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (_dbAdapter == null)
            {
                App.ErrorMessage("Not found link to dbAdapter");
                return;
            }
            if (_dbAdapter.CurrentStatus == ConnectionStatusType.Disconnected)
            {
                App.ErrorMessage("db not conneted");
                return;
            }

            UpdateQueryResult();
        }

        private void UpdateQueryResult()
        {
            var query = QueryText.Text;
            if (_dbAdapter != null)
            {
                QueryResult.ItemsSource = _dbAdapter.ReadFromTable(query).AsDataView();
                QueryResult.IsReadOnly = true;
            }
        }
        
        #region IRequestAnswer
        public void ShowWithUpdateInfo(IDbAdapter dbAdapter, string tableName)
        {
            TableName.Text = tableName;
            QueryText.Text = START_QUERY.Replace(START_TABLE_NAME, tableName);
            _dbAdapter = dbAdapter;
            Show();
            UpdateQueryResult();
        }

        #endregion

        private bool CheckConnectionErrors()
        {
            if (_dbAdapter == null)
            {
                App.ErrorMessage("Not found link to dbAdapter");
                return false;
            }
            if (_dbAdapter.CurrentStatus == ConnectionStatusType.Disconnected)
            {
                App.ErrorMessage("db not conneted");
                return false;
            }
            return true;
        }

        private void OnAddData_Click(object sender, RoutedEventArgs e)
        {
            if (_dbAdapter == null)
            {
                App.ErrorMessage("Not found link to dbAdapter");
                return;
            }
            if (_dbAdapter.CurrentStatus == ConnectionStatusType.Disconnected)
            {
                App.ErrorMessage("db not conneted");
                return;
            }

            var tableDataWindow = new DataTableWindow(_dbAdapter, TableName.Text, DataTableWindow.DateTableMode.AddData);
            tableDataWindow.Owner = this;

            var fields = _dbAdapter.GetFieldParams(TableName.Text);
            var dt = SqlFieldProperty.GetDataTable(fields, true);
            for (var i = 0; i < ADD_COUNT_ROW; i++)
            {
                var rw = dt.NewRow();
                dt.Rows.Add(rw);
            }
            FillData(tableDataWindow.TableData, dt, false);
            tableDataWindow.ShowDialog();
        }

        private void OnChangeData_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }

            var tableDataWindow = new DataTableWindow(_dbAdapter, TableName.Text, DataTableWindow.DateTableMode.ChangeData);

            tableDataWindow.Owner = this;
            FillData(tableDataWindow.TableData, _dbAdapter.ReadFromTableAll(TableName.Text), true);
            //var fields = _dbAdapter.GetFieldParams(TableName.Text);
            //for(var i = 0; i < fields.Count; i++)
            //{
            //    if (fields[i].IsAutoIncrement)
            //    {
            //        (tableDataWindow.TableData.ed as DataGridTextColumn).IsReadOnly = true;
            //    }
            //}
            tableDataWindow.ShowDialog();
        }

        private void OnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }

            if (!string.IsNullOrEmpty(ColumnNameBlock.Text))
            {
                _dbAdapter.AddColumn(TableName.Text, ColumnNameBlock.Text, new SqlDataType());
                UpdateQueryResult();
            }
        }

        private void OnDeleteColumn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }
            _dbAdapter.DeleteColumn(TableName.Text, ColumnNameBlock.Text);
            UpdateQueryResult();
        }

        private void FillData(DataGrid dg, DataTable dt, bool readOnly)
        {
            //for (var i = 0; i < dt.Columns.Count; i++)
            //{
            //    dg.Columns.Add(new DataGridTextColumn
            //    {
            //        Header = dt.Columns[i].ColumnName,
            //        Binding = new Binding($"[{dt.Columns[i].ColumnName}]")
            //    });

            //    for (var j = 0; j < dt.Rows.Count; j++)
            //    {
            //        dg.Columns[0].
            //    }
            //}

            dg.ItemsSource = dt.AsDataView();
            dg.IsReadOnly = readOnly;
        }
    }
}
