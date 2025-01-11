using System;
using UnityEngine;

public class Runner : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float baseSpeed = 1.0f;
    private float speed = 1.0f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.5f, 2.0f);
    [SerializeField] private float deceleration = 1f;
    
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
        speed = baseSpeed;
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.forward;
    }

    private void FixedUpdate()
    {
        Speed -= deceleration * Time.deltaTime;
    }
}
