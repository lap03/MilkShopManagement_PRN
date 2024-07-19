using MilkShopManagementDisplay.AdminDisplay;
using MilkShopManagementDisplay.UserDisplay;
using Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MilkShopManagementDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService();
            var email = txtEmail.Text;
            var password = txtPassword.Password;

            var user = userService.checkLogin(email, password);
            if (user != null)
            {
                if (user.IsActive == true && user.Role == 1)
                {
                    MainAdminWindow mainAdminWindow = new MainAdminWindow(this, user);
                    mainAdminWindow.Show();
                    this.Hide();

                } else if(user.IsActive == true && user.Role == 2)
                {
                    MainUserWindow mainUserWindow = new MainUserWindow(this);
                    mainUserWindow.CurrentUser = user;
                    mainUserWindow.Show();
                    this.Hide();

                } else
                {
                    MessageBox.Show("account does not have access rights!!");
                }
            }
            else
            {
                MessageBox.Show("Wrong email or password!!");
            }
        }

        public void ResetFields()
        {
            txtEmail.Text = string.Empty;
            txtPassword.Password = string.Empty; // Assuming txtPassword is a PasswordBox
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(this);
            registerWindow.Show();
            this.Hide();
        }
    }
}