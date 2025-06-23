using System.Windows;
using TennisScoreTracker.ViewModels;

namespace TennisScoreTracker
{
    
    public partial class TieBreakWindow : Window
    {
        public TieBreakWindow()
        {
            InitializeComponent();
            DataContext = new TieBreakViewModel();

        }
    }
}
