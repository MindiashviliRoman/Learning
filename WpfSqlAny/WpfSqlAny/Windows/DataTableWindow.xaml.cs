using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSqlAny.Logic;

namespace WpfSqlAny.Windows
{
    /// <summary>
    /// Interaction logic for DataTableWindow.xaml
    /// </summary>
    public partial class DataTableWindow : Window
    {
        public enum DateTableMode
        {
            AddData,
            ChangeData
        }

        public DateTableMode Mode { get; }
        private IDbAdapter _dbAdapter;
        private string _tableName;
        private DataTableWindow()
        {
            InitializeComponent();
        }

        public DataTableWindow(IDbAdapter dbAdapter, string tableName, DateTableMode mode)
        {
            InitializeComponent();
            _dbAdapter = dbAdapter;
            Mode = mode;
            Title = tableName + $" in mode: {Mode}";
            _tableName = tableName;
        }

        public bool CheckConnectionErrors()
        {
            if (_dbAdapter == null)
            {
                App.ErrorMessage("Not found link to dbAdapter");
                return false;
            }
            if (_dbAdapter.CurrentStatus != ConnectionStatusType.Connected)
            {
                App.ErrorMessage("db not conneted");
                return false;
            }
            return true;
        }

        private void Accept_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckConnectionErrors())
            {
                return;
            }
            switch(Mode)
            {
                case DateTableMode.AddData:
                    _dbAdapter.SaveDataToDB(GetDataTable(TableData), _tableName);
                    break;
                case DateTableMode.ChangeData:

                    break;
            }
        }

        private DataTable GetDataTable(DataGrid dg)
        {
            var dataTable = new DataTable();
            foreach (var item in dg.ItemsSource)
            {
                var table = (item as DataRowView).Row.Table;
                foreach (var column in table.Columns)
                {
                    dataTable.Columns.Add(column.ToString());
                }
                break;
            }

            foreach (var item in dg.ItemsSource)
            {
                var row = (item as DataRowView).Row;
                foreach (var cell in row.ItemArray)
                {
                    if (!string.IsNullOrEmpty(cell.ToString()))
                    {
                        dataTable.Rows.Add(row.ItemArray);
                        break;
                    }
                    //var count = row.ItemArray.Length;
                    //var values = new object[count];
                    //for (int i = 0; i < count; i++)
                    //{
                    //    values[i] = row.ItemArray[i];
                    //}
                }
            }

            return dataTable;
        }
    }
}
