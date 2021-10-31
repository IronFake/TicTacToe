using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Core
{
    public class Board
    {
        public enum Mark
        {
            O = 1,
            X = 2,
            None = 0
        }
        
        private readonly byte[,] _board;
        
        public Board(int boardSize)
        {
            _board = new byte[boardSize,boardSize];
        }
        
        public bool TryToPlaceMark(CellCoordinates cellCoordinates, Mark mark)
        {
            if (cellCoordinates.x < 0 || cellCoordinates.x > _board.GetUpperBound(0))
                return false;

            if (cellCoordinates.y < 0 || cellCoordinates.y > _board.GetUpperBound(1))
                return false;

            if (CellIsFree(cellCoordinates) == false)
                return false;

            _board[cellCoordinates.x, cellCoordinates.y] = (byte) mark;
            return true;
        }

        private bool CellIsFree(CellCoordinates coord)
        {
            return _board[coord.x, coord.y] == 0;
        }

        private bool CellIsFree(int x, int y)
        {
            return _board[x, y] == 0;
        }
        
        public bool CheckWinState()
        {
            if (CheckHorizontalLines())
                return true;

            if (CheckVerticalLines())
                return true;

            if (CheckStraightDiagonal())
                return true;

            if (CheckReverseDiagonal())
                return true;

            return false;
        }

        private bool CheckHorizontalLines()
        {
            int firstDimensionBound = _board.GetUpperBound(0);
            int secondDimensionBound = _board.GetUpperBound(1);
            for (int i = 0; i <= firstDimensionBound; i++)
            {
                byte firstSymbol = _board[i,0];
                if(firstSymbol == 0)
                    continue;
                
                bool haveWinLine = true;
                for (int j = 1; j <= secondDimensionBound; j++)
                {
                    haveWinLine &= firstSymbol == _board[i,j];
                }

                if (haveWinLine)
                    return true;
            }

            return false;
        }

        private bool CheckVerticalLines()
        {
            int firstDimensionBound = _board.GetUpperBound(0);
            int secondDimensionBound = _board.GetUpperBound(1);
            for (int i = 0; i <= secondDimensionBound; i++)
            {
                byte firstSymbol = _board[0,i];
                if(firstSymbol == 0)
                    continue;
                
                bool haveWinLine = true;
                for (int j = 1; j <= firstDimensionBound; j++)
                {
                    haveWinLine &= firstSymbol == _board[j,i];
                }

                if (haveWinLine)
                    return true;
            }

            return false;
        }

        private bool CheckStraightDiagonal()
        {
            int firstDimensionBound = _board.GetUpperBound(0);
            byte firstSymbol = _board[0,0];
            if(firstSymbol == 0)
                return false;
            
            bool haveWinLine = true;
            for (int i = 1; i <= firstDimensionBound; i++)
            {
                haveWinLine &= firstSymbol == _board[i,i];
            }

            return haveWinLine;
        }
        
        private bool CheckReverseDiagonal()
        {
            int firstDimensionBound = _board.GetUpperBound(0);
            byte firstSymbol = _board[firstDimensionBound, 0];
            if(firstSymbol == 0)
                return false;
            
            bool haveWinLine = true;
            for (int i = 1; i <= firstDimensionBound; i++)
            {
                haveWinLine &= firstSymbol == _board[firstDimensionBound - i, i];
            }

            return haveWinLine;
        }

        public bool HaveFreeCell()
        {
            return GetFreeCells().Count != 0;
        }

        public List<CellCoordinates> GetFreeCells()
        {
            List<CellCoordinates> freeCells = new List<CellCoordinates>();
            for (int i = 0; i <= _board.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= _board.GetUpperBound(1); j++)
                {
                    if (CellIsFree(i, j))
                    {
                        freeCells.Add(new CellCoordinates() {x = i, y = j});
                    }
                }
            }

            return freeCells;
        }
    }
}
