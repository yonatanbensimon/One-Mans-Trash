using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                _instance = gameObject.AddComponent<GameManager>();
                DontDestroyOnLoad(gameObject);
            }
            return _instance;
        }
        private set => _instance = value;
    }

    private int _points;

    public event Action<int> OnScoreUpdated;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("There should only be one GameManager");
            Destroy(this);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
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
    public void Restart()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void MainMenu()
    {
        //Once MainMenu is implemented
        print("Main Menu");
    }

    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            OnScoreUpdated.Invoke(value);
        }
    }
}
