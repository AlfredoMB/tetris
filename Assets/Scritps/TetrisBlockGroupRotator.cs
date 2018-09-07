using System;
using UnityEngine;

public class TetrisBlockGroupRotator : MonoBehaviour
{
    public TetrisBlockGroup BlockGroupPivot;

    public bool CounterClockWise;

    private void OnEnable()
    {
        //new TetrisBoardController().Rotate(BlockGroupPivot, CounterClockWise);
    }    
}