using System;
using System.Text;

public class TetrisBoard
{
    public TetrisBlock[,] TetrisBlocks;

    public event Action OnBoardChanged;

    private StringBuilder _sb;

    public TetrisBoard(int x, int y)
    {
        TetrisBlocks = new TetrisBlock[x, y];
        _sb = new StringBuilder();
    }

    public void DispatchBoardChanged()
    {
        if (OnBoardChanged != null)
        {
            OnBoardChanged();
        }
    }

    public override string ToString()
    {
        // force clear
        _sb.Length = 0;
        _sb.Capacity = 0;

        int x = TetrisBlocks.GetLength(0);
        int y = TetrisBlocks.GetLength(1);

        for (int i = y-1; i >= 0; i--)
        {
            for (int j = 0; j < x; j++)
            {
                _sb.Append(TetrisBlocks[j, i] != null ? 1 : 0);
            }

            if (i > 0)
            {
                _sb.AppendLine();
            }
        }
        return _sb.ToString();
    }

    public void CopyFrom(TetrisBoard board)
    {
        int x = board.TetrisBlocks.GetLength(0);
        int y = board.TetrisBlocks.GetLength(1);

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                TetrisBlocks[j, i] = board.TetrisBlocks[j, i];
            }
        }
    }

    public void Reset()
    {
        int x = TetrisBlocks.GetLength(0);
        int y = TetrisBlocks.GetLength(1);

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                // TODO: add pool recycle
                TetrisBlocks[j, i] = null;
            }
        }
    }
}