using UnityEngine;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;
    Runner runner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        runner = FindAnyObjectByType<Runner>();
        if (runner != null)
        {
            runner.OnSpeedChanged += OnChangeSpeed;
        }
    }

    private void OnDisable()
    {
        if (runner != null)
        {
            runner.OnSpeedChanged -= OnChangeSpeed;
        }
    }

    public void OnChangeSpeed(float speed)
    {
        speed = 0.4f * speed / runner.maxSpeed + 0.8f;
        speed = Mathf.Clamp(speed, 0.8f, 1.2f);
        audioSource.pitch = speed;
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f / speed);
    }
}
