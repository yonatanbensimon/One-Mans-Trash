using UnityEngine;

public class ExitScene : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Restart()
    {
        gameManager.Restart();
    }

    public void MainMenu()
    {
        gameManager.MainMenu();
    }
}
