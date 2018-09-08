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

    private void Awake()
    {
        _board = new TetrisBoard(StageConfig.BoardSizeX, StageConfig.BoardSizeY);
        _boardController = new TetrisBoardController(_board);
        _blockGroupController = new TetrisBlockGroupController(_boardController);
        _spawner = new TetrisBlockGroupSpawner(_board, _boardController);
        _updateStepController = new TetrisUpdateStepController(StageConfig.UpdateStepInterval);
        _inputController = new TetrisInputController(_blockGroupController);
        _gravity = new TetrisGravityController(StageConfig.Gravity, _boardController);

        _score = new TetrisScore();
        _scoreController = new TetrisScoreController(StageConfig.LineConsumptionScoreValue, _score);
                
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
        if (!_updateStepController.ShouldUpdate())
        {
            return;
        }
        _inputController.Update();
        _gravity.Update();
        if (!_gravity.HitBottom)
        {
            return;
        }

        if (_spawner.Spawn(BlockGroup))
        {
            int linesConsumed = _boardController.ConsumeFullLines();

            if (linesConsumed > 0)
            {
                _scoreController.AddScore(linesConsumed);
            }
        }
        else
        {
            Debug.Log("GameOver");
        }
    }
}
