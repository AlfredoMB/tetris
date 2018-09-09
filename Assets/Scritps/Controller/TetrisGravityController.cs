public class TetrisGravityController
{
    private TetrisStageConfig _stageConfig;
    private TetrisBoardController _boardController;

    public bool DidCurrentBlockGroupHitBottom { get; private set; }

    public TetrisGravityController(TetrisStageConfig stageConfig, TetrisBoardController boardController)
    {
        _stageConfig = stageConfig;
        _boardController = boardController;
    }

    public void Update()
    {
        DidCurrentBlockGroupHitBottom = !_boardController.MoveCurrentBlockGroup(0, _stageConfig.Gravity);
        if (DidCurrentBlockGroupHitBottom)
        {
            _boardController.SetCurrentBlockGroup(null);
        }
    }
}