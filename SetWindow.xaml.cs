using System.Windows;
using TennisScoreTracker.ViewModels;

namespace TennisScoreTracker
{
    
    public partial class SetWindow : Window
    {
        public SetWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

        }
    }
}
