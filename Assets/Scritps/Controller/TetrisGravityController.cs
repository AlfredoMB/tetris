public class TetrisGravityController
{
    private readonly int _gravity;
    private TetrisBoardController _boardController;

    public bool HitBottom { get; private set; }

    public TetrisGravityController(int gravity, TetrisBoardController boardController)
    {
        _gravity = gravity;
        _boardController = boardController;
    }
    
    public void Update()
    {
        HitBottom = false;
        HitBottom = !_boardController.MoveCurrentBlockGroup(0, _gravity);
    }
}