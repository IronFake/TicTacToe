using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Core
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private BoardCell boardCellPrefab;
        [SerializeField] private Transform gridTransform;
        [SerializeField] private Board.Mark firstTurnMark;
        
        private Board _board;
        private BoardCell[,] _boardCells;
        private bool _firstPlayerTurn;
        private BoardState _boardStatus;
        
        public Board Board => _board;
        public bool FirstPlayerTurn => _firstPlayerTurn;

        public BoardState BoardStatus => _boardStatus;

        public bool CanContinue => _boardStatus == BoardState.None;
        
        
        public void InitBoard(int boardSize)
        {
            ClearBoard();
            _board = new Board(boardSize);

            _boardCells = new BoardCell[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    BoardCell boardCell = Instantiate(boardCellPrefab, gridTransform);
                    boardCell.SetCellIndex(new CellCoordinates() {x = i, y = j});
                    _boardCells[i, j] = boardCell;
                }
            }

            _firstPlayerTurn = true;
        }

        private void ClearBoard()
        {
            foreach (Transform child in gridTransform)
            {
                Destroy(child.gameObject);
            }
        }

        public bool TryPlaceMark(CellCoordinates cellCoord)
        {
             return TryPlaceMark(_boardCells[cellCoord.x, cellCoord.y]);
        }

        public bool TryPlaceMark(BoardCell boardCell)
        {
            Board.Mark mark = GetMark();
            if (_board.TryToPlaceMark(boardCell.Coordinates, mark))
            {
                boardCell.SetIcon(mark);
                _firstPlayerTurn = !_firstPlayerTurn;
                UpdateBoardState();
                return true;
            }

            return false;
        }

        private void UpdateBoardState()
        {
            if (_board.CheckWinState())
            {
                _boardStatus = BoardState.Win;
                return;
            }

            if (_board.HaveFreeCell() == false)
            {
                _boardStatus = BoardState.Draw;
                return;
            }

            _boardStatus = BoardState.None;
        }
        
        private Board.Mark GetMark()
        {
            if (_firstPlayerTurn)
            {
                return firstTurnMark;
            }
            
            return firstTurnMark == Board.Mark.X ? Board.Mark.O : Board.Mark.X;
        }
        
        public enum BoardState
        {
            Win,
            Draw,
            None
        }
    }
}