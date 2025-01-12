using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
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

    private void Start()
    {
        Points = 0;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }

<<<<<<< Updated upstream
    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void MainMenu()
    {
        //Once MainMenu is implemented
        print("Main Menu");
=======
    public int Points
    {
        get;
        set;
>>>>>>> Stashed changes
    }
}
