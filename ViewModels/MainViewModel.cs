using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TennisScoreTracker.Models;

namespace TennisScoreTracker.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Match _currentMatch;

        public MainViewModel()
        {
            // Initialize with default players
            _currentMatch = new Match("Player 1", "Player 2");

            // Set up commands (like assigning tasks to kitchen staff)
            Player1PointCommand = new RelayCommand(() => AwardPoint(_currentMatch.Player1),
                                                 () => !_currentMatch.IsMatchComplete);
            Player2PointCommand = new RelayCommand(() => AwardPoint(_currentMatch.Player2),
                                                 () => !_currentMatch.IsMatchComplete);
            ResetMatchCommand = new RelayCommand(ResetMatch);
        }

        // Expose the match data to the View
        public Match CurrentMatch
        {
            get => _currentMatch;
            set
            {
                _currentMatch = value;
                OnPropertyChanged();
                RefreshAllProperties();
            }
        }

        // Commands that the View can bind to
        public ICommand Player1PointCommand { get; }
        public ICommand Player2PointCommand { get; }
        public ICommand ResetMatchCommand { get; }

        // Computed properties for the UI (like preparing the final dish presentation)
        public string Player1GameScore => _currentMatch.GetGameScoreDisplay(_currentMatch.Player1, _currentMatch.Player2)[0];
        public string Player2GameScore => _currentMatch.GetGameScoreDisplay(_currentMatch.Player1, _currentMatch.Player2)[1];

        public string Player1SetScore => _currentMatch.Player1.GamesWon.ToString();
        public string Player2SetScore => _currentMatch.Player2.GamesWon.ToString();

        public string Player1SetsWon => _currentMatch.Player1.SetsWon.ToString();
        public string Player2SetsWon => _currentMatch.Player2.SetsWon.ToString();

        public string MatchStatus
        {
            get
            {
                if (_currentMatch.IsMatchComplete)
                    return $"🏆 {_currentMatch.Winner.Name} Wins!";
                return "Match in Progress";
            }
        }

        private void AwardPoint(Player player)
        {
            _currentMatch.AwardPoint(player);
            RefreshAllProperties();
        }

        private void ResetMatch()
        {
            _currentMatch.ResetMatch();
            RefreshAllProperties();
        }

        // Notify the UI that multiple properties changed
        private void RefreshAllProperties()
        {
            OnPropertyChanged(nameof(Player1GameScore));
            OnPropertyChanged(nameof(Player2GameScore));
            OnPropertyChanged(nameof(Player1SetScore));
            OnPropertyChanged(nameof(Player2SetScore));
            OnPropertyChanged(nameof(Player1SetsWon));
            OnPropertyChanged(nameof(Player2SetsWon));
            OnPropertyChanged(nameof(MatchStatus));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}