using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition; // To store the camera's initial position
    private List<Coroutine> infiniteShakes;

    void Awake()
    {
        // Save the camera's initial position
        originalPosition = transform.localPosition;
        infiniteShakes = new List<Coroutine>();
    }

    public void TriggerShake(float duration = 0.5f, float magnitude = 0.2f)
    {
        // Start the shake coroutine
        var cr = StartCoroutine(Shake(duration, magnitude));
        if (duration >= float.PositiveInfinity)
        {
            infiniteShakes.Add(cr);
        }
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

    public void StopAllShake()
    {
        foreach (var cr in infiniteShakes)
        {
            StopCoroutine(cr);
        }
    }
}
