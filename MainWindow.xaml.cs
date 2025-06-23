using System.Windows;
using TennisScoreTracker.ViewModels;

namespace TennisScoreTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}