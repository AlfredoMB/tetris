using UnityEngine.UI;

namespace AlfredoMB.Tetris.Views
{
    public class UnityScoreView : AbstractView
    {
        public Text Text;

        private void Update()
        {
            if (GameController.Score == null)
            {
                return;
            }

            Text.text = GameController.Score.Score.ToString();
        }
    }
}
