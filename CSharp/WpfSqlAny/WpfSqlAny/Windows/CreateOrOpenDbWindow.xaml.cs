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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSqlAny.Logic.SupportTypes;

namespace WpfSqlAny.Windows
{
    /// <summary>
    /// Interaction logic for CreateOrOpenDbWindow.xaml
    /// </summary>
    public partial class CreateOrOpenDbWindow : Window
    {

        public CreateOrOpenDbWindow(DataBaseType dataBaseType)
        {
            InitializeComponent();

            TableDataGrid.ItemsSource = FindDatabases(dataBaseType).AsDataView();
        }

        public DataTable FindDatabases(DataBaseType dataBaseType)
        {
            var dt = new DataTable();

            return dt;
        }

        private void AddNewPath_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void CreateNew_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
