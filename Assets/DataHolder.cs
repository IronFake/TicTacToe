using System.Collections;
using System.Collections.Generic;
using IronFake.Utils;
using TicTacToe.Core;
using UnityEngine;

public class DataHolder : SingletonPersistent<DataHolder>
{
    private GameConfig _gameConfig;

    public GameConfig GameConfig
    {
        get
        {
            if (_gameConfig.boardSize == 0)
            {
                _gameConfig.boardSize = 3;
                _gameConfig.targetWinCount = 1;
                _gameConfig.playerMark = Board.Mark.X;
            }

            return _gameConfig;
        }
        set => _gameConfig = value;
    }

    private GameResult _gameResult;

    public GameResult GameResult
    {
        get => _gameResult;
        set => _gameResult = value;
    }
}
