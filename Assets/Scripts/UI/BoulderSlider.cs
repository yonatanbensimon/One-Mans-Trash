using UnityEngine;
using UnityEngine.UI;

public class BoulderSlider : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] Image playerImage;
    [SerializeField] float scrollSpeed = 1.0f;
    [SerializeField] Material mat;
    [SerializeField] Vector2 offset;
    [SerializeField] float smoothingFactor = 15f;
    Boulder boulder;
    Vector2 targetPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundImage != null)
        {
            backgroundImage.material = mat;
        }

        boulder = FindAnyObjectByType<Boulder>();
    }

    // Update is called once per frame
    void Update()
    {
        offset.x += boulder.boulderSpeed * scrollSpeed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);
        playerImage.rectTransform.localPosition = Vector2.MoveTowards(playerImage.rectTransform.localPosition, targetPos, boulder.boulderSpeed * smoothingFactor * Time.deltaTime);
    }

    public void ChangePlayerOffset(float offset)
    {
        offset = Mathf.Clamp(offset, -50f, 50f);
        targetPos = new Vector2(offset, playerImage.rectTransform.localPosition.y);
    }
}
