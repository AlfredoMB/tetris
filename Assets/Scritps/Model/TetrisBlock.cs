using System;

namespace AlfredoMB.Tetris.Models
{
    [Serializable]
    public class TetrisBlock
    {
        public TetrisBlockGroup.EBlockType BlockType;

        public int RelativePositionX;
        public int RelativePositionY;

        public int PositionX;
        public int PositionY;
    }
}
