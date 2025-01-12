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

    public int HighScore { get; private set; }

    private int _points;

    private bool paragraphViewed = false;

    public event Action<int> OnScoreUpdated;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else if (_instance != this)
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

    public void Continue()
    {
        SceneManager.LoadScene("MainLevel");
        if (Camera.main.TryGetComponent(out CameraShake cs))
        {
            cs.TriggerShake(3f);
        }
    }

    public void StartGame()
    {
        if (!paragraphViewed)
        {
            SceneManager.LoadScene("IntroParagraph");
            paragraphViewed = true;
        } else
        {
            SceneManager.LoadScene("MainLevel");
            if (Camera.main.TryGetComponent(out CameraShake cs))
            {
                cs.TriggerShake(3f);
            }
        }
    }

    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            if (_points >= HighScore) 
            { 
                HighScore = _points;
            }
            OnScoreUpdated?.Invoke(value);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
