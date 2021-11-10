using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe.UI
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        [SerializeField] private int updateTextInterval = 1;
        
        private bool _stopwatchActive = false;
        private float _currentTime;
        private float _lastUpdateTime;

        public float StopwatchTime => _currentTime;
        
        private void Start()
        {
            _currentTime = 0;
        }

        private void Update()
        {
            if (_stopwatchActive)
            {
                _currentTime += Time.deltaTime;
            }

            if (_currentTime - _lastUpdateTime > updateTextInterval)
            {
                TimeSpan time = TimeSpan.FromSeconds(_currentTime);
                textField.text = time.ToString(@"mm\:ss");
                _lastUpdateTime = _currentTime;
            }
        }
        
        [ContextMenu("Start")]
        public void StartStopWatch()
        {
            _stopwatchActive = true;
        }

        [ContextMenu("Stop")]
        public void StopStopwatch()
        {
            _stopwatchActive = false;
        }
    }
}
