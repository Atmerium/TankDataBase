using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for TanksChange.xaml
    /// </summary>
    public partial class TanksChange : Page
    {
        int index;
        private Users LoggedInUser;
        public TanksChange(Tanks tank, int indexOfSelected, Users loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
            NameTextBox.Text = tank.Name;
            FromTextBox.Text = tank.From;
            AgeTextBox.Text = tank.Age.ToString();
            CategoryComboBox.Text = tank.Category;

            index = indexOfSelected;
        }

        private void Editing(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string from = FromTextBox.Text;
            string age = AgeTextBox.Text;
            string category = CategoryComboBox.Text;
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(age))
            {
                MessageBox.Show($"You must fill all the spaces!", "Kitöltési Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(age, out int parsedAge))
            {
                MessageBox.Show($"Only number is acceptable!", "Helytelen Bemenet", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (parsedAge < 1910 || parsedAge > DateTime.Now.Year)
            {
                MessageBox.Show($"The year of issue must be at least 1910 and must not exceed the present year.", "Helytelen Bemenet", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tanks = File.ReadAllLines("tanks.txt").Select(Tanks.FromString).ToList();

            var selectedtank = tanks[index];
            selectedtank.Name = name;
            selectedtank.From = from;
            selectedtank.Age = parsedAge;
            selectedtank.Category = category;

            File.WriteAllLines("tanks.txt", tanks.Select(tank => tank.ToString()));
            MessageBox.Show($"Successful editing!", "Módosítás", MessageBoxButton.OK, MessageBoxImage.Information);

            TankListing booksList = new TankListing(LoggedInUser);
            this.NavigationService.Navigate(booksList);
        }
    }
}
