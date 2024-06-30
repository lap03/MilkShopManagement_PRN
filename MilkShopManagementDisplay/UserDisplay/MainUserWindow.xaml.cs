using Microsoft.VisualBasic.ApplicationServices;
using Reprository.Models;
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

namespace MilkShopManagementDisplay.UserDisplay
{
    /// <summary>
    /// Interaction logic for MainUserWindow.xaml
    /// </summary>
    public partial class MainUserWindow : Window
    {
        private LoginWindow _loginWindow;
        private Reprository.Models.User? _currentUser;

        public MainUserWindow(LoginWindow loginWindow, Reprository.Models.User currentUser)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
            _currentUser = currentUser;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            _currentUser = null;
            _loginWindow.ResetFields();
            _loginWindow.Show();
            this.Close();
        }

        private void btnYourOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderPage f = new OrderPage();
            f.SelectedUser = _currentUser;
            f.LoadSelectedUser();
            f.Show();
        }
    }
}
