using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Constants;
using TicTacToe.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreLoadingOperation : ILoadingOperation
{
    public string Description => "Starting game...";
    
    public async Task Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.2f);

        var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.CORE, LoadSceneMode.Additive);
        while (loadOp.isDone == false)
        {
            await Task.Delay(1000);
        }
        
        onProgress?.Invoke(1f);
    }
}
