using UnityEngine;

[CreateAssetMenu]
public class TetrisStageConfig : ScriptableObject
{
    public int BoardSizeX;
    public int BoardSizeY;

    public float GravityUpdateInterval;
    public int Gravity;

    public int LineConsumptionScoreValue;
}