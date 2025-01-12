using UnityEngine;
using UnityEngine.UI;

public class BoulderSlider : MonoBehaviour
{
    [SerializeField] RectTransform backgroundRect;
    [SerializeField] Image backgroundImage;
    [SerializeField] float backgroundMoveSpeed = 1.0f;
    float width;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        width = 0.5f * backgroundRect.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        backgroundRect.anchoredPosition += backgroundMoveSpeed * Time.deltaTime * Vector2.left;
        if (backgroundRect.anchoredPosition.x <= -width)
        {
            backgroundImage.transform.localPosition = new Vector2(0, backgroundRect.anchoredPosition.y);
        } 
    }
}
