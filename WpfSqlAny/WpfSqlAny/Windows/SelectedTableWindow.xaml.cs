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

        public SelectedTableWindow()
        {
            InitializeComponent();
            FillTypesCombobox();
        }

        private void FillTypesCombobox()
        {
            var typeNames = SqlDataType.GetDataTypes();
            foreach(var name in typeNames)
            {
                ColumnTypeComboBox.Items.Add(name);
            }
            ColumnTypeComboBox.SelectedIndex = 0;
        }

        private void OnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
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
            //UpdateQueryResult();
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
            if (!CheckConnectionErrors())
            {
                return;
            }

            var tableDataWindow = new DataTableWindow(_dbAdapter, 
                TableName.Text, 
                UpdateQueryResult, 
                DataTableWindow.DateTableMode.AddData);

            tableDataWindow.Owner = this;
            tableDataWindow.ShowDialog();
        }

        private void OnChangeData_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }

            var tableDataWindow = new DataTableWindow(_dbAdapter, 
                TableName.Text, 
                UpdateQueryResult, 
                DataTableWindow.DateTableMode.ChangeData);

            tableDataWindow.Owner = this;
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
    }
}
