namespace TicTacToe.Core.Bot
{
    public interface IBotStrategy
    {
        bool TryToChooseCell(out CellCoordinates chosenCell);
    }
}