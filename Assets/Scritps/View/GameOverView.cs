using UnityEngine;

public class GameOverView : AbstractView
{
    public GameObject Panel;

    private void Update()
    {
        Panel.SetActive(GameController.IsGameOver);
    }
}
