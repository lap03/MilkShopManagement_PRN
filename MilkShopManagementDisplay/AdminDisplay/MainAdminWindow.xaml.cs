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

namespace MilkShopManagementDisplay.AdminDisplay
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        private LoginWindow _loginWindow;
        public MainAdminWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            _loginWindow.ResetFields();
            _loginWindow.Show();
            this.Close();
        }
    }
}
