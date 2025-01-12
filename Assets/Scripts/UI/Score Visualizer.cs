using TMPro;
using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
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
