using TicTacToe.Core;
using TicTacToe.Loading;
using TicTacToe.Player;

namespace TicTacToe.UI
{
    public class MatchSetting : View
    {
        private int _winStreak = 1;
        private Board.Mark _mark = Board.Mark.X;

        
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
            
            var ai = new Player.Player() {name = "AI", exp = 100};
            gameConfig.playerForCross = _mark == Board.Mark.X ? PlayerHolder.Instance.Player : ai;
            gameConfig.playerForRing =  _mark == Board.Mark.X ? ai : PlayerHolder.Instance.Player;
            gameConfig.targetWinCount = _winStreak;
            gameConfig.playerMark = _mark;
            
            DataHolder.Instance.GameConfig = gameConfig;
            DataHolder.Instance.GameResult = new GameResult();
            LoadingManager.Instance.StartingGame();
        }
    }
}
