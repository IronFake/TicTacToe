using System;
using System.Collections.Generic;

namespace TicTacToe.Core.Bot
{
    public class RandomStrategy : IBotStrategy
    {
        private readonly Board _board;
        
        public RandomStrategy(Board board)
        {
            _board = board;
        }
        
        public bool TryToChooseCell(out CellCoordinates chosenCell)
        {
            List<CellCoordinates> freeCells = _board.GetFreeCells();
            chosenCell = new CellCoordinates();
            if (freeCells.Count == 0)
                return false;

            Random random = new Random();
            chosenCell = freeCells[random.Next(0, freeCells.Count)];
            return true;
        }
    }
}