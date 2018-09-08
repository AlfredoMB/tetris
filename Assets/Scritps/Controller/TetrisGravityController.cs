public class TetrisGravityController
{
    private TetrisStageConfig _stageConfig;
    private TetrisBoardController _boardController;

    public bool HitBottom { get; private set; }

    public TetrisGravityController(TetrisStageConfig stageConfig, TetrisBoardController boardController)
    {
        _stageConfig = stageConfig;
        _boardController = boardController;
    }

    public void Update()
    {
        HitBottom = false;
        HitBottom = !_boardController.MoveCurrentBlockGroup(0, _stageConfig.Gravity);
    }
}