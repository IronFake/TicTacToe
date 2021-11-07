namespace TicTacToe.Core
{
    public struct GameConfig
    {
        public int boardSize;
        public int targetWinCount;
        public Player.Player player;
        public Player.Player opponent;
        public bool playerTurnFirst;
    }

    public struct GameResult
    {
        public Player.Player winner;
        public int playerWinCount;
        public int opponentWinCount;
        public float durationGame;
        public int pointsForMatch;
    }
}