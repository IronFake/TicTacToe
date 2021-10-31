using System;
using System.Threading.Tasks;

namespace DefaultNamespace
{
    public class LoadingDelayOperation : ILoadingOperation
    {
        public string Description => "Test operation...";
        
        public async Task Load(Action<float> onProgress)
        {
            float progress = 0f;
            float parts = 2;
            for (int i = 0; i < parts; i++)
            {
                progress += 1 / parts;
                onProgress.Invoke(progress);
                await Task.Delay(1000);
            }
        }
    }
}