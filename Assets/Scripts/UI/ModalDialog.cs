using System;
using IronFake.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI
{
    public class ModalDialog : SingletonPersistent<ModalDialog>
    {
        [SerializeField] private Canvas canvas;
        
        [Header("HeaderArea")]
        [SerializeField] private TextMeshProUGUI titleText;
        
        [Header("ContentArea")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TextMeshProUGUI messageText;
        
        [Header("ButtonsArea")]
        [SerializeField] private Button confirmButton;
        [SerializeField] private TextMeshProUGUI confirmButtonText;
        [SerializeField] private Button declineButton;
        [SerializeField] private TextMeshProUGUI declineButtonText;

        public string InputString => inputField.text;
        
        private Action _onConfirmCallback;
        private Action _onDeclineCallback;

        public void ShowChangeNameDialog(Action confirmAction)
        {
            ShowDialog("Enter the name", String.Empty, true, "Confirm", confirmAction);
        }
        
        public void ShowDialog(string title, string message, bool hasInput, 
            string confirmText, Action confirmAction, string declineText = null, Action declineAction = null)
        {
            canvas.enabled = true;
            
            titleText.text = title;

            bool hasMessage = string.IsNullOrEmpty(message) == false;
            messageText.gameObject.SetActive(hasMessage);
            messageText.text = message;
            
            inputField.gameObject.SetActive(hasInput);

            confirmButtonText.text = confirmText;
            _onConfirmCallback = confirmAction;
            
            bool hasDeclineAction = declineAction != null;
            declineButton.gameObject.SetActive(hasDeclineAction);
            declineButtonText.text = declineText;
            _onDeclineCallback = declineAction;
        }
        
        public void Confirm()
        {
            _onConfirmCallback?.Invoke();
            Close();
        }

        public void Decline()
        {
            _onDeclineCallback?.Invoke();
            Close();
        }

        private void Close()
        {
            canvas.enabled = false;
        }
    }
}
