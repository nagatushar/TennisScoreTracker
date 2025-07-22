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

        public List<string> GetGameScoreDisplay(Player player1, Player player2)
        {
            if (player1.GamesWon == 6 && player2.GamesWon == 6)
            {
                return new List<string> { $"{player1.CurrentGameScore}", $"{player2.CurrentGameScore}" };
            }

            else if (player1.CurrentGameScore <= 3 && player2.CurrentGameScore <= 3)
            {
                return new List<string> { GetDisplayScore(player1.CurrentGameScore), GetDisplayScore(player2.CurrentGameScore) };
            }

            else if (player1.CurrentGameScore == player2.CurrentGameScore)
                {
                return new List<string> { "Deuce", "Deuce" };
            }

            else if (player1.CurrentGameScore > player2.CurrentGameScore)
            {
                return new List<string> { "Adv", "Deuce" };
            }

            else
            {
                return new List<string> { "Deuce", "Adv" };
            }


        }

        // Handle the complex tennis scoring logic
        public void AwardPoint(Player player)
        {
            if (IsMatchComplete) return;

            player.CurrentGameScore++;

            if (player.GamesWon == 6)
            {
                // If in tiebreak, check if player won the tiebreak
                if (IsTBWon(player))
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

            if (player.GamesWon != 6)
            {
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
        }


        private bool IsGameWon(Player player)
        {
            var opponent = player == Player1 ? Player2 : Player1;

            // Need at least 4 points and lead by 2
            return player.CurrentGameScore >= 4 &&
                   player.CurrentGameScore - opponent.CurrentGameScore >= 2;
        }

        private bool IsTBWon(Player player)
        {
            var opponent = player == Player1 ? Player2 : Player1;

            return player.CurrentGameScore >= 7 &&
                   player.CurrentGameScore - opponent.CurrentGameScore >= 2;
        }

        private bool IsSetWon(Player player)
        {
            var opponent = player == Player1 ? Player2 : Player1;

            // If 6 or more games, check if player has won the set
            if (player.GamesWon >= 6 && opponent.GamesWon < 5)
            {
                return true; // Player wins set at 6-5 or 6-1
            }

            if (player.GamesWon == 7 && opponent.GamesWon <= 6)
            {
                return true; // Player wins set at 7-6 or 7-5
            }

            else
            { return false; }
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