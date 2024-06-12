using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfSqlAny.Logic;
using WpfSqlAny.Windows;

namespace WpfSqlAny
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string NAME_TABLE_HEADER = "NAME";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static List<string> GetDataByColumnName(string name, DataTable dt)
        {
            List<string> result = new List<string>();
            int nNameColumn = 0;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ToString().ToUpper() == name)
                {
                    nNameColumn = i;
                    break;
                }
            }
            foreach (DataRow row in dt.Rows)
            {

                result.Add(row[nNameColumn].ToString());
            }
            return result;
        }
    }
}
