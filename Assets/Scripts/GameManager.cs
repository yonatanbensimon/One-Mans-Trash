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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else if (_instance == this)
        {
        }
        else
        {
            Debug.LogWarning("There should only be one GameManager");
            Destroy(this);
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
        Points = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            OnScoreUpdated?.Invoke(value);
        }
    }
}
