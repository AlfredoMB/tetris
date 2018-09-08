using System.Text;

public class TetrisBoard
{
    public TetrisBlock[,] TetrisBlocks;

    public TetrisBoard(int x, int y)
    {
        TetrisBlocks = new TetrisBlock[x, y];
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        int x = TetrisBlocks.GetLength(0);
        int y = TetrisBlocks.GetLength(1);

        for (int i = y-1; i >= 0; i--)
        {
            for (int j = 0; j < x; j++)
            {
                sb.Append(TetrisBlocks[j, i] != null ? 1 : 0);
            }

            if (i > 0)
            {
                sb.AppendLine();
            }
        }
        return sb.ToString();
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
}