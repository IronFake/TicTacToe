using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TicTacToe.Core
{
    public class MemberInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerNameField;
        [SerializeField] private TextMeshProUGUI winsField;

        public void UpdateInfo(string playerName, int wins)
        {
            playerNameField.text = playerName;
            winsField.text = wins.ToString();
        }
    }
}
