using AlfredoMB.Tetris.Models;

namespace AlfredoMB.Tetris.Controllers
{
    public class TetrisBoardController
    {
        private TetrisBoard _board;
        private readonly int _maxX;
        private readonly int _maxY;

        private TetrisBlockGroup _currentBlockGroup;

        public TetrisBoardController(TetrisBoard board)
        {
            _board = board;
            _maxX = board.TetrisBlocks.GetLength(0);
            _maxY = board.TetrisBlocks.GetLength(1);
        }

        public void SetCurrentBlockGroup(TetrisBlockGroup blockGroup)
        {
            _currentBlockGroup = blockGroup;
        }

        public bool RotateCurrentBlockGroup(bool clockWise)
        {
            if (_currentBlockGroup == null)
            {
                return false;
            }

            int centerX = _currentBlockGroup.Pivot.PositionX;
            int centerY = _currentBlockGroup.Pivot.PositionY;
            var blocks = _currentBlockGroup.RelativeBlocks;

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
            if (_currentBlockGroup == null)
            {
                return false;
            }

            // if there are positions available for all,
            if (!IsPositionAvailable(_currentBlockGroup.Pivot.PositionX + deltaX, _currentBlockGroup.Pivot.PositionY + deltaY))
            {
                return false;
            }
            foreach (TetrisBlock block in _currentBlockGroup.RelativeBlocks)
            {
                if (!IsPositionAvailable(block.PositionX + deltaX, block.PositionY + deltaY))
                {
                    return false;
                }
            }

            // move all
            SetBlockPosition(_currentBlockGroup.Pivot, _currentBlockGroup.Pivot.PositionX + deltaX, _currentBlockGroup.Pivot.PositionY + deltaY);
            foreach (TetrisBlock block in _currentBlockGroup.RelativeBlocks)
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
            }
            else
            {
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
            _board.DispatchBoardChanged();
        }

        private bool IsPositionAvailable(int newX, int newY)
        {
            if (newX < 0 || newX >= _board.TetrisBlocks.GetLength(0) || newY < 0 || newY >= _board.TetrisBlocks.GetLength(1))
            {
                return false;
            }

            var block = _board.TetrisBlocks[newX, newY];
            return block == null || (_currentBlockGroup != null && _currentBlockGroup.Contains(block));
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

        /// <summary>
        /// As by description:
        /// "- All blocks without neighbors below must fall down into place"
        /// </summary>
        public bool FallBlocksWithoutNeighborsBelowIntoPlace()
        {
            bool didABlockFall = false;
            bool isLineEmpty;
            for (int i = 0; i < _maxY - 1; i++)
            {
                isLineEmpty = true;
                for (int j = 0; j < _maxX; j++)
                {
                    if (_board.TetrisBlocks[j, i] != null)
                    {
                        isLineEmpty = false;
                    }
                    else if (_board.TetrisBlocks[j, i + 1] != null)
                    {
                        SetBlockPosition(_board.TetrisBlocks[j, i + 1], j, i);
                        didABlockFall = true;
                    }
                }

                if (isLineEmpty)
                {
                    break;
                }
            }
            return didABlockFall;
        }

        public void Reset()
        {
            _board.Reset();
            _board.DispatchBoardChanged();
        }
    }
}
