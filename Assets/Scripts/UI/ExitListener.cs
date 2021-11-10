using System;
using System.Collections;
using System.Collections.Generic;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe
{
    public class ExitListener : MonoBehaviour
    {
        [SerializeField] private KeyCode exitKeyCode;
        
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
                    ModalDialog.Instance.ShowDialog("Exit Game", "Are you sure want to quit the game?", false, 
                        "Yes",() => Application.Quit(), "Back", () => {});
                }
                else
                {
                    UIManager.Instance.ShowLast();
                }
            }
        }
    }
}
