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
using WpfSqlAny.Logic.SupportTypes;

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

        private Action _updateData;
        private List<SqlFieldProperty> _fields = null;
        private Dictionary<string, int> _fieldNameToIndx = new Dictionary<string, int>();

        const int ADD_COUNT_ROW = 10;

        private DataTableWindow()
        {
            InitializeComponent();
        }

        public DataTableWindow(IDbAdapter dbAdapter, string tableName, Action updateQueryInfo, DateTableMode mode)
        {
            InitializeComponent();
            _dbAdapter = dbAdapter;
            Mode = mode;
            Title = tableName + $" in mode: {Mode}";
            _tableName = tableName;
            _updateData = updateQueryInfo;

            _fields = _dbAdapter.GetFieldParams(_tableName);

            for (var i = 0; i < _fields.Count; i++)
            {
                _fieldNameToIndx[_fields[i].Name] = i;
            }

            if (mode == DateTableMode.AddData)
            {
                var dt = SqlFieldProperty.GetDataTable(_fields, true);
                for (var i = 0; i < ADD_COUNT_ROW; i++)
                {
                    var rw = dt.NewRow();
                    dt.Rows.Add(rw);
                }
                FillData(TableDataGrid, dt, false);
            }
            else
            {
                var fields = SqlFieldProperty.GetDataTable(_fields, false);
                var dt = _dbAdapter.ReadFromTableAll(_tableName);
                //for (var i = _fields.Count - 1; i > -1; i--) 
                //{
                //    if (_fields[i].IsAutoIncrement)
                //    {
                //        dt.Columns.RemoveAt(i);
                //    }
                //}
                FillData(TableDataGrid, dt, false);
            }
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

            //foreach(var fld in _fields)
            //{
            //    var dataGridTemplate = new DataGridTemplateColumn();
            //    var factory = new FrameworkElementFactory(typeof(TextBlock));
            //    //factory.SetValue(TextBlock.StyleProperty, "Delete Table");
            //    var cellTemplate = new DataTemplate();
            //    cellTemplate.VisualTree = factory;
            //    dataGridTemplate.Header = "action";
            //    dataGridTemplate.CellTemplate = cellTemplate;
            //    dg.Columns.Add(dataGridTemplate);
            //}

            for (var i = 0; i < dt.Columns.Count; i++)
            {
                var column = dt.Columns[i];
                var field = _fields[_fieldNameToIndx[column.ColumnName]];
                if (field.IsAutoIncrement)
                {
                    dt.Columns[i].ReadOnly = true;
                }
            }

            dg.ItemsSource = dt.AsDataView();
            dg.IsReadOnly = readOnly;
        }

        private bool CheckConnectionErrors()
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
                    {
                        var data = GetDataTable(TableDataGrid);
                        //if (!ValidateData(data))
                        //    return;

                        _dbAdapter.AddDataToDB(data, _tableName);
                        break;
                    }
                case DateTableMode.ChangeData:
                    {
                        var data = GetDataTable(TableDataGrid);
                        //if (!ValidateData(data))
                        //    return;

                        _dbAdapter.UpdateDataToDB(data, _tableName, _fields);
                        break;
                    }
            }

            _updateData?.Invoke();
        }

        //private bool ValidateData(DataTable dt)
        //{
        //    for(var i = 0; i < dt.Rows.Count; i++)
        //    {
        //        var row = dt.Rows[i];
        //        for(var j = 0; j < row.ItemArray.Length; j++)
        //        {
        //            var column = dt.Columns[j];
        //            var dataType = column.DataType;
        //            var cell = row.ItemArray[j];
        //            if(cell.ToString() == "" && dataType == typeof(Int64))
        //            {
        //                App.ErrorMessage($"Not correct dataType for column: {column.ColumnName} and rowIndex: {i}");
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        private DataTable GetDataTable(DataGrid dg)
        {
            var dataTable = new DataTable();
            foreach (var item in dg.ItemsSource)
            {
                var table = (item as DataRowView).Row.Table;
                for (var i = 0; i < table.Columns.Count; i++)
                {
                    var column = table.Columns[i];
                    dataTable.Columns.Add(column.ToString());
                    var field = _fields[_fieldNameToIndx[column.ColumnName]];
                    dataTable.Columns[i].DataType = field.Type.GetMappedType();
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
                }
            }

            return dataTable;
        }
    }
}
