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
using static System.Reflection.Metadata.BlobBuilder;

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for TankAdding.xaml
    /// </summary>
    public partial class TankAdding : Page
    {
        private Users LoggedInUser;
        public TankAdding(Users loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
        }

        private void Saving(object sender, RoutedEventArgs e)
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

            Tanks tank = new Tanks(name, from, parsedAge, category);
            using (StreamWriter wr = new StreamWriter("tanks.txt", true))
            {
                wr.Write("");
                wr.WriteLine(tank.ToString());
            }

            TankListing tanksList = new TankListing(LoggedInUser);
            this.NavigationService.Navigate(tanksList);
        }
    }
}
