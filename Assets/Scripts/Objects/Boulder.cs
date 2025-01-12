using System.Collections;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody rb;
    private Runner runner;
    private GameManager gameManager;

    private float boulderSpeed;

    public float BoulderSpeed
    {
        get => boulderSpeed;
        private set => boulderSpeed = value;
    }

    [SerializeField] private float baseSpeed = 25;
    [SerializeField] private float difficultyLevel = 1f;
    [SerializeField] private float acceleration;
    [SerializeField] private float shakeThreshold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        runner = FindAnyObjectByType<Runner>();
        boulderSpeed = baseSpeed;

        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        boulderSpeed = Mathf.Min((boulderSpeed + difficultyLevel * acceleration * Time.fixedDeltaTime), runner.maxSpeed);
        rb.MovePosition(transform.position + transform.forward * boulderSpeed * Time.fixedDeltaTime);
        if (Camera.main.TryGetComponent(out CameraShake cs))
        {
            if (Vector3.Distance(transform.position, runner.transform.position) < shakeThreshold * boulderSpeed + 0.5 * shakeThreshold * shakeThreshold * acceleration)
            {
                cs.TriggerShake(float.PositiveInfinity, 0.2f);
            }
            else
            {
                cs.StopAllShake();
            }
        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Runner runner))
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        runner.DieEvent();
        var sfx = FindAnyObjectByType<SoundFX>();
        float lag = 0;
        if (sfx != null && sfx.death != null && sfx.death.clip != null) 
        {
            lag = sfx.death.clip.length;
        }
        yield return new WaitForSeconds(lag);
        gameManager.EndGame();
    }
}
