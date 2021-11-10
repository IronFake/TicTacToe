using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Loading
{
    public class MenuLoadingOperation : ILoadingOperation
    {
        public string Description => "Main menu loading...";

        public async Task Execute(Action<float> onProgress)
        {
            onProgress?.Invoke(0.2f);
            
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(
                Constants.GetSceneIndex(Constants.EScene.MainMenu),
                LoadSceneMode.Additive);
            
            while (loadOp.isDone == false)
            {
                await Task.Delay(1000);
            }
            
            SceneManager.SetActiveScene(
                SceneManager.GetSceneByBuildIndex(Constants.GetSceneIndex(Constants.EScene.MainMenu)));
            
            onProgress?.Invoke(1f);
        }
    }
}