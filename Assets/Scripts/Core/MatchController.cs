using System.Collections;
using TicTacToe.Core.Bot;
using TicTacToe.Loading;
using TicTacToe.Player;
using TicTacToe.UI;
using UnityEngine;

namespace TicTacToe.Core
{
    public class MatchController : Singleton<MatchController>
    {
        [SerializeField] private int matchPoints = 100;
        [SerializeField] private BoardController boardController;
        [SerializeField] private Animation endMatchAnimation;
        [SerializeField] private Stopwatch stopwatch;
        [SerializeField] private MemberInfo playerForCrossInfo;
        [SerializeField] private MemberInfo playerForRingInfo;
        
        private bool PlayerTurn => _playerMark == boardController.Turn;

        private readonly string _drawAnimationName = "DrawAnimation";
        private readonly string _winAnimationName = "WinAnimation";
        private readonly string _loseAnimationName = "LoseAnimation";
        
        private GameConfig _matchConfig;
        private Board.Mark  _playerMark;
        private GameResult _matchResult;
        private IBotStrategy _botStrategy;
        private bool _alreadyTurn;
        
        private void Start()
        {
            InitMatch(DataHolder.Instance.GameConfig);
        }

        private void InitMatch(GameConfig gameConfig)
        {
            _matchResult = new GameResult();
            _matchConfig = gameConfig;
            _playerMark = _matchConfig.playerMark;
            StartMatch();
            
            stopwatch.StartStopWatch();
        }
        
        private void UpdateMemberInfo()
        {
            playerForCrossInfo.UpdateInfo(_matchConfig.playerForCross.name, _matchResult.crossWins);
            playerForRingInfo.UpdateInfo(_matchConfig.playerForRing.name, _matchResult.ringWins);
        }

        private void StartMatch()
        {
            boardController.InitBoard(_matchConfig.boardSize);
            _botStrategy = new RandomStrategy(boardController.Board);
            
            endMatchAnimation.gameObject.SetActive(false);
            UpdateMemberInfo();
            _alreadyTurn = false;
            
            if (!PlayerTurn)
            {
                BotTurn();
            }
        }
        
        public void PlaceMark(BoardCell boardCell)
        {
            if (PlayerTurn && _alreadyTurn == false)
            {
                _alreadyTurn = true;
                if (boardController.TryPlaceMark(boardCell))
                {
                    if (boardController.CanContinue)
                    {
                        BotTurn();
                        return;
                    }
                    
                    CheckBoardState();
                    return;
                }
                
                _alreadyTurn = false;
            }
        }
        
        private void BotTurn()
        {
            _botStrategy.TryToChooseCell(out CellCoordinates chosenCell);
            boardController.TryPlaceMark(chosenCell);
            CheckBoardState();
            _alreadyTurn = false;
        }

        private void CheckBoardState()
        {
            BoardController.BoardState currentBoardState = boardController.State;
            if(currentBoardState == BoardController.BoardState.None)
                return;
            
            switch (currentBoardState)
            {
                case BoardController.BoardState.CrossWin:
                    _matchResult.crossWins += 1;
                    break;
                case BoardController.BoardState.RingWin:
                    _matchResult.ringWins += 1;
                    break;
            }
            
            StartCoroutine(PlayEndAnimation(currentBoardState));
        }

        IEnumerator PlayEndAnimation(BoardController.BoardState boardState)
        {
            if (boardState == BoardController.BoardState.Draw)
            {
                endMatchAnimation.gameObject.SetActive(true);
                endMatchAnimation.Play(_drawAnimationName);
            }
            else
            {
                if (PlayerTurn)
                {
                    endMatchAnimation.gameObject.SetActive(true);
                    endMatchAnimation.Play(_winAnimationName);
                }
                else
                {
                    endMatchAnimation.gameObject.SetActive(true);
                    endMatchAnimation.Play(_loseAnimationName);
                }
            }
            
            yield return new WaitForSeconds(endMatchAnimation.clip.length + 2);
            CheckEndMatch();
        }

        private void CheckEndMatch()
        {
            if (_matchResult.crossWins >= _matchConfig.targetWinCount ||
                _matchResult.ringWins >= _matchConfig.targetWinCount)
            {
                EndMatch();
            }
            else
            {
                StartMatch();
            }
        }

        private void EndMatch()
        {
            stopwatch.StopStopwatch();
            _matchResult.winner = boardController.Turn == Board.Mark.X ? _matchConfig.playerForCross : _matchConfig.playerForRing;
            _matchResult.pointsForMatch = PlayerTurn ? matchPoints : -matchPoints;
            _matchResult.durationGame = stopwatch.StopwatchTime;
            
            PlayerHolder.Instance.UpdateScore(_matchResult.pointsForMatch);
            DataHolder.Instance.GameResult = _matchResult;
            LoadingManager.Instance.EndingGame();
        }
    }
}
