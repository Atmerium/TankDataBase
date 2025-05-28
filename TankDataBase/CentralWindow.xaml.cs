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
using System.Windows.Shapes;

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for CentralWindow.xaml
    /// </summary>
    public partial class CentralWindow : Window
    {
        private Users LoggedInUser;
        public CentralWindow(Users loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
            if (loggedInUser.Role == "Admin")
            {
                UsersList.Visibility = Visibility.Visible;
            }
        }

        private void Log_Out(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Userlisting(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UsersListing(LoggedInUser);
        }

        private void TankListing(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TankListing(LoggedInUser);
        }

        private void TankAdding(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TankAdding(LoggedInUser);
        }

        private void Reaction(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new Reaction(LoggedInUser);
        }
    }
}
