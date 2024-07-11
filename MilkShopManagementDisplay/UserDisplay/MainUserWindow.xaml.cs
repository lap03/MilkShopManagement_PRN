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
        public User CurrentUser { get; set; }

        public MainUserWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = null;
            _loginWindow.ResetFields();
            _loginWindow.Show();
            this.Close();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileUserWindow profileUserWindow = new ProfileUserWindow(this);
            profileUserWindow.SelectedUser = CurrentUser;
            profileUserWindow.ShowDialog();
        }
        private void btnYourOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderPage f = new OrderPage();
            f.SelectedUser = CurrentUser;
            f.LoadSelectedUser();
            f.ShowDialog();
        }

        public void updateCurrentUser(User newUser)
        {
            CurrentUser = newUser;
        }

        private void MainUser_Loaded(object sender, RoutedEventArgs e)
        {
            lblUserName.Content = "Hello " + CurrentUser?.Name;
        }

        public void ResetFields()
        {
            lblUserName.Content = "Hello " + CurrentUser?.Name;
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();  
            productWindow.ShowDialog();
        }
    }
}