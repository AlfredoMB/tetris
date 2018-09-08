using System;

public class TetrisBlockGroupController
{
    private TetrisBoardController _boardController;

    public TetrisBlockGroupController(TetrisBoardController boardController)
    {
        _boardController = boardController;
    }

    public void MoveLeft()
    {
        _boardController.MoveCurrentBlockGroup(-1, 0);
    }

    public void MoveRight()
    {
        _boardController.MoveCurrentBlockGroup(1, 0);
    }

    public void MoveDown()
    {
        _boardController.MoveCurrentBlockGroup(0, -1);
    }

    public void RotateClockwise()
    {
        _boardController.RotateCurrentBlockGroup(true);
    }

    public void RotateCounterclockwise()
    {
        _boardController.RotateCurrentBlockGroup(false);
    }

    public void MoveDownUntilBottom()
    {
        while(_boardController.MoveCurrentBlockGroup(0, -1))
        { }
    }
}