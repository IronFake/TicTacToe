using TicTacToe.Loading;
using TicTacToe.Player;
using UnityEngine;

namespace TicTacToe.UI
{
    public class MainMenuView : View
    {
        public void StartGame()
        {
            UIManager.Instance.Show(ViewType.MatchSettings);
        }

        public void OpenSettings()
        {
            UIManager.Instance.Show(ViewType.Settings);
        }

        public void ChangeName()
        {
            var modalDialog = ModalDialog.Instance;
            modalDialog.ShowChangeNameDialog(() =>
            {
                PlayerHolder.Instance.UpdateName(modalDialog.InputString);
            });
        }
    }
}
