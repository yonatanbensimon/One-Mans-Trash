using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [SerializeField] private AudioSource obstacles;
    [SerializeField] private AudioSource collectibles;
    [SerializeField] public AudioSource death;

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

    private void OnCollectTreasure()
    {
        if (collectibles != null)
        {
            collectibles.PlayOneShot(collectibles.clip);
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
