using UnityEditor;

public class OnGUIScoreView : AbstractView
{
    private void OnGUI()
    {
        if (GameController.Score == null)
        {
            return;
        }

        EditorGUILayout.TextArea(GameController.Score.ToString());
    }
}
