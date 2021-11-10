using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Loading
{
    public class DelayOperation : ILoadingOperation
    {
    public string Description => "Delay...";

    public async Task Execute(Action<float> onProgress)
    {
        onProgress?.Invoke(0.5f);
            
        await Task.Delay(2000);
            
        onProgress?.Invoke(1f);
    }
    }
}