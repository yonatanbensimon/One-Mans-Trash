using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogWarning("There should only be one GameManager");
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void MainMenu()
    {
        //Once MainMenu is implemented
        print("Main Menu");
    }
}
