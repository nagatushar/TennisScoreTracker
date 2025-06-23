namespace TennisScoreTracker.Models
{
    public class TieBreak
    {
        public Player player1 { get; set; }
        public Player player2 { get; set; }
        public bool IsTieBreakComplete { get; set; }
        public Player Winner { get; set; }

        public TieBreak(string player1Name, string player2Name)
        {
            this.player1 = new Player();
            this.player2 = new Player();
            this.player1.Name = player1Name;
            this.player2.Name = player2Name;
            IsTieBreakComplete = false;
            Winner = null;
        }

        public void AwardPoint(Player player)
        {
            if (IsTieBreakComplete)
                return;
            player.CurrentTBScore++;

            if (IsTieBreakWon(player))
            {
                player.GamesWon++;
                IsTieBreakComplete = true;
                Winner = player;
            }
        }

        private bool IsTieBreakWon(Player player)
        {
            Player opponent;
            if (player == player1)
            {
                opponent = player2;
            }
            else
            {
                opponent = player1;
            }
            return player.CurrentTBScore >= 7 && player.CurrentTBScore - opponent.CurrentTBScore >= 2;
        }

        public void Reset()
        {
            player1.CurrentTBScore = 0;
            player2.CurrentTBScore = 0;
            IsTieBreakComplete = false;
            Winner = null;
        }
    }
}
