using UnityEditor;

public class OnGUIScoreView : AbstractScoreView
{
    private TetrisScore _score;

    public override void SetScore(TetrisScore score)
    {
        _score = score;
    }

    private void OnGUI()
    {
        if (_score == null)
        {
            return;
        }

        EditorGUILayout.TextArea(_score.ToString());
    }
}
