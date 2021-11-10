namespace TicTacToe.Core
{
    public struct GameConfig
    {
        public int boardSize;
        public int targetWinCount;
        public Player.Player playerForCross;
        public Player.Player playerForRing;
        public Board.Mark playerMark;
    }

    public struct GameResult
    {
        public Player.Player winner;
        public int crossWins;
        public int ringWins;
        public float durationGame;
        public int pointsForMatch;
    }
}