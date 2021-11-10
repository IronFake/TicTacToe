using System;
using TicTacToe.Loading;
using UnityEngine;

namespace TicTacToe.UI
{
    public class PauseView : View
    {
        [SerializeField] private Stopwatch stopwatch;
        
        private void OnEnable()
        {
            if (stopwatch)
            {
                stopwatch.StopStopwatch();
            }
        }

        private void OnDisable()
        {
            if (stopwatch)
            {
                stopwatch.StartStopWatch();
            }
        }

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