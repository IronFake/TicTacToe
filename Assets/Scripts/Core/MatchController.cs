using System;
using System.Collections;
using TicTacToe.Core.Bot;
using TicTacToe.Loading;
using TicTacToe.Player;
using TicTacToe.UI;
using TMPro;
using UnityEngine;

namespace TicTacToe.Core
{
    public class MatchController : Singleton<MatchController>
    {
        [SerializeField] private int matchPoints = 100;
        [SerializeField] private BoardController boardController;
        [SerializeField] private Animation endMatchAnimation;
        [SerializeField] private Stopwatch stopwatch;
        [SerializeField] private TextMeshProUGUI firstPlayerName;
        [SerializeField] private TextMeshProUGUI opponentPlayerName;
        [SerializeField] private TextMeshProUGUI firstPlayerWinStreak;
        [SerializeField] private TextMeshProUGUI opponentPlayerWinStreak;

        private bool PlayerTurn => _playerTurnFirst == boardController.FirstPlayerTurn;
        
        private GameConfig _gameConfig;
        private bool _playerTurnFirst;
        private GameResult _matchResult;
        private IBotStrategy _botStrategy;
        
        private void Start()
        {
            _matchResult = new GameResult();
            InitMatch(DataHolder.Instance.GameConfig);
        }

        private void InitMatch(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _playerTurnFirst = gameConfig.playerTurnFirst;
            InitPlayerNames();
            InitBoard();
            stopwatch.StartStopWatch();
        }
        
        private void InitPlayerNames()
        {
            firstPlayerName.text = _gameConfig.player.name;
            opponentPlayerName.text = _gameConfig.opponent.name;
        }

        private void InitBoard()
        {
            boardController.InitBoard(_gameConfig.boardSize);
            _botStrategy = new RandomStrategy(boardController.Board);
            
            if (_playerTurnFirst == false)
            {
                BotTurn();
            }
        }
        
        public void PlaceMark(BoardCell boardCell)
        {
            if (PlayerTurn)
            {
                boardController.TryPlaceMark(boardCell);
                if (boardController.CanContinue)
                {
                    BotTurn();
                    if (boardController.CanContinue)
                    {
                        return;
                    }
                }
                
                EndMatch();
            }
        }
        
        private void BotTurn()
        {
            _botStrategy.TryToChooseCell(out CellCoordinates chosenCell);
            boardController.TryPlaceMark(chosenCell);
        }

        private void EndMatch()
        {
            BoardController.BoardState currentBoardState = boardController.BoardStatus;
            switch (currentBoardState)
            {
                case BoardController.BoardState.HaveWinRow:
                    if (PlayerTurn)
                    {
                        _matchResult.playerWinCount += 1;
                        if (_matchResult.playerWinCount < _gameConfig.targetWinCount)
                        {
                            firstPlayerWinStreak.text = _matchResult.playerWinCount.ToString();
                            InitBoard();
                        }
                        else
                        {
                            StartCoroutine(PlayEndAnimation(currentBoardState, true));
                        }
                    }
                    else
                    {
                        _matchResult.opponentWinCount += 1;
                        if (_matchResult.opponentWinCount < _gameConfig.targetWinCount)
                        {
                            opponentPlayerWinStreak.text = _matchResult.opponentWinCount.ToString();
                            InitBoard();
                        }
                        else
                        {
                            StartCoroutine(PlayEndAnimation(currentBoardState, false));
                        }
                    }
                    break;
                case BoardController.BoardState.Draw:
                    InitBoard();
                    //StartCoroutine(PlayEndAnimation(currentBoardState));
                    break;
            }
        }

        IEnumerator PlayEndAnimation(BoardController.BoardState boardState, bool win = false)
        {
            stopwatch.StopStopwatch();
            switch (boardState)
            {
                case BoardController.BoardState.HaveWinRow:
                    if (win)
                    {
                        endMatchAnimation.gameObject.SetActive(true);
                        endMatchAnimation.Play("WinAnimation");
                    }
                    else
                    {
                        endMatchAnimation.gameObject.SetActive(true);
                        endMatchAnimation.Play("LoseAnimation");
                    }
                    break;
                case BoardController.BoardState.Draw:
                    endMatchAnimation.gameObject.SetActive(true);
                    endMatchAnimation.Play("DrawAnimation");
                    break;
            }

            yield return new WaitForSeconds(endMatchAnimation.clip.length + 2);

            _matchResult.winner = win ? _gameConfig.player : _gameConfig.opponent;
            _matchResult.durationGame = stopwatch.StopwatchTime;
            _matchResult.pointsForMatch = win ? matchPoints : -matchPoints;
            
            PlayerHolder.Instance.UpdateScore(_matchResult.pointsForMatch);
            DataHolder.Instance.GameResult = _matchResult;
            LoadingManager.Instance.EndingGame();
        }
    }
}
