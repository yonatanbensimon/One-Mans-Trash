using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class Runner : MonoBehaviour
{
    private Rigidbody rb;
    private PauseMenu pm;

    [SerializeField] float baseSpeed = 1.0f;
    private float speed = 1.0f;
    [SerializeField] private Vector2 speedRange = new Vector2(0.5f, 2.0f);
    [SerializeField] private float deceleration = 1f;

    [SerializeField] private float sizeOfPlane;
    [SerializeField] private int numOfLanes;
    [SerializeField] private float speedOfX;
    [SerializeField] private float posRightPlane;
    [SerializeField] BoulderSlider speedSlider;
    [SerializeField] Boulder boulder;
    [SerializeField] float clampDistanceBetweenBoulderAndPlayer;

    private float lineWidth;

    private int currentLane;

    private Vector3 newPos;
    private bool isMoving = false;

    public event Action<float> OnSpeedChanged;
    public event Action<bool> OnCollectTreasure;
    public event Action OnHitObstacle;
    public event Action OnDeath;

    [SerializeField] MeshRenderer meshRenderer;

    private List<Coroutine> speedUpCoroutines;

    public float Speed
    {
        get => speed;
        set
        {
            speed = Math.Min(Math.Max(speedRange.x, value), speedRange.y);
            OnSpeedChanged?.Invoke(speed);
        }
    }

    public float maxSpeed
    {
        get => speedRange.y;
    }

    private void Start()
    {
        speed = baseSpeed;
        rb = GetComponent<Rigidbody>();
        if (meshRenderer == null)
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        // We want our character to be in the middle of the second lane when starting the game
        currentLane = 1; //Lanes are 0-indexed
        lineWidth = sizeOfPlane / numOfLanes;

        pm = FindAnyObjectByType<PauseMenu>();

        UpdateLane();
        speedUpCoroutines = new List<Coroutine>();
        speedSlider = FindAnyObjectByType<BoulderSlider>();
        boulder = FindAnyObjectByType<Boulder>();
    }

    private void FixedUpdate()
    {
        Speed -= deceleration * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + transform.forward * (speed * Time.fixedDeltaTime));
        float playerPos = -100f + 200f * Vector3.Distance(transform.position, boulder.transform.position) / clampDistanceBetweenBoulderAndPlayer;
        speedSlider.ChangePlayerOffset(playerPos);
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

    void OnPause()
    {
        if (pm.Paused)
        {
            pm.Resume();
        } else
        {
            pm.Pause();
        }
    }

    public void AddSpeedUpCoroutine(Coroutine cr)
    {
        speedUpCoroutines.Add(cr);
    }

    public void CancelAllSpeedUps()
    {
        foreach (var cr in speedUpCoroutines)
        {
            if (cr != null)
            {
                StopCoroutine(cr);
            }
        }
        speedUpCoroutines.Clear();
    }

    public void HitObstacleEvent()
    {
        OnHitObstacle?.Invoke();
    }

    public void CollectTreasureEvent(bool good)
    {
        OnCollectTreasure?.Invoke(good);
    }

    public void DieEvent()
    {
        meshRenderer.enabled = false;
        OnDeath?.Invoke();
    }
}
