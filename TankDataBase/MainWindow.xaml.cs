using System.IO;
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

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistrationWindow(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerForm = new RegisterWindow();
            registerForm.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (File.Exists("users.txt"))
            {
                var users = File.ReadAllLines("users.txt")
                                .Select(line => Users.FromString(line))
                                .ToList();

                var matchedUser = users.FirstOrDefault(user => user.Username == username
                && user.Password == password);

                if (matchedUser != null)
                {
                    MessageBox.Show($"Successful login!\nWelcome on board, {matchedUser.Username}",
                        "Bejelentkezés", MessageBoxButton.OK, MessageBoxImage.Information);
                    CentralWindow mainDashboardWindow = new CentralWindow(matchedUser);
                    mainDashboardWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No user name or password",
                        "Bejelentkezés", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("There is no registrated user",
                        "Bejelentkezés", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}