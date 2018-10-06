namespace RockPaperScissors
{
    // A simple data object that records a player's score
    class PlayerScore
    {
        public readonly IPlayer Player;
        public readonly int Score;

        public PlayerScore(IPlayer player, int score)
        {
            Player = player;
            Score = score;
        }
        public static int operator +(PlayerScore playerScore, int win)
        {
            return playerScore.Score + win;
        }
    }
}