using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TicTacToe.Loading
{
    public class CoreLoadingOperation : ILoadingOperation
    {
        public string Description => "Starting game...";
        
        public async Task Execute(Action<float> onProgress)
        {
            onProgress?.Invoke(0.2f);
            
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(
                Constants.GetSceneIndex(Constants.EScene.Core),
                LoadSceneMode.Additive);
            
            while (loadOp.isDone == false)
            {
                await Task.Delay(1000);
            }

            onProgress?.Invoke(1f);
        }
    }
}
