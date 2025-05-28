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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for UserAdding.xaml
    /// </summary>
    public partial class UserAdding : Page
    {
        private Users LoggedInUser;
        public UserAdding(Users loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
        }
        private void Saving(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string email = EmailTextBox.Text;
            string role = RoleComboBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show($"You must fill all the spaces!", "Kitöltési Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!IsPasswordStrong(password))
            {
                MessageBox.Show("The password need to be at least 8 characters long," +
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
            Users user = new Users(username, password, email, role);
            using (StreamWriter writer = new StreamWriter("users.txt", true))
            {
                writer.Write("");
                writer.WriteLine(user.ToString());
            }


            UsersListing usersList = new UsersListing(LoggedInUser);
            this.NavigationService.Navigate(usersList);

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
    }
}
