using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Load Scene
public class MainMenu : MonoBehaviour
{
    // Load Scene
    public void Play() {
        SceneManager.LoadScene("MainLevel");
    }

    // Quit Game
    public void Quit() {
        Application.Quit();
        Debug.Log("Player has Quit the Game!");
    }
}


