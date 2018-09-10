namespace AlfredoMB.Tetris.Models
{
    public class TetrisScore
    {
        public int Score;
        public int TotalLinesConsumed;

        public override string ToString()
        {
            return Score.ToString();
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}
