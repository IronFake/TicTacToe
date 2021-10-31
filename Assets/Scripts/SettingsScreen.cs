using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.UI
{
    public class SettingsScreen : MonoBehaviour
    {
        public void BackButton()
        {
            SceneManager.UnloadSceneAsync(Constants.Constants.Scenes.SETTINGS);
        }
    }

}