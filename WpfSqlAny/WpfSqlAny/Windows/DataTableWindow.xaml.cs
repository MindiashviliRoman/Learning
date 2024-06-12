using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfSqlAny.Logic;

namespace WpfSqlAny.Windows
{
    /// <summary>
    /// Interaction logic for DataTableWindow.xaml
    /// </summary>
    public partial class DataTableWindow : Window, IRequestToTables
    {
        public enum DateTableMode
        {
            AddData,
            ChangeData
        }

        public DateTableMode Mode;
        private IDbAdapter _dbAdapter;
        public DataTableWindow()
        {
            InitializeComponent();
        }

        #region IRequestAnswer
        public void ShowWithUpdateInfo(IDbAdapter dbAdapter, string tableName)
        {
            Title = tableName + $" in mode: {Mode}";
            _dbAdapter = dbAdapter;
            ShowDialog();
        }

        public bool CheckConnectionErrors()
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

        #endregion

        public void Init(IDbAdapter dbAdapter, DateTableMode mode)
        {
            _dbAdapter = dbAdapter;
            Mode = mode;
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
                    //_dbAdapter.SaveDataToDB();
                    break;
                case DateTableMode.ChangeData:

                    break;
            }
        }
    }
}
