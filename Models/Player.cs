using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TennisScoreTracker.Models
{
    public class Player
    {
        private string _name;
        private int _gamesWon;
        private int _setsWon;
        private int _pointsWon;
        private bool isServing;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public int GamesWon
        {
            get => _gamesWon;
            set
            {
                _gamesWon = value;
            }
        }

        public int SetsWon
        {
            get => _setsWon;
            set
            {
                _setsWon = value;
            }
        }

        public int PointsWon
        {
            get => _pointsWon;
            set
            {
                _pointsWon = value;
            }
        }

        public bool IsServing
        {
            get => isServing;
            set
            {
                isServing = value;
            }
        }
        public int CurrentGameScore { get; set; } // 0, 15, 30, 40

        public int CurrentTBScore { get; set; }
    }
}