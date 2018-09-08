using System;
using System.Collections.Generic;

public class TetrisBoardController
{
    private TetrisBoard _board;
    private int _maxX;
    private int _maxY;

    private TetrisBlockGroup _blockGroup;

    public TetrisBoardController(TetrisBoard board)
    {
        _board = board;
        _maxX = board.TetrisBlocks.GetLength(0);
        _maxY = board.TetrisBlocks.GetLength(1);
    }

    public void SetCurrentBlockGroup(TetrisBlockGroup blockGroup)
    {
        _blockGroup = blockGroup;
    }

    public bool RotateCurrentBlockGroup(bool clockWise)
    {
        int centerX = _blockGroup.Pivot.PositionX;
        int centerY = _blockGroup.Pivot.PositionY;
        var blocks = _blockGroup.RelativeBlocks;

        int deltaX, deltaY, newX, newY;
        TetrisBlock block;
        // if all blocks can move,
        for (int i = 0; i < blocks.Length; i++)
        {
            block = blocks[i];
            deltaX = block.PositionY - centerY;
            deltaY = block.PositionX - centerX;
            if (clockWise)
            {
                newX = centerX + deltaX;
                newY = centerY - deltaY;
            }
            else
            {
                newX = centerX - deltaX;
                newY = centerY + deltaY;
            }

            if (!IsPositionAvailable(newX, newY))
            {
                return false;
            }
        }

        // TODO: authoritative rotate

        // move all blocks
        for (int i = 0; i < blocks.Length; i++)
        {
            block = blocks[i];
            deltaX = block.PositionY - centerY;
            deltaY = block.PositionX - centerX;
            if (clockWise)
            {
                SetBlockPosition(block, centerX + deltaX, centerY - deltaY);
            }
            else
            {
                SetBlockPosition(block, centerX - deltaX, centerY + deltaY);
            }
        }

        return true;
    }

    public bool MoveCurrentBlockGroup(int deltaX, int deltaY)
    {
        // if there are positions available for all,
        if (!IsPositionAvailable(_blockGroup.Pivot.PositionX + deltaX, _blockGroup.Pivot.PositionY + deltaY))
        {
            return false;
        }
        foreach (TetrisBlock block in _blockGroup.RelativeBlocks)
        {
            if (!IsPositionAvailable(block.PositionX + deltaX, block.PositionY + deltaY))
            {
                return false;
            }
        }

        // move all
        SetBlockPosition(_blockGroup.Pivot, _blockGroup.Pivot.PositionX + deltaX, _blockGroup.Pivot.PositionY + deltaY);
        foreach (TetrisBlock block in _blockGroup.RelativeBlocks)
        {
            SetBlockPosition(block, block.PositionX + deltaX, block.PositionY + deltaY);
        }

        return true;
    }

    public void SetBlockPosition(TetrisBlock block, int x, int y)
    {
        // when emptying position
        if (block == null)
        {
            _board.TetrisBlocks[x, y] = null;
            return;
        }

        // remove from old position
        if (block.PositionX >= 0 && block.PositionY >= 0 && _board.TetrisBlocks[block.PositionX, block.PositionY] == block)
        {
            _board.TetrisBlocks[block.PositionX, block.PositionY] = null;
        }

        // put on new position
        block.PositionX = x;
        block.PositionY = y;
        _board.TetrisBlocks[block.PositionX, block.PositionY] = block;
    }

    private bool IsPositionAvailable(int newX, int newY)
    {
        if (newX < 0 || newX >= _board.TetrisBlocks.GetLength(0) || newY < 0 || newY >= _board.TetrisBlocks.GetLength(1))
        {
            return false;
        }

        var currentBlock = _board.TetrisBlocks[newX, newY];
        return currentBlock == null || _blockGroup.Contains(currentBlock);
    }

    public int ConsumeFullLines()
    {
        bool isLineFull;
        int newY = 0;
        for (int i = 0; i < _maxY; i++)
        {
            isLineFull = true;
            for (int j = 0; j < _maxX; j++)
            {
                if (_board.TetrisBlocks[j, i] == null)
                {
                    isLineFull = false;
                }

                if (newY < 0)
                {
                    SetBlockPosition(_board.TetrisBlocks[j, i], j, i + newY);
                }
            }

            if (isLineFull)
            {
                newY--;
            }
        }
        return -newY;
    }
}
