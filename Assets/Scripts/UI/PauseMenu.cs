using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    public static bool isGamePaused = false;
    public GameObject gameMenu;

    
    void Start()
    {
        gameManager = GameManager.instance;
    }

    public bool Paused
    {
        get => isGamePaused;
    }

    public void Resume()
    {
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void MainMenu()
    {
        gameManager.MainMenu();
    }
}
