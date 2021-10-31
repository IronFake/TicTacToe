using System;
using System.Threading.Tasks;
using TicTacToe.Constants;
using UnityEngine.SceneManagement;

public class MenuLoadingOperation : ILoadingOperation
{
    public string Description => "Main menu loading...";

    public async Task Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.2f);

        var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU, LoadSceneMode.Additive);
        while (loadOp.isDone == false)
        {
            await Task.Delay(1000);
        }

        onProgress?.Invoke(1f);
    }
}