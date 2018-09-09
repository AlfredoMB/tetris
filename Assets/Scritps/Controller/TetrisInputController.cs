using System;
/// <summary>
/// As by description: 
/// "- Match the controls to that of Tetris, click/holding down the Left/Right keys should only move the block once per update step
/// - Clicking the Down key should shift the block down once per update step, holding down the key should speed up the downward movement of the block
/// - Use [F] and [G] keys to rotate blocks by clockwise and counterclockwise orientation (match exactly with Tetris)"
/// </summary>
public class TetrisInputController
{
    private readonly TetrisGameController _gameController;
    private readonly TetrisBlockGroupController _blockGroupController;
    private readonly TetrisStageConfig _stageConfig;
    private bool _canInput;

    public float InputHoldTime { get { return _stageConfig.InputHoldTime; } }

    public TetrisInputController(TetrisGameController gameController, TetrisBlockGroupController blockGroupController, TetrisStageConfig stageConfig)
    {
        _gameController = gameController;
        _blockGroupController = blockGroupController;
        _stageConfig = stageConfig;
    }

    public void MoveLeft()
    {
        if (!_canInput)
        {
            return;
        }
        _canInput = false;
        _blockGroupController.MoveLeft();
    }

    public void MoveRight()
    {
        if (!_canInput)
        {
            return;
        }
        _canInput = false;
        _blockGroupController.MoveRight();
    }

    public void MoveDown()
    {
        if (!_canInput)
        {
            return;
        }
        _canInput = false;
        _blockGroupController.MoveDown();
    }

    public void MoveDownUntilBottom()
    {
        if (!_canInput)
        {
            return;
        }
        _canInput = false;
        _blockGroupController.MoveDownUntilBottom();
    }

    public void RotateClockwise()
    {
        if (!_canInput)
        {
            return;
        }
        _canInput = false;
        _blockGroupController.RotateClockwise();
    }

    public void RotateCounterclockwise()
    {
        if (!_canInput)
        {
            return;
        }
        _canInput = false;
        _blockGroupController.RotateCounterclockwise();
    }

    public void Reload()
    {
        _canInput = true;
    }

    public void RestartGame()
    {
        _gameController.Restart();
    }
}