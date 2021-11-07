using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.UI
{
    public class SettingsView : View
    {
        public void BackButton()
        {
            UIManager.Instance.ShowLast();
        }
    }

}