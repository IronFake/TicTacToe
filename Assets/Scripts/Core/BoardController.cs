using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Core
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private BoardCell boardCellPrefab;
        [SerializeField] private Transform gridTransform;
        [SerializeField] private Board.Mark firstTurnMark;

        private int _boardSize;
        private Board _board;
        private BoardCell[,] _boardCells;
        private Board.Mark _turn;
        private BoardState _boardStatus;
        
        public Board Board => _board;
        public Board.Mark Turn => _turn;
        public BoardState State => _boardStatus;
        public bool CanContinue => _boardStatus == BoardState.None;
        
        public void InitBoard(int boardSize)
        {
            DeleteBoardCells();
            _boardSize = boardSize;
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

            _turn = firstTurnMark;
        }

        private void DeleteBoardCells()
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
            if (_board.TryToPlaceMark(boardCell.Coordinates, _turn))
            {
                boardCell.SetIcon(_turn);
                UpdateBoardState();
                return true;
            }

            return false;
        }

        private void UpdateBoardState()
        {
            if (_board.CheckWinState())
            {
                _boardStatus = _turn == Board.Mark.X ? BoardState.CrossWin : BoardState.RingWin;
                return;
            }

            if (_board.HaveFreeCell() == false)
            {
                _boardStatus = BoardState.Draw;
                return;
            }

            _turn = _turn == Board.Mark.X ? Board.Mark.O : Board.Mark.X;
            _boardStatus = BoardState.None;
        }
        
        public enum BoardState
        {
            CrossWin,
            RingWin,
            Draw,
            None
        }
    }
}