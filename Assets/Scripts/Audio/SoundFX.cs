using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [SerializeField] private AudioSource obstacles;
    [SerializeField] private AudioSource collectibles;
    [SerializeField] public AudioSource death;
    [SerializeField] private AudioClip cursedObject;
    [SerializeField] private AudioClip goodObject;

    Runner runner;

    private void OnEnable()
    {
        runner = FindAnyObjectByType<Runner>();
        if (runner != null)
        {
            runner.OnHitObstacle += OnHitObstacle;
            runner.OnCollectTreasure += OnCollectTreasure;
            runner.OnDeath += OnDeath;
        }

        goodObject = collectibles.clip;
    }

    private void OnDisable()
    {
        if (runner != null)
        {
            runner.OnHitObstacle -= OnHitObstacle;
            runner.OnCollectTreasure -= OnCollectTreasure;
            runner.OnDeath -= OnDeath;
        }
    }

    private void OnHitObstacle()
    {
        if (obstacles != null)
        {
            obstacles.PlayOneShot(obstacles.clip);
        }
    }

    private void OnCollectTreasure(bool good)
    {
        if (collectibles != null)
        {
            collectibles.PlayOneShot(good ? goodObject : cursedObject);
        }
    }

    private void OnDeath()
    {
        if (death != null)
        {
            death.PlayOneShot(death.clip);
        }
    }
}
