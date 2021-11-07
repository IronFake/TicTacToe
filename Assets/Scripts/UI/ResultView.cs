using System;
using System.Globalization;
using TicTacToe.Core;
using TicTacToe.Player;
using TicTacToe.UI;
using TMPro;
using UnityEngine;

namespace TicTacToe.UI
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private GameObject resultArea;
        [SerializeField] private GameObject backdoor;
        [SerializeField] private TextMeshProUGUI winnerField;
        [SerializeField] private TextMeshProUGUI durationText;
        [SerializeField] private TextMeshProUGUI playerPoints;
        
        private void Start()
        {
            GameResult gameResult = DataHolder.Instance.GameResult;
            if (gameResult.durationGame != 0)
            {
                winnerField.text = gameResult.winner.name;
                durationText.text = TimeSpan.FromSeconds(gameResult.durationGame).ToString(@"mm\:ss");

                if (gameResult.pointsForMatch > 0)
                {
                    playerPoints.text = string.Format("{0} (+{1})", 
                        PlayerHolder.Instance.Player.exp.ToString(),
                        gameResult.pointsForMatch.ToString());
                }
                else
                {
                    playerPoints.text = string.Format("{0} (-{1})", 
                        PlayerHolder.Instance.Player.exp.ToString(),
                        gameResult.pointsForMatch.ToString());
                }
                
                resultArea.SetActive(true);
                backdoor.SetActive(true);
            }
        }
    }
}