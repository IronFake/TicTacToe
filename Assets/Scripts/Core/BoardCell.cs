using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Core
{
    public class BoardCell : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Sprite crossIcon;
        [SerializeField] private Sprite ringIcon;
        [SerializeField] private Button button;

        private CellCoordinates _cellCoordinates;

        public CellCoordinates Coordinates => _cellCoordinates;

        private void Awake()
        {
            button.onClick.AddListener(() => MatchController.Instance.PlaceMark(this));
        }

        public void SetCellIndex(CellCoordinates cellCoordinates)
        {
            _cellCoordinates = cellCoordinates;
        }
        
        public void SetIcon(Board.Mark mark)
        {
            switch (mark)
            {
                case Board.Mark.O:
                    icon.sprite = ringIcon;
                    break;
                case Board.Mark.X:
                    icon.sprite = crossIcon;
                    break;
                default:
                    icon.sprite = null;
                    break;
            }
        }
    }
}