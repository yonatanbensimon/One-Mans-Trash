using UnityEngine;

public class Runner : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1.0f;
    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            rb.linearVelocity = speed * Vector3.forward;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.forward;
    }
}
