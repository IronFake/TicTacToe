using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace TicTacToe.UI
{
    public class MainMenuScreen : MonoBehaviour
    {
        public void StartGame()
        {
            LoadingScreen.Instance.LoadCore();
        }

        public void OpenSettings()
        {
            LoadingScreen.Instance.LoadSettings();
        }
    }
}
