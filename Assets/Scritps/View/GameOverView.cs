using UnityEngine;

public class GameOverView : AbstractView
{
    public GameObject Panel;

    private void Start()
    {
        GameController.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameController.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        Panel.SetActive(GameController.IsGameOver);
    }
}
