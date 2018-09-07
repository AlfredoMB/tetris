using System;

public class TetrisBoardController
{
    private TetrisBoard _board;

    public TetrisBoardController(TetrisBoard board)
    {
        _board = board;
    }

    public bool MoveBlockGroup(TetrisBlockGroup blockGroup, int x, int y)
    {
        // if there are positions available for all,
        if (!IsPositionAvailableForBlock(blockGroup.Pivot, x, y, blockGroup))
        {
            return false;
        }
        foreach (TetrisBlock block in blockGroup.RelativeBlocks)
        {
            if (!IsPositionAvailableForBlock(block, x, y, blockGroup))
            {
                return false;
            }
        }

        // move
        MoveBlock(blockGroup.Pivot, x, y);
        foreach (TetrisBlock block in blockGroup.RelativeBlocks)
        {
            MoveBlock(block, x, y);
        }

        return true;
    }

    public void MoveBlock(TetrisBlock block, int x, int y)
    {
        if (block.PositionX >= 0 && block.PositionY >= 0 && _board.TetrisBlocks[block.PositionX, block.PositionY] == block)
        {
            _board.TetrisBlocks[block.PositionX, block.PositionY] = null;
        }
        block.PositionX += x;
        block.PositionY += y;
        _board.TetrisBlocks[block.PositionX, block.PositionY] = block;
    }

    public bool IsPositionAvailableForBlock(TetrisBlock block, int x, int y, TetrisBlockGroup blockGroup)
    {
        int newX = block.PositionX + x;
        int newY = block.PositionY + y;

        return IsPositionAvailable(newX, newY, blockGroup);
    }

    public bool IsPositionAvailable(int newX, int newY, TetrisBlockGroup blockGroup)
    {
        if (newX < 0 || newX >= _board.TetrisBlocks.GetLength(0) || newY < 0)
        {
            return false;
        }

        var currentBlock = _board.TetrisBlocks[newX, newY];
        return currentBlock == null || blockGroup.Contains(currentBlock);
    }

    public void Rotate(TetrisBlockGroup blockGroup, bool counterClockWise = false)
    {
        int centerX = blockGroup.Pivot.PositionX;
        int centerY = blockGroup.Pivot.PositionY;
        var blocks = blockGroup.RelativeBlocks;

        int newX, newY;
        TetrisBlock block;
        for (int i = 0; i < blocks.Length; i++)
        {
            block = blocks[i];
            newX = block.PositionY - centerY;
            newY = block.PositionX - centerX;
            if (counterClockWise)
            {
                block.PositionX = centerX - newX;
                block.PositionY = centerY + newY;
            }
            else
            {
                block.PositionX = centerX + newX;
                block.PositionY = centerY - newY;
            }
        }
    }
}