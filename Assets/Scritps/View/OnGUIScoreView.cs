using UnityEngine;

public class OnGUIScoreView : AbstractView
{
    private GUIStyle _style;
    private Rect _rect;

    private void Awake()
    {
        _rect = new Rect(100, 0, 100, 100);
    }

    private void OnGUI()
    {
        if (GameController.Score == null)
        {
            return;
        }

        GUI.Label(_rect, "Score: " + GameController.Score.ToString());
    }
}
