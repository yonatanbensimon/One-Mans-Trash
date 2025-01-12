using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private GameObject deathParticles;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Runner runner))
        {
            runner.HitObstacleEvent();
            runner.CancelAllSpeedUps();
            runner.Speed *= slowAmount;
            Die();
        }
    }

    private void Die()
    {
        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, transform.rotation);
        }
        Destroy(this);
    }
}
