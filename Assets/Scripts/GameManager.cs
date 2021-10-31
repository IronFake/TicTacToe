using System;
using TicTacToe.Core.Bot;
using UnityEngine;

namespace TicTacToe.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private BoardController boardController;
        
        private IBotStrategy _botStrategy;
        private GameConfig _gameConfig;

        private bool _playerTurnFirst;

        public bool PlayerTurn => _playerTurnFirst && boardController.FirstPlayerTurn;

        [ContextMenu("CreateGame")]
        public void CreateGame()
        {
            GameConfig newGameConfig = new GameConfig();
            newGameConfig.boardSize = 3;
            newGameConfig.winningStreak = 1;
            newGameConfig.playerTurnFirst = true;
            CreateGame(newGameConfig);
        }
        
        public void CreateGame(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            
            boardController.InitBoard(gameConfig.boardSize);
            _botStrategy = new RandomStrategy(boardController.Board);
            
            _playerTurnFirst = gameConfig.playerTurnFirst;
        }

        public void PlaceMark(BoardCell boardCell)
        {
            if (PlayerTurn)
            {
                boardController.TryPlaceMark(boardCell);
                if (boardController.CanContinue)
                {
                    _botStrategy.TryToChooseCell(out CellCoordinates chosenCell);
                    boardController.TryPlaceMark(chosenCell);
                    if (boardController.CanContinue)
                    {
                        return;
                    }
                }
                
                EndingGame();
            }
        }

        private void EndingGame()
        {
            BoardController.BoardState currentBoardState = boardController.BoardStatus;
            switch (currentBoardState)
            {
                case BoardController.BoardState.Win:
                    Debug.Log("Win");
                    break;
                case BoardController.BoardState.Draw:
                    Debug.Log("Draw");
                    break;
            }
        }
    }
}
