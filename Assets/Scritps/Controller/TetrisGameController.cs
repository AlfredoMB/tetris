using System;
using UnityEngine;

public class TetrisGameController : MonoBehaviour
{
    public TetrisStageConfig StageConfig;
    public event Action OnGameOver;

    private TetrisBoardController _boardController;
    private TetrisBlockGroupController _blockGroupController;
    private TetrisBlockGroupSpawnController _spawner;
    private TetrisUpdateStepController _updateStepController;
    private TetrisGravityController _gravity;
    private TetrisScoreController _scoreController;
    
    public TetrisBoard Board { get; private set; }
    public TetrisInputController InputController { get; private set; }
    public TetrisScore Score { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        IsGameOver = false;
        Board = new TetrisBoard(StageConfig.BoardSizeX, StageConfig.BoardSizeY);
        _boardController = new TetrisBoardController(Board);
        _blockGroupController = new TetrisBlockGroupController(_boardController);
        _spawner = new TetrisBlockGroupSpawnController(Board, _boardController, StageConfig);
        _updateStepController = new TetrisUpdateStepController(StageConfig);
        InputController = new TetrisInputController(this, _blockGroupController, StageConfig);
        _gravity = new TetrisGravityController(StageConfig, _boardController);

        Score = new TetrisScore();
        _scoreController = new TetrisScoreController(StageConfig, Score);
    }

    private void Update()
    {
        // update step control
        if (!_updateStepController.ShouldUpdate())
        {
            return;
        }

        // input
        InputController.Reload();

        // physics
        _gravity.Update();
        if (!_gravity.DidCurrentBlockGroupHitBottom)
        {
            return;
        }

        // game logic
        // consume lines
        int linesConsumed = _boardController.ConsumeFullLines();
        if (linesConsumed > 0)
        {
            _scoreController.AddLinesConsumed(linesConsumed);
            return;
        }

        // fall flying blocks
        if (StageConfig.EnableBlocksWithoutNeighborsBelowToFall
            && _boardController.FallBlocksWithoutNeighborsBelowIntoPlace())
        {
            return;
        }

        // spawn new blockgroup
        if (_spawner.Spawn())
        {
            return;
        }
        else
        {
            IsGameOver = true;
            enabled = false;
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }
    }

    public void Restart()
    {
        if (!IsGameOver)
        {
            return;
        }
        IsGameOver = false;
        enabled = true;

        _boardController.Reset();
        _scoreController.Reset();
        _updateStepController.Reset();
    }
}
