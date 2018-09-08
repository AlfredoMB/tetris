using UnityEngine;

public class InputView : MonoBehaviour
{
    private TetrisBlockGroupController _blockGroupController;

    public void SetTetrisBlockGroupController(TetrisBlockGroupController boardController)
    {
        _blockGroupController = boardController;
    }

    private void Update()
    {
        if (_blockGroupController == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _blockGroupController.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _blockGroupController.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _blockGroupController.MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _blockGroupController.RotateClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            _blockGroupController.RotateCounterclockwise();
        }
    }
}
