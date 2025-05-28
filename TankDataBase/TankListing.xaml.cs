using System;
using System.IO;
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
using static System.Reflection.Metadata.BlobBuilder;

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for TankListing.xaml
    /// </summary>
    public partial class TankListing : Page
    {
        private Users LoggedInUser;
        private List<Tanks> Tankok;
        public TankListing(Users loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;

            Tankok = File.ReadAllLines("tanks.txt").Select(Tanks.FromString).ToList();
            TanksListBox.ItemsSource = Tankok.Select(u => u.ToString());
        }

        private void Deleting(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser.Role != "Admin")
            {
                MessageBox.Show($"Your role doesn't allow this function!", "Nincs Jogosultság", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TanksListBox.SelectedIndex == -1)
            {
                MessageBox.Show($"There is no selected tank.", "Kiválasztási Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int index = TanksListBox.SelectedIndex;
            Tankok.RemoveAt(index);

            File.WriteAllLines("tanks.txt", Tankok.Select(u => u.ToString()));
            TanksListBox.ItemsSource = Tankok.Select(u => u.ToString());
        }

        private void Editing(object sender, RoutedEventArgs e)
        {
            if (LoggedInUser.Role != "Admin")
            {
                MessageBox.Show($"Your role doesn't allow this function!", "Nincs Jogosultság", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TanksListBox.SelectedIndex == -1)
            {
                MessageBox.Show($"There is no selected tank to edit.", "Kiválasztási Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var selectedItem = TanksListBox.SelectedItem;
            int index = TanksListBox.SelectedIndex;
            Tanks book = Tanks.FromObject(selectedItem);
            TanksChange bm = new TanksChange(book, index, LoggedInUser);
            this.NavigationService.Navigate(bm);
        }

        private void Filter(object sender, RoutedEventArgs e)
        {
            if (Tankok.Count > 0)
            {
                string findingCategory = FinderTextBox.Text.ToLower();
                TanksListBox.ItemsSource = Tankok.Where(u => u.Category.ToLower().Contains(findingCategory)).Select(u => u.ToString());
            }
        }
    }
}
