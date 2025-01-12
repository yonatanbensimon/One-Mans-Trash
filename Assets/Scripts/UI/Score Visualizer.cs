using TMPro;
using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Start()
    {
        if (scoreText == null)
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }
        if (scoreText == null)
        {
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        GameManager.instance.OnScoreUpdated += UpdateScore;
        UpdateScore(GameManager.instance.Points);
    }

    private void OnDisable()
    {
        GameManager.instance.OnScoreUpdated -= UpdateScore;
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + GameManager.instance.HighScore;
        }
    }
}
