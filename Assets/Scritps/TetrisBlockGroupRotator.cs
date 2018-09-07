using System;
using UnityEngine;

public class TetrisBlockGroupRotator : MonoBehaviour
{
    public TetrisBlockGroup BlockGroup;

    public bool CounterClockWise;

    private void OnEnable()
    {
        Rotate(BlockGroup, CounterClockWise);
    }

    private void Rotate(TetrisBlockGroup blockGroup, bool counterClockWise = false)
    {
        int centerX = blockGroup.Pivot.PositionX;
        int centerY = blockGroup.Pivot.PositionY;
        var blocks = blockGroup.Blocks;

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