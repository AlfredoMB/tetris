using UnityEngine;

public class TetrisBlockGroupSpawner
{
    private TetrisBoardController _boardController;
    private readonly int _spawnX;
    private readonly int _spawnY;

    public TetrisBlockGroupSpawner(TetrisBoard board, TetrisBoardController boardController)
    {
        _spawnX = board.TetrisBlocks.GetLength(0) / 2;
        _spawnY = board.TetrisBlocks.GetLength(1);
        _boardController = boardController;
    }

    public bool Spawn(TetrisBlockGroup blockGroup)
    {
        var instance = Object.Instantiate(blockGroup);
        instance.Initialize();
        _boardController.SetCurrentBlockGroup(instance);

        if (_boardController.MoveCurrentBlockGroup(_spawnX, _spawnY - 1))
        {
            return true;
        }

        if (_boardController.MoveCurrentBlockGroup(_spawnX, _spawnY - 2))
        {
            return true;
        }
        return false;
    }
}