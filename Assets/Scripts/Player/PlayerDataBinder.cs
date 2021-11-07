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

        private void Start()
        {
            UpdateValue(PlayerHolder.Instance.Player);
        }

        private void OnEnable()
        {
            PlayerHolder.Instance.onPlayerUpdateAction += UpdateValue;
        }

        private void OnDisable()
        {
            PlayerHolder.Instance.onPlayerUpdateAction -= UpdateValue;
        }

        protected abstract void UpdateValue(Player player);
    }
}
