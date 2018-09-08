using UnityEngine;

// Using abstract instead of interface for UnityEditor's referencing:
public abstract class AbstractBoardView : MonoBehaviour
{
    public abstract void SetBoard(TetrisBoard board);
}