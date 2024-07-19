using Reprository.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MilkShopManagementDisplay
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private AdminService _service = new AdminService();
        private LoginWindow _loginWindow;

        public RegisterWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            _loginWindow = loginWindow;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == "" || txtName.Text == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("Some fields are blank, please enter all fields!!!");
                return;
            }

            if (!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                MessageBox.Show("Wrong format email!!!");
                return;
            }

            if (_service.GetAllUsers().Any(u => u.Email.Equals(txtEmail.Text)))
            {
                MessageBox.Show("This email is duplicated. Please Enter again!", "Invalid email", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.Match(txtPhoneNumber.Text, @"^0[0-9]{9}$").Success)
            {
                MessageBox.Show("Phone number must be 10 number and start with 0!!!");
                return;
            }

            if (txtPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Password and password confirm does not match!!!");
                return;
            }

            User newUser = new User
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                Password = txtPassword.Password,
                Role = 2,
                PhoneNumber = txtPhoneNumber.Text,
                IsActive = true,
            };

            _service.AddUser(newUser);
            MessageBox.Show("Add successfully!!!");
            _loginWindow.ResetFields();
            _loginWindow.Show();
            this.Close();

        }
    }
}
