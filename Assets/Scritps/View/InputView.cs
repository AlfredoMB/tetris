using UnityEngine;

public class InputView : AbstractView
{
    private float _holdTime;

    private void Update()
    {
        if (GameController.InputController == null)
        {
            return;
        }

        if (Input.anyKey)
        {
            _holdTime += Time.deltaTime;
        }
        else
        {
            _holdTime = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GameController.InputController.MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GameController.InputController.MoveRight();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (_holdTime < GameController.InputController.InputHoldTime)
            {
                GameController.InputController.MoveDown();
            }
            else
            {
                GameController.InputController.MoveDownUntilBottom();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            GameController.InputController.RotateClockwise();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            GameController.InputController.RotateCounterclockwise();
        }
    }
}
