using System;

public class TetrisScoreController
{
    private readonly int _lineConsumptionScoreValue;
    private readonly TetrisScore _score;

    public TetrisScoreController(int lineConsumptionScoreValue, TetrisScore score)
    {
        _lineConsumptionScoreValue = lineConsumptionScoreValue;
        _score = score;
    }

    public void AddScore(int linesConsumed)
    {
        _score.Score += linesConsumed * _lineConsumptionScoreValue;
    }
}