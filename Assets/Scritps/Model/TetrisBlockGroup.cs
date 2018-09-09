using UnityEngine;

[CreateAssetMenu]
public class TetrisBlockGroup : ScriptableObject
{
    public enum EBlockType
    {
        O,
        I,
        J,
        L,
        S,
        Z,
        T,
    }
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
            block.BlockType = Pivot.BlockType;
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