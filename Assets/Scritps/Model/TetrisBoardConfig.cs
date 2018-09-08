using UnityEngine;

[CreateAssetMenu]
public class TetrisBoardConfig : ScriptableObject
{
    public int BoardSizeX;
    public int BoardSizeY;

    public float GravityUpdateInterval;
    public int Gravity;
}