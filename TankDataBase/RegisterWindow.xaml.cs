using System;
using System.Collections.Generic;
using System.IO;
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

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)
                || string.IsNullOrWhiteSpace(confirmPassword)
                || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("You must fill all the spaces!",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("A jelszavak nem egyeznek meg!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsPasswordStrong(password))
            {
                MessageBox.Show("The password need to be at least 8 characters long, " +
                    " and must contain a number or other special character",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsEmail(email))
            {
                MessageBox.Show("The email must be a valied one!",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string role = "";
            if (File.Exists("users.txt") == false)
            {
                role = "Admin";
            }
            else
            {
                role = "User";
            }
            Users user = new Users(username, password, email, role);
            using (StreamWriter writer = new StreamWriter("users.txt", true))
            {
                writer.WriteLine(user.ToString());
            }

            MessageBox.Show("Successful registration!", "Regisztráció", MessageBoxButton.OK,
                MessageBoxImage.Information);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private bool IsPasswordStrong(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }
        private bool IsEmail(string email)
        {
            return email.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
