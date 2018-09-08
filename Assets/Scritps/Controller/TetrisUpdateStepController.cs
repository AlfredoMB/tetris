using System;
using UnityEngine;

public class TetrisUpdateStepController
{
    private readonly TetrisStageConfig _stageConfig;
    private float _nextUpdate;

    public TetrisUpdateStepController(TetrisStageConfig stageConfig)
    {
        _stageConfig = stageConfig;
        _nextUpdate = Time.time;
    }

    public bool ShouldUpdate()
    {
        if (Time.time < _nextUpdate)
        {
            return false;
        }
        _nextUpdate += _stageConfig.UpdateStepInterval;
        return true;
    }

    public void Reset()
    {
        _nextUpdate = Time.time;
    }
}