using System;
using UnityEngine;

public class TetrisGameController : MonoBehaviour
{
    public InputView InputView;
    public AbstractBoardView BoardView;
    public AbstractScoreView ScoreView;

    public TetrisStageConfig StageConfig;
    public TetrisBlockGroup BlockGroup;
    
    private TetrisBoard _board;
    private TetrisBoardController _boardController;
    private TetrisBlockGroupController _blockGroupController;
    private TetrisBlockGroupSpawner _spawner;
    private TetrisUpdateStepController _updateStepController;
    private TetrisInputController _inputController;
    private TetrisGravityController _gravity;
    private TetrisScore _score;
    private TetrisScoreController _scoreController;
    private bool _firstFrameSpawned;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        IsGameOver = false;
        _board = new TetrisBoard(StageConfig.BoardSizeX, StageConfig.BoardSizeY);
        _boardController = new TetrisBoardController(_board);
        _blockGroupController = new TetrisBlockGroupController(_boardController);
        _spawner = new TetrisBlockGroupSpawner(_board, _boardController);
        _updateStepController = new TetrisUpdateStepController(StageConfig);
        _inputController = new TetrisInputController(this, _blockGroupController, StageConfig);
        _gravity = new TetrisGravityController(StageConfig, _boardController);

        _score = new TetrisScore();
        _scoreController = new TetrisScoreController(StageConfig, _score);
                
        InputView.SetTetrisInputController(_inputController);
        BoardView.SetBoard(_board);
        ScoreView.SetScore(_score);
    }

    private void OnEnable()
    {
        _spawner.Spawn(BlockGroup);
    }

    private void Update()
    {
        if (!StageConfig.EnableUpdateStepBoundInput)
        {
            _inputController.Update();
        }

        // update step control
        if (!_updateStepController.ShouldUpdate())
        {
            return;
        }

        // input
        if (StageConfig.EnableUpdateStepBoundInput)
        {
            // adding a frame to reset input and avoid late input moving the new group
            if (_firstFrameSpawned)
            {
                _inputController.Reset();
                _firstFrameSpawned = false;
            }
            else
            {
                _inputController.Update();
            }
        }

        // physics
        _gravity.Update();
        if (!_gravity.HitBottom)
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
        if (_spawner.Spawn(BlockGroup))
        {
            _firstFrameSpawned = true;
        }
        else
        {
            IsGameOver = true;
            enabled = false;
            Debug.Log("GameOver");
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
