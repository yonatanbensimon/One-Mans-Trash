using System;
using UnityEngine;

public class Runner : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1.0f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.5f, 2.0f);
    
    public float Speed
    {
        get => speed;
        set
        {
            speed = Math.Min(Math.Max(speedRange.x, value), speedRange.y);
            rb.linearVelocity = speed * Vector3.forward;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.forward;
    }

    private void Update()
    {
        Speed -= 0.001f;
    }
}
