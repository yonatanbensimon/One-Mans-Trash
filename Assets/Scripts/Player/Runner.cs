using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Runner : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float baseSpeed = 1.0f;
    private float speed = 1.0f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.5f, 2.0f);
    [SerializeField] private float deceleration = 1f;

    [SerializeField] private float sizeOfPlane;
    [SerializeField] private int numOfLanes;
    [SerializeField] private float speedOfX;
    [SerializeField] private float posRightPlane;

    private float lineWidth;

    private int currentLane;

    private Vector3 newPos;
    private bool isMoving = false;

    public float Speed
    {
        get => speed;
        set
        {
            speed = Math.Min(Math.Max(speedRange.x, value), speedRange.y);
        }
    }

    private void Start()
    {
        speed = baseSpeed;
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.forward;

        //We want our character to be in the middle of the second lane when starting the game
        currentLane = 1; //Lanes are 0-indexed
        lineWidth = sizeOfPlane / numOfLanes;

        UpdateLane();
    }

    private void FixedUpdate()
    {
        Speed -= deceleration * Time.deltaTime;
        rb.MovePosition(transform.position + transform.forward * (speed * Time.fixedDeltaTime));
    }

    void OnMoveLeft()
    {
        if (currentLane != 0 && !isMoving)
        {
            currentLane--;
            UpdateLane();
        }

    }

    void OnMoveRight()
    {
        if (currentLane != (numOfLanes - 1) && !isMoving)
        {
            currentLane++;
            UpdateLane();
        }
    }

    void UpdateLane()
    {
        float pos = posRightPlane + (lineWidth * currentLane) + (lineWidth / 2);
        newPos = new Vector3(pos, transform.position.y, transform.position.z); //This exclusively changes the x-coord

        if (!isMoving)
        {
            StartCoroutine(MoveToLane());
        }
    }

    private IEnumerator MoveToLane()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, newPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPos, speedOfX * Time.deltaTime);
            yield return null;
        }

        transform.position = newPos;
        isMoving = false;
    }
}
