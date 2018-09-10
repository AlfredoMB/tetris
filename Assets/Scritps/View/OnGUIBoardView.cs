using UnityEditor;

namespace AlfredoMB.Tetris.Views
{
    public class OnGUIBoardView : AbstractView
    {
        private void OnGUI()
        {
            if (GameController.Board == null)
            {
                return;
            }
            EditorGUILayout.TextArea(GameController.Board.ToString());
        }
    }
}
