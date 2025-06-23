using System;

namespace TennisScoreTracker.Models
{
    public class Match
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public bool IsMatchComplete { get; set; }
        public Player Winner { get; set; }
        public DateTime StartTime { get; set; }

        public Match(string player1Name, string player2Name)
        {
            Player1 = new Player { Name = player1Name };
            Player2 = new Player { Name = player2Name };
            StartTime = DateTime.Now;
        }

        // Convert numeric score to tennis display (0->0, 1->15, 2->30, 3->40)
        public string GetDisplayScore(int score)
        {
            return score switch
            {
                0 => "0",
                1 => "15",
                2 => "30",
                3 => "40",
                _ => "40"
            };
        }

        // Handle the complex tennis scoring logic
        public void AwardPoint(Player player)
        {
            if (IsMatchComplete) return;

            player.CurrentGameScore++;

            // Check if game is won
            if (IsGameWon(player))
            {
                player.GamesWon++;
                ResetGameScores();

                // Check if set is won
                if (IsSetWon(player))
                {
                    player.SetsWon++;
                    ResetGameCounts();

                    // Check if match is won (best of 3 sets)
                    if (player.SetsWon >= 2)
                    {
                        IsMatchComplete = true;
                        Winner = player;
                    }
                }
            }
        }

        private bool IsGameWon(Player player)
        {
            var opponent = player == Player1 ? Player2 : Player1;

            // Need at least 4 points and lead by 2
            return player.CurrentGameScore >= 4 &&
                   player.CurrentGameScore - opponent.CurrentGameScore >= 2;
        }

        private bool IsSetWon(Player player)
        {
            var opponent = player == Player1 ? Player2 : Player1;

            // Need at least 6 games and lead by 2
            return player.GamesWon >= 6 &&
                   player.GamesWon - opponent.GamesWon >= 2;
        }

        private void ResetGameScores()
        {
            Player1.CurrentGameScore = 0;
            Player2.CurrentGameScore = 0;
        }

        private void ResetGameCounts()
        {
            Player1.GamesWon = 0;
            Player2.GamesWon = 0;
        }

        public void ResetMatch()
        {
            Player1.CurrentGameScore = 0;
            Player1.GamesWon = 0;
            Player1.SetsWon = 0;

            Player2.CurrentGameScore = 0;
            Player2.GamesWon = 0;
            Player2.SetsWon = 0;

            IsMatchComplete = false;
            Winner = null;
            StartTime = DateTime.Now;
        }
    }
}