using UnityEngine;

public class TetrisGravityController
{
    private int _gravity;
    private float _gravityUpdateInterval;
    private TetrisBoardController _boardController;
    private float _nextUpdate;

    public bool HitBottom { get; private set; }

    public TetrisGravityController(int gravity, float gravityUpdateInterval, TetrisBoardController boardController)
    {
        _gravity = gravity;
        _gravityUpdateInterval = gravityUpdateInterval;
        _boardController = boardController;
    }
    
    public void Update()
    {
        if (Time.time < _nextUpdate)
        {
            HitBottom = false;
            return;
        }
        _nextUpdate = Time.time + _gravityUpdateInterval;

        HitBottom = !_boardController.MoveCurrentBlockGroup(0, _gravity);
    }
}