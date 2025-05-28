using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for UsersListing.xaml
    /// </summary>
    public partial class UsersListing : Page
    {
        private Users LoggedInUser;
        private List<Users> Userek;
        public UsersListing(Users loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;

            Userek = File.ReadAllLines("users.txt").Select(Users.FromString).ToList();
            UsersListingBox.ItemsSource = Userek.Select(u => $"{u.Username}");
        }

        private void Adding(object sender, RoutedEventArgs e)
        {
            UserAdding usersadding = new UserAdding(LoggedInUser);
            this.NavigationService.Navigate(usersadding);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListingBox.SelectedIndex == -1)
            {
                MessageBox.Show("Kérlek válassz egy felhasználót!",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LoggedInUser.Username == UsersListingBox.SelectedItem.ToString())
            {
                MessageBox.Show("Magadat nem törölheted ki",
                    "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedUser = UsersListingBox.SelectedItem.ToString();
            var userToRemove = Userek.FirstOrDefault(user => user.Username == selectedUser);

            if (userToRemove != null)
            {
                Userek.Remove(userToRemove);
                File.WriteAllLines("users.txt", Userek.Select(user => user.ToString()));

                UsersListingBox.ItemsSource = Userek.Select(user => user.Username);
            }
        }
    }
}
