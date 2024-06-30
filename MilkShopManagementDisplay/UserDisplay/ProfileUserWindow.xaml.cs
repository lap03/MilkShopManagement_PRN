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

namespace MilkShopManagementDisplay.UserDisplay
{
    /// <summary>
    /// Interaction logic for ProfileUserWindow.xaml
    /// </summary>
    public partial class ProfileUserWindow : Window
    {

        public User SelectedUser { get; set; }
        private MainUserWindow _mainUserWindow;
        public ProfileUserWindow(MainUserWindow mainUserWindow)
        {
            InitializeComponent();
            _mainUserWindow = mainUserWindow;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            if (txtEmail.Text == "" || txtName.Text == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show("Some fields are blank, please enter all fields!!!");
                return;
            }
            if(!Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                MessageBox.Show("Wrong format email!!!");
                return;
            }
            if(!Regex.Match(txtPhoneNumber.Text, @"^0[0-9]{9}$").Success)
            {
                MessageBox.Show("Phone number must be 10 number and start with 0!!!");
                return;
            }
            if(txtPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Password and password confirm does not match!!!");
                return;
            }

            SelectedUser.Email = txtEmail.Text;
            SelectedUser.Name = txtName.Text;
            SelectedUser.Address = txtAddress.Text;
            SelectedUser.PhoneNumber = txtPhoneNumber.Text;
            SelectedUser.Password = txtPassword.Password;

            userService.Update(SelectedUser);
            _mainUserWindow.CurrentUser = SelectedUser;
            _mainUserWindow.ResetFields();
            this.Close();
        }

        private void Profile_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = SelectedUser.Email;
            txtName.Text = SelectedUser.Name;
            txtAddress.Text = SelectedUser.Address;
            txtPhoneNumber.Text = SelectedUser.PhoneNumber;
            txtPassword.Password = SelectedUser.Password;
            txtConfirmPassword.Password = SelectedUser.Password;
        }
    }
}
