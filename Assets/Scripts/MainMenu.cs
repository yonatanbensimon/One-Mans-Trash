using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Load Scene
public class MainMenu : MonoBehaviour
{

    private PauseMenu pm;
    private GameManager gameManager;

    public void Start()
    {
        pm = FindAnyObjectByType<PauseMenu>();
        gameManager = GameManager.instance;

    }
    // Load Scene
    public void Play() {
        gameManager.StartGame();
    }

    // Quit Game
    public void Quit() {
        Application.Quit();
        Debug.Log("Player has Quit the Game!");
    }
}


