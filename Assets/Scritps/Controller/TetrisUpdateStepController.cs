using UnityEngine;

internal class TetrisUpdateStepController
{
    private float _updateStepInterval;
    private float _nextUpdate;

    public TetrisUpdateStepController(float updateStepInterval)
    {
        _updateStepInterval = updateStepInterval;
        _nextUpdate = Time.time;
    }

    public bool ShouldUpdate()
    {
        if (Time.time < _nextUpdate)
        {
            return false;
        }
        _nextUpdate += _updateStepInterval;
        return true;
    }
}