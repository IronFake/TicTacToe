using System;
using IronFake.Utils;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Player
{
    public class PlayerHolder : SingletonPersistent<PlayerHolder>
    {
        private Player _player;

        public Player Player => _player;
        
        public Action<Player> onPlayerUpdateAction;
        
        private void Start()
        {
            if (TryToLoadPlayer(ref _player) == false)
            {
                CreatePlayer();
            }
        }

        private void OnApplicationQuit()
        {
            SavePlayer();
        }

        private bool TryToLoadPlayer(ref Player player)
        {
            var playerData = PlayerPrefs.GetString(Constants.PlayerPrefsKeys.PLAYER_DATA, String.Empty);
            if (string.IsNullOrEmpty(playerData))
                return false;

            player = JsonUtility.FromJson<Player>(playerData);
            onPlayerUpdateAction?.Invoke(_player);
            return true;
        }
        
        private void CreatePlayer()
        {
            ModalDialog modalDialog = ModalDialog.Instance;
            modalDialog.ShowChangeNameDialog(() =>
            {
                _player = new Player();
                UpdateName(modalDialog.InputString);
            });
        }

        private void SavePlayer()
        {
            var playerData = JsonUtility.ToJson(_player);
            PlayerPrefs.SetString(Constants.PlayerPrefsKeys.PLAYER_DATA, playerData);
            PlayerPrefs.Save();
        }

        public void UpdateName(string playerName)
        {
            _player.name = playerName;
            onPlayerUpdateAction?.Invoke(_player);
        }

        public void UpdateScore(int value)
        {
            _player.exp += value;
            onPlayerUpdateAction?.Invoke(_player);
        }
    }
}
