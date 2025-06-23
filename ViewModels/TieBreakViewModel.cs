using System.ComponentModel;
using System.Runtime.CompilerServices;
using TennisScoreTracker.Models;
using System.Windows.Input;

namespace TennisScoreTracker.ViewModels
{
    class TieBreakViewModel : INotifyPropertyChanged
    {
        private TieBreak _currentTieBreak;
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }

        public TieBreakViewModel()
        {
            _currentTieBreak = new TieBreak(Player1Name, Player2Name);

            Player1PointCommand = new RelayCommand(() => AwardPoint(_currentTieBreak.player1), () => !_currentTieBreak.IsTieBreakComplete);
            Player2PointCommand = new RelayCommand(() => AwardPoint(_currentTieBreak.player2), () => !_currentTieBreak.IsTieBreakComplete);
            ResetCommand = new RelayCommand(() => ResetMatch());
        }

        public ICommand Player1PointCommand { get; set; }
        public ICommand Player2PointCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public TieBreak CurrentTieBreak
        {
            get { return _currentTieBreak; }
            set { _currentTieBreak = value;
                OnPropertyChanged();
                RefreshAllProperties();
            }
        }

        public int Player1GameScore => _currentTieBreak.player1.CurrentTBScore;
        public int Player2GameScore => _currentTieBreak.player2.CurrentTBScore;
        public int Player1PointScore => _currentTieBreak.player1.CurrentTBScore;
        public int Player2PointScore => _currentTieBreak.player2.CurrentTBScore;


        private void ResetMatch()
        {
            _currentTieBreak.Reset();
            RefreshAllProperties();
        }

        private void RefreshAllProperties()
        {
            OnPropertyChanged(nameof(Player1GameScore));
            OnPropertyChanged(nameof(Player2GameScore));
            OnPropertyChanged(nameof(Player1PointScore));
            OnPropertyChanged(nameof(Player2PointScore));
            //OnPropertyChanged(nameof(MatchStatus));
        }


        private void AwardPoint(Player player)
        {
            _currentTieBreak.AwardPoint(player);
            RefreshAllProperties();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    }
