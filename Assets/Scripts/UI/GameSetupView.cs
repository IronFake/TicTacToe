using TicTacToe.Core;
using TicTacToe.Loading;
using TicTacToe.Player;

namespace TicTacToe.UI
{
    public class GameSetupView : View
    {
        private int _winStreak;
        private Board.Mark _mark;


        public void ChooseWinningStreak(int winStreak)
        {
            _winStreak = winStreak;
        }

        public void ChooseMark(int markIndex)
        {
            _mark = (Board.Mark) markIndex;
        }

        public void StartGame()
        {
            GameConfig gameConfig = new GameConfig();
            gameConfig.boardSize = 3;
            gameConfig.player = PlayerHolder.Instance.Player;
            gameConfig.opponent = new Player.Player() {name = "AI", exp = 100};
            gameConfig.targetWinCount = _winStreak;
            gameConfig.playerTurnFirst = _mark == Board.Mark.X;
            
            DataHolder.Instance.GameConfig = gameConfig;
            DataHolder.Instance.GameResult = new GameResult();
            
            LoadingManager.Instance.StartingGame();
        }
    }
}
