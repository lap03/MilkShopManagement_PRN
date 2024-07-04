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

namespace MilkShopManagementDisplay.AdminDisplay
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        private LoginWindow _loginWindow;
        private ProductPage _productPage;
     
        public User CurrentUser { get; set; }

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

        private void BtnProductManagement_Click(object sender, RoutedEventArgs e)
        {
          _productPage = new ProductPage();
            _productPage.Show();
            //productPage.Show();
            this.Close();
          
                
        }
    }
}
