using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool shake = false;
    private Vector3 originalPosition; // To store the camera's initial position

    void Awake()
    {
        // Save the camera's initial position
        originalPosition = transform.localPosition;
    }

    public void TriggerShake(float duration = 0.5f, float magnitude = 0.2f)
    {
        // Start the shake coroutine
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Generate a random offset
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset to the camera's position
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Reset the camera to its original position
        transform.localPosition = originalPosition;
    }

    private void Update()
    {
        if (shake)
        {
            shake = false;
            TriggerShake();
        }
    }
}
