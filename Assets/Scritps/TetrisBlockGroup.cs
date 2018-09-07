using System;

[Serializable]
public class TetrisBlockGroup
{
    public TetrisBlock Pivot;
    public TetrisBlock[] RelativeBlocks;

    public void Initialize()
    {
        TetrisBlock block;
        for (int i = 0; i < RelativeBlocks.Length; i++)
        {
            block = RelativeBlocks[i];
            block.PositionX = Pivot.PositionX + block.RelativePositionX;
            block.PositionY = Pivot.PositionY + block.RelativePositionY;
        }
    }

    public bool Contains(TetrisBlock block)
    {
        if (Pivot == block)
        {
            return true;
        }

        for (int i = 0; i < RelativeBlocks.Length; i++)
        {
            if (RelativeBlocks[i] == block)
            {
                return true;
            }
        }
        return false;
    }
}