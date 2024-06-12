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
    /// Interaction logic for CreatingTableWindow.xaml
    /// </summary>
    public partial class CreatingTableWindow : Window, IShowWithStringCallback
    {
        private Action<string> _callback;
        public CreatingTableWindow()
        {
            InitializeComponent();
        }

        private void OnAccept_Click(object sender, RoutedEventArgs e)
        {
            var name = TabName.Text;
            if(_callback != null)
            {
                _callback.Invoke(name);
            }
            Hide();
        }

        #region IShowWithStringCallback
        public void ShowWithCallback(Action<string> callback)
        {
            _callback = callback;
            Show();
        }
        #endregion

    }
}
