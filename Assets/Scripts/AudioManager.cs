using System;
using IronFake.Utils;
using UnityEngine;

namespace TicTacToe.Audio
{
    public class AudioManager : SingletonPersistent<AudioManager>
    {
        [SerializeField] private AudioSource musicSource;

        private bool _musicEnable;
        private float _musicVolume;

        public bool MusicEnable => _musicEnable;
        public float MusicVolume => _musicVolume;
        
        private void Start()
        {
            _musicEnable = PlayerPrefs.GetInt(Constants.PlayerPrefsKeys.BACKGROUND_MUSIC_ON, 1) != 0;
            _musicVolume = PlayerPrefs.GetFloat(Constants.PlayerPrefsKeys.BACKGROUND_MUSIC_VOLUME, 0.5f);
            
            EnableMusic(_musicEnable);
            SetVolume(_musicVolume);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat(Constants.PlayerPrefsKeys.BACKGROUND_MUSIC_VOLUME, _musicVolume);
            PlayerPrefs.SetInt(Constants.PlayerPrefsKeys.BACKGROUND_MUSIC_ON, _musicEnable ? 1 : 0);
        }

        public void SetVolume(float value)
        {
            _musicVolume = value;
            if (_musicVolume == 0)
            {
                musicSource.enabled = false;
            }
            else
            {
                musicSource.enabled = _musicEnable;
                musicSource.volume = _musicVolume;
            }
        }

        public void EnableMusic(bool on)
        {
            _musicEnable = on;
            musicSource.enabled = _musicEnable;
        }
    }
}