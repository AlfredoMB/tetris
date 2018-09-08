/// <summary>
/// As by description: 
/// "- Match the controls to that of Tetris, click/holding down the Left/Right keys should only move the block once per update step
/// - Clicking the Down key should shift the block down once per update step, holding down the key should speed up the downward movement of the block
/// - Use [F] and [G] keys to rotate blocks by clockwise and counterclockwise orientation (match exactly with Tetris)"
/// </summary>
public class TetrisInputController
{
    private enum EStepAction
    {
        None,
        MoveLeft,
        MoveRight,
        MoveDown,
        MoveDownUntilBottom,
    }

    private EStepAction _nextAction;

    private readonly TetrisGameController _gameController;
    private readonly TetrisBlockGroupController _blockGroupController;
    private readonly TetrisStageConfig _stageConfig;

    public float InputHoldTime { get { return _stageConfig.InputHoldTime; } }

    public TetrisInputController(TetrisGameController gameController, TetrisBlockGroupController blockGroupController, TetrisStageConfig stageConfig)
    {
        _gameController = gameController;
        _blockGroupController = blockGroupController;
        _stageConfig = stageConfig;
    }

    public void MoveLeft()
    {
        _nextAction = EStepAction.MoveLeft;
    }

    public void MoveRight()
    {
        _nextAction = EStepAction.MoveRight;
    }

    public void MoveDown()
    {
        _nextAction = EStepAction.MoveDown;
    }

    public void MoveDownUntilBottom()
    {
        _nextAction = EStepAction.MoveDownUntilBottom;
    }

    public void RotateClockwise()
    {
        _blockGroupController.RotateClockwise();
    }

    public void RotateCounterclockwise()
    {
        _blockGroupController.RotateCounterclockwise();
    }

    public void Update()
    {
        switch (_nextAction)
        {
            case EStepAction.MoveLeft:
                _blockGroupController.MoveLeft();
                break;

            case EStepAction.MoveRight:
                _blockGroupController.MoveRight();
                break;

            case EStepAction.MoveDown:
                _blockGroupController.MoveDown();
                break;

            case EStepAction.MoveDownUntilBottom:
                _blockGroupController.MoveDownUntilBottom();
                break;
        }
        Reset();
    }

    public void Reset()
    {
        _nextAction = EStepAction.None;
    }

    public void RestartGame()
    {
        _gameController.Restart();
    }
}