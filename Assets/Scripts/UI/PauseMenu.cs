using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    public static bool isGamePaused = false;
    public GameObject gameMenu;
    public AudioSource audioSource;
    
    void Start()
    {
        gameManager = GameManager.instance;
        audioSource = GetComponent<AudioSource>();
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
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public void Pause()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    public void MainMenu()
    {
        gameManager.MainMenu();
    }
}
