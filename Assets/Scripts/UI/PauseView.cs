using TicTacToe.Loading;

namespace TicTacToe.UI
{
    public class PauseView : View
    {
        public void OpenSettings()
        {
            UIManager.Instance.Show(ViewType.Settings);
        }

        public void Exit()
        {
            ModalDialog.Instance.ShowDialog("Exit Game", "Are you sure want to quit the game?", false, 
                "Yes",() => LoadingManager.Instance.EndingGame(), "Back", () => {});
        }
    }
}