using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.UI;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoadingScreen : Singleton<LoadingScreen>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI loadingInfo;
        [SerializeField] private ProgressBar progressBar;

        protected override void Awake()
        {
            base.Awake();
            LoadMainMenu();
        }

        public void LoadSettings()
        {
            Queue<ILoadingOperation> loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new SettingLoadingOperation());
            loadingOperations.Enqueue(new LoadingDelayOperation());
            Load(loadingOperations);
        }

        public void LoadCore()
        {
            Queue<ILoadingOperation> loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new CoreLoadingOperation());
            loadingOperations.Enqueue(new LoadingDelayOperation());
            Load(loadingOperations);
        }

        public void LoadMainMenu()
        {
            Queue<ILoadingOperation> loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(new MenuLoadingOperation());
            //loadingOperations.Enqueue(new LoadingDelayOperation());
            Load(loadingOperations);
        }
        
        private async void Load(Queue<ILoadingOperation> loadingOperations)
        {
            canvas.enabled = true;
            //StartCoroutine(UpdateProgressBar());
        
            foreach (var operation in loadingOperations)
            {
                progressBar.ResetFill();
                loadingInfo.text = operation.Description;
        
                await operation.Load(progressBar.UpdateProgress);
                await WaitForBarFill();
            }
        
            canvas.enabled = false;
        }

        private async Task WaitForBarFill()
        {
            await Task.Delay(1000);
        }
    }
}