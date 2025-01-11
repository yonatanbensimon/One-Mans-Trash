using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sizeOfPlane;
    [SerializeField] private int numOfLanes;
    [SerializeField] private float speed;

    private float lineWidth;

    private int currentLane;

    private Vector3 newPos;
    private bool isMoving = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //We want our character to be in the middle of the second lane when starting the game
        currentLane = 1; //Lanes are 0-indexed
        lineWidth = sizeOfPlane / numOfLanes;

        UpdateLane();
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
        float pos = (lineWidth * currentLane) + (lineWidth / 2);
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
            transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = newPos;
        isMoving = false;
    }
}
