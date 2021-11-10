using System;
using TicTacToe.Audio;
using TicTacToe.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI
{
    public class SettingsView : View
    {
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Toggle musicToggle;

        private AudioManager _audioManager;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
        }

        private void OnEnable()
        {
            musicVolumeSlider.value = _audioManager.MusicVolume;
            musicToggle.isOn = _audioManager.MusicEnable;
        }

        public void BackButton()
        {
            UIManager.Instance.ShowLast();
        }

        public void MusicVolume(float value)
        {
            _audioManager.SetVolume(value);
        }

        public void EnableMusic(bool isOn)
        {
            _audioManager.EnableMusic(isOn);
        }
        
        public void ChangeName()
        {
            var modalDialog = ModalDialog.Instance;
            modalDialog.ShowChangeNameDialog(() =>
            {
                PlayerHolder.Instance.UpdateName(modalDialog.InputString);
            });
        }
    }
}