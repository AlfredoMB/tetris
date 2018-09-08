﻿using UnityEngine;

public class TetrisGameController : MonoBehaviour
{
    public InputView InputView;
    public AbstractBoardView BoardView;
    public AbstractScoreView ScoreView;

    public TetrisStageConfig BoardSize;
    public TetrisBlockGroup BlockGroup;
    
    private TetrisBoard _board;
    private TetrisBoardController _boardController;
    private TetrisBlockGroupController _blockGroupController;
    private TetrisBlockGroupSpawner _spawner;
    private TetrisGravityController _gravity;
    private TetrisScore _score;
    private TetrisScoreController _scoreController;

    private void Awake()
    {
        _board = new TetrisBoard(BoardSize.BoardSizeX, BoardSize.BoardSizeY);
        _boardController = new TetrisBoardController(_board);
        _blockGroupController = new TetrisBlockGroupController(_boardController);
        _spawner = new TetrisBlockGroupSpawner(_board, _boardController);
        _gravity = new TetrisGravityController(BoardSize.Gravity, BoardSize.GravityUpdateInterval, _boardController);

        _score = new TetrisScore();
        _scoreController = new TetrisScoreController(BoardSize.LineConsumptionScoreValue, _score);
                
        InputView.SetTetrisBlockGroupController(_blockGroupController);
        BoardView.SetBoard(_board);
        ScoreView.SetScore(_score);
    }

    private void OnEnable()
    {
        _spawner.Spawn(BlockGroup);
    }

    private void Update()
    {
        _gravity.Update();
        if (!_gravity.HitBottom)
        {
            return;
        }

        _spawner.Spawn(BlockGroup);

        int linesConsumed = _boardController.ConsumeFullLines();

        if (linesConsumed > 0)
        {
            _scoreController.AddScore(linesConsumed);
        }
    }
}