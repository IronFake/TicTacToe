using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Loading
{
    public class MenuUnloadingOperation : ILoadingOperation
    {
        public string Description => "Unload menu...";
        
        public async Task Execute(Action<float> onProgress)
        {
            onProgress?.Invoke(0.2f);
            
            AsyncOperation loadOp = SceneManager.UnloadSceneAsync(Constants.GetSceneIndex(Constants.EScene.MainMenu));
            
            while (loadOp.isDone == false)
            {
                await Task.Delay(1000);
            }
            
            onProgress?.Invoke(1f);
        }
    }
}