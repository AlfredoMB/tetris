using UnityEngine;

namespace AlfredoMB.Tetris.Models
{
    [CreateAssetMenu]
    public class TetrisStageConfig : ScriptableObject
    {
        public int BoardSizeX;
        public int BoardSizeY;

        public float UpdateStepInterval;
        public int Gravity;

        public int LineConsumptionScoreValue;
        public float InputHoldTime;

        public bool EnableBlocksWithoutNeighborsBelowToFall;

        public TetrisBlockGroup[] AvailableBlockGroups;
    }
}
