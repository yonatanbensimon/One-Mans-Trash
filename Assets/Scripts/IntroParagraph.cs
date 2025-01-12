using UnityEngine;

public class IntroParagraph : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnContinue()
    {
        gameManager.Continue();
    }
}
