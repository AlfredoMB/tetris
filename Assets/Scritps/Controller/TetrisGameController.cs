using UnityEngine;

public class TetrisGameController : MonoBehaviour
{
    public TetrisStageConfig StageConfig;

    private TetrisBoardController _boardController;
    private TetrisBlockGroupController _blockGroupController;
    private TetrisBlockGroupSpawner _spawner;
    private TetrisUpdateStepController _updateStepController;
    private TetrisGravityController _gravity;
    private TetrisScoreController _scoreController;
    private bool _firstFrameSpawned;

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
        _spawner = new TetrisBlockGroupSpawner(Board, _boardController, StageConfig);
        _updateStepController = new TetrisUpdateStepController(StageConfig);
        InputController = new TetrisInputController(this, _blockGroupController, StageConfig);
        _gravity = new TetrisGravityController(StageConfig, _boardController);

        Score = new TetrisScore();
        _scoreController = new TetrisScoreController(StageConfig, Score);
    }

    private void Update()
    {
        // optional free input
        if (!StageConfig.EnableUpdateStepBoundInput)
        {
            InputController.Update();
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
                InputController.Reset();
                _firstFrameSpawned = false;
            }
            else
            {
                InputController.Update();
            }
        }

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
