using UnityEngine;

public class InputView : MonoBehaviour
{
    private TetrisInputController _inputController;

    public void SetTetrisInputController(TetrisInputController inputController)
    {
        _inputController = inputController;
    }

    private void Update()
    {
        if (_inputController == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _inputController.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _inputController.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _inputController.MoveDown();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _inputController.MoveDownUntilBottom();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            _inputController.RotateClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            _inputController.RotateCounterclockwise();
        }
    }
}
