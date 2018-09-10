using UnityEngine;

namespace AlfredoMB.Tetris.Views
{
    public class GameOverView : AbstractView
    {
        public GameObject Panel;

        private void Start()
        {
            GameController.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameController.OnGameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged()
        {
            Panel.SetActive(GameController.IsGameOver);
        }
    }
}
