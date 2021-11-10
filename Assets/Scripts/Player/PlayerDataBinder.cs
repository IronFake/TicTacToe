using System;
using TMPro;
using UnityEngine;

namespace TicTacToe.Player
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class PlayerDataBinder : MonoBehaviour
    {
        protected TextMeshProUGUI textField;

        private void Awake()
        {
            textField = GetComponent<TextMeshProUGUI>();
            
        }
        
        private void OnEnable()
        {
            PlayerHolder.Instance.ONPlayerUpdateAction += UpdateValue;
            UpdateValue(PlayerHolder.Instance.Player);
        }

        private void OnDisable()
        {
            PlayerHolder.Instance.ONPlayerUpdateAction -= UpdateValue;
        }

        protected abstract void UpdateValue(Player player);
    }
}
