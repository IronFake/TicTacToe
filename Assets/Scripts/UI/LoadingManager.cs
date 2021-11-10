using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Core;
using TicTacToe.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Loading
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        [SerializeField] private Constants.EScene startingScene;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI loadingInfo;
        [SerializeField] private ProgressBar progressBar;

        protected override void Awake()
        {
            base.Awake();
            Load(startingScene);
        }

        public void Load(Constants.EScene scene)
        {
            Queue<ILoadingOperation> operations = new Queue<ILoadingOperation>();
            switch (scene)
            {
                case Constants.EScene.MainMenu:
                    operations.Enqueue(new MenuLoadingOperation());
                    operations.Enqueue(new DelayOperation());
                    Load(operations);
                    break;
                case Constants.EScene.Core:
                    operations.Enqueue(new CoreLoadingOperation());
                    operations.Enqueue(new DelayOperation());
                    Load(operations);
                    break;
            }
        }
        
        public void StartingGame()
        {
            Queue<ILoadingOperation> operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new MenuUnloadingOperation());
            operations.Enqueue(new CoreLoadingOperation());
            operations.Enqueue(new DelayOperation());
            Load(operations);
        }

        public void EndingGame()
        {
            Queue<ILoadingOperation> operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new CoreUnloadingOperation());
            operations.Enqueue(new MenuLoadingOperation());
            operations.Enqueue(new DelayOperation());
            Load(operations);
        }
        
        private async void Load(Queue<ILoadingOperation> loadingOperations)
        {
            canvas.enabled = true;
            
            foreach (var operation in loadingOperations)
            {
                progressBar.ResetFill();
                loadingInfo.text = operation.Description;
        
                await operation.Execute(progressBar.UpdateProgress);
            }
        
            canvas.enabled = false;
        }
    }
}