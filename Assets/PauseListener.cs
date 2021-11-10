using System.Collections;
using System.Collections.Generic;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe
{
    public class PauseListener : MonoBehaviour
    {
        [SerializeField] private KeyCode exitKeyCode;
        [SerializeField] private GameObject pauseView;
        
        private void Update()
        {
            if (Input.GetKeyDown(exitKeyCode))
            {
                if (ModalDialog.Instance.IsOpen)
                {
                    ModalDialog.Instance.IsOpen = false;
                    return;
                }
                
                if (UIManager.Instance.IsStartingView)
                {
                    pauseView.SetActive(!pauseView.activeSelf);
                }
                else
                {
                    UIManager.Instance.ShowLast();
                }
            }
        }
    }
}
