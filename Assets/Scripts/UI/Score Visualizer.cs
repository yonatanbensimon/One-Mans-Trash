using TMPro;
using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

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
    }

    private void OnDisable()
    {
        GameManager.instance.OnScoreUpdated -= UpdateScore;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
