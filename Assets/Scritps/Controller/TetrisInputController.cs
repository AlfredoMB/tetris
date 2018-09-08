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
    private readonly TetrisBlockGroupController _blockGroupController;

    public TetrisInputController(TetrisBlockGroupController blockGroupController)
    {
        _blockGroupController = blockGroupController;
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
        _nextAction = EStepAction.None;
    }
}