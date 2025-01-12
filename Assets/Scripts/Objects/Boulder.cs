using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Rigidbody rb;
    private Runner runner;
    private GameManager gameManager;

    public float boulderSpeed;
    private float runnerSpeed;

    [SerializeField] private float baseSpeed = 25;
    [SerializeField] private float difficultyLevel = 1f;
    [SerializeField] private float acceleration;

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
        rb.MovePosition(transform.position + transform.forward * baseSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Runner runner))
        {
            gameManager.EndGame();
        }
    }

}
