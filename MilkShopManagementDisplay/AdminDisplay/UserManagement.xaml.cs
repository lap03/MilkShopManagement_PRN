using Microsoft.IdentityModel.Tokens;
using Reprository.Models;
using Services;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MilkShopManagementDisplay.AdminDisplay
{

    public partial class UserManagement : Window
    {
        private User _User;
        private AdminService _service = new AdminService();
        public UserManagement(MainAdminWindow mainAdminWindow, User user)
        {
            InitializeComponent();
            _User = user;
        }

        private void UserList_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataToGrid();
            btnSave.IsEnabled = false;
        }

        private void LoadDataToGrid()
        {
            UserListDataGrid.ItemsSource = null;
            UserListDataGrid.ItemsSource = _service.GetAllUsers().Where(u => u != _User);
        }
        private void UserListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserListDataGrid.SelectedItem is User selectedUser)
            {
                // Hiện nút Save
                btnSave.IsEnabled = true;

                // Điền thông tin của user được chọn vào các textbox
                txtName.Text = selectedUser.Name;
                txtEmail.Text = selectedUser.Email;
                txtAdress.Text = selectedUser.Address;
                txtPassword.Text = selectedUser.Password;
                txtPhoneNumber.Text = selectedUser.PhoneNumber;
                txtRole.Text = selectedUser.Role == 1 ? "Admin" : "User";
                cbIsActive.IsChecked = selectedUser.IsActive;
            }
            else
            {
                // Ẩn nút Save
                btnSave.IsEnabled = false;

                // Xóa thông tin trong các textbox
                txtName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtAdress.Text = string.Empty;
                txtPhoneNumber.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtRole.Text = string.Empty;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (UserListDataGrid.SelectedItem is User selectedUser)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    {
                        selectedUser.IsActive = false;
                        _service.SaveUser(selectedUser);
                        LoadDataToGrid();
                        MessageBox.Show("Delete successfully!!!");
                        return;
                    }
                }
            }
            MessageBox.Show("Cannot Delete!", "Cannot find User", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string role = txtRole.Text;
            string adress = txtAdress.Text;
            string phoneNumber = txtPhoneNumber.Text;

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(adress) && !string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(password))
            {
                if (phoneNumber.Length != 10 || !phoneNumber.StartsWith("0") || !long.TryParse(phoneNumber, out _))
                {
                    MessageBox.Show("Please enter a valid 10-digit phone number starting with 0!", "Invalid Phone Number", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }
                if (_service.GetAllUsers().Any(u => u.Email.Equals(email)))
                {
                    MessageBox.Show("This email is duplicated. Please Enter again!", "Invalid email", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                int roleValue = 0;
                if (role.Equals("Admin"))
                {
                    roleValue = 1;
                }
                else if (role.Equals("User"))
                {
                    roleValue = 2;
                }
                else
                {
                    MessageBox.Show("Invalid role. Please enter 'Admin' or 'User'.", "Invalid Role", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                User newUser = new User
                {
                    Name = name,
                    Email = email,
                    Address = adress,
                    Password = password,
                    Role = roleValue,
                    PhoneNumber = phoneNumber,
                    IsActive = cbIsActive.IsChecked ?? false,
                };

                _service.AddUser(newUser);
                LoadDataToGrid();
                MessageBox.Show("Add successfully!!!");
                return;
            }
            else
            {
                MessageBox.Show("Please fill in missing form!", "Some forms are not filled", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            if (UserListDataGrid.SelectedItem is User selectedUser)
            {
                if (string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtEmail.Text) && string.IsNullOrEmpty(txtAdress.Text) && string.IsNullOrEmpty(txtPhoneNumber.Text) && string.IsNullOrEmpty(txtRole.Text) && string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Please fill in missing form!", "Some forms are not filled", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                if (phoneNumber.Length != 10 || !phoneNumber.StartsWith("0") || !long.TryParse(phoneNumber, out _))
                {
                    MessageBox.Show("Please enter a valid 10-digit phone number starting with 0!", "Invalid Phone Number", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                if (_service.GetAllUsers().Where(a => a.Email != selectedUser.Email).Any(u => u.Email.Equals(email)))
                {
                    MessageBox.Show("This email is duplicated. Please Enter again!", "Invalid email", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                int roleValue = 0;
                if (txtRole.Text.Equals("Admin"))
                {
                    roleValue = 1;
                }
                else if (txtRole.Text.Equals("User"))
                {
                    roleValue = 2;
                }
                else
                {
                    MessageBox.Show("Invalid role. Please enter 'Admin' or 'User'.", "Invalid Role", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }

                selectedUser.Name = txtName.Text;
                selectedUser.Email = txtEmail.Text;
                selectedUser.Password = txtPassword.Text;
                selectedUser.Role = roleValue;
                selectedUser.Address = txtAdress.Text;
                selectedUser.PhoneNumber = txtPhoneNumber.Text;
                selectedUser.IsActive = cbIsActive.IsChecked ?? false;
                _service.SaveUser(selectedUser);
                LoadDataToGrid();
                MessageBox.Show("User information updated successfully!");
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRole.Text = string.Empty;
        }
    }
}