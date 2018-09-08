using UnityEngine;

public class InputView : MonoBehaviour
{
    private TetrisInputController _inputController;
    private float _downHoldTime;

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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _inputController.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _inputController.MoveRight();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _downHoldTime += Time.deltaTime;
            if (_downHoldTime < _inputController.InputHoldTime)
            {
                _inputController.MoveDown();
            }
            else
            {
                _inputController.MoveDownUntilBottom();
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _downHoldTime = 0;
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
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            _inputController.RestartGame();
        }
    }
}
