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
using System.Windows.Threading;

namespace TankDataBase
{
    /// <summary>
    /// Interaction logic for Reaction.xaml
    /// </summary>
    public partial class Reaction : Page
    {
        private Users LoggedInUser;
        private DispatcherTimer ReactionTimer;
        private DispatcherTimer StopwatchTimer;
        private DateTime ReactionStartTime;
        private bool GameActive = false;

        public Reaction(Users loggedInUser)
        {
            InitializeComponent();
            ReactionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            ReactionTimer.Tick += ReactionTimer_Tick;
            StopwatchTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            StopwatchTimer.Tick += StopwatchTimer_Tick;
            LoggedInUser = loggedInUser;
        }
        private void StopwatchTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - ReactionStartTime;
            InstructionText.Text = $"Idő: {elapsed.TotalMilliseconds:F0} ms";
        }

        private void ReactionTimer_Tick(object sender, EventArgs e)
        {
            Random rd = new Random();
            ReactionTimer.Stop();
            ReactionStartTime = DateTime.Now;
            ReactionButton.Margin = new Thickness(0, rd.Next(0, 400), 0, rd.Next(0, 400));
            ReactionButton.Visibility = Visibility.Visible;
            InstructionText.Text = "SHOOT NOW";

            StopwatchTimer.Start();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            if (GameActive)
            {
                return;
            }
            GameActive = true;
            ReactionButton.Visibility = Visibility.Hidden;
            InstructionText.Text = "Wait for the enemy....";

            Random random = new Random();
            int delay = random.Next(1000, 5000);

            ReactionTimer.Interval = TimeSpan.FromMilliseconds(delay);
            ReactionTimer.Start();
        }

        private void ReactionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!GameActive) { return; }
            GameActive = false;

            StopwatchTimer.Stop();

            TimeSpan reactionTime = DateTime.Now - ReactionStartTime;

            if (reactionTime.TotalSeconds <= 1 && reactionTime.TotalSeconds > 0.7)
            {
                InstructionText.Text = $"Reaction time: {reactionTime.TotalSeconds:F3} s\nPractice more gunner!";
                ReactionButton.Visibility = Visibility.Hidden;
            }
            else if(reactionTime.TotalSeconds <= 0.7 && reactionTime.TotalSeconds > 0.5)
            {
                InstructionText.Text = $"Reaction time: {reactionTime.TotalSeconds:F3} s\nNice reaction gunner!";
                ReactionButton.Visibility = Visibility.Hidden;
            }
            else if (reactionTime.TotalSeconds <= 0.5)
            {
                InstructionText.Text = $"Reaction time: {reactionTime.TotalSeconds:F3} s\nExcellent reaction gunner! Bravo!";
                ReactionButton.Visibility = Visibility.Hidden;
            }
        }
    }
}
