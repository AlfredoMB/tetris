using UnityEditor;
using UnityEngine;

public class TetrisBlockGroupMover : MonoBehaviour
{
    public TetrisBlockGroup BlockGroup;
    public TetrisBoard Board;
    public TetrisBoardController BoardController;

    public int x;
    public int y;

    private void Awake()
    {
        Board = new TetrisBoard(10, 22);
        BoardController = new TetrisBoardController(Board);
        BlockGroup.Initialize();
        Debug.Log(Board);
    }

    private void OnEnable()
    {
        Debug.Log(BoardController.MoveBlockGroup(BlockGroup, x, y) + "\n" + Board.ToString());
    }

    private void OnGUI()
    {
        EditorGUILayout.TextArea(Board.ToString());
    }
}