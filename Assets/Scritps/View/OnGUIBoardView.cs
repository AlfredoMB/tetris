using UnityEditor;

public class OnGUIBoardView : AbstractBoardView
{
    private TetrisBoard _board;

    public override void SetBoard(TetrisBoard board)
    {
        _board = board;
    }

    private void OnGUI()
    {
        if (_board == null)
        {
            return;
        }

        EditorGUILayout.TextArea(_board.ToString());
    }
}
