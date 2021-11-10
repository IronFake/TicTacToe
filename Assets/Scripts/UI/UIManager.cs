using System;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private View startingView;
        [SerializeField] private View[]  views;

        public bool IsStartingView => _history.Count == 0;
        
        private View _currentView;
        private readonly Stack<View> _history = new Stack<View>();

        private void Start()
        {
            if (startingView != null)
            {
                _currentView = startingView;
            }
        }

        public void Show(View view, bool remember = true)
        {
            if (_currentView != null)
            {
                if (remember)
                {
                    _history.Push(_currentView);
                }
                
                _currentView.Hide();
            }
            
            view.Show();
            _currentView = view;
        }
        
        public void Show(ViewType viewType, bool remember = true)
        {
            View view = GetView(viewType);
            if (view != null)
            {
                Show(view, remember);
            }
        }

        private View GetView(ViewType viewType)
        {
            for (int i = 0; i < views.Length; i++)
            {
                if (views[i].ViewType == viewType)
                {
                    return views[i];
                }
            }

            return null;
        }

        public void ShowLast()
        {
            if (_history.Count != 0)
            {
                Show(_history.Pop(), false);
            }
        }
    }

    public enum ViewType
    {
        MainMenu,
        Pause,
        Settings,
        MatchSettings,
        MatchResult
    }
}