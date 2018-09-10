using AlfredoMB.Tetris.Models;

namespace AlfredoMB.Tetris.Controllers
{
    public class TetrisScoreController
    {
        private readonly TetrisStageConfig _stageConfig;
        private readonly TetrisScore _score;

        public TetrisScoreController(TetrisStageConfig stageConfig, TetrisScore score)
        {
            _stageConfig = stageConfig;
            _score = score;
        }

        public void AddLinesConsumed(int linesConsumed)
        {
            _score.TotalLinesConsumed += linesConsumed;
            _score.Score = _score.TotalLinesConsumed * _stageConfig.LineConsumptionScoreValue;
        }

        public void Reset()
        {
            _score.Reset();
        }
    }
}
