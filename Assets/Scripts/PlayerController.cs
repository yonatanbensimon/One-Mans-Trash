using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float sizeOfPlane;
    [SerializeField] private int numOfLanes;

    private float lineWidth;
    private Rigidbody rb;

    private int currentLane;

    private Vector3 newPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //We want our character to be in the middle of the second lane when starting the game
        currentLane = 1; //Lanes are 0-indexed
        lineWidth = sizeOfPlane / numOfLanes;
        float initialPos = (lineWidth * currentLane) + (lineWidth / 2);

        transform.position = new Vector3(initialPos, transform.position.y, transform.position.z); //This exclusively changes the x-coord
        newPos = transform.position;
    }

    void OnMoveLeft()
    {
        if (currentLane != 0)
        {
            currentLane--;
            UpdateLane();
        }

    }

    void OnMoveRight()
    {
        if (currentLane != (numOfLanes - 1))
        {
            currentLane++;
            UpdateLane();
        }
    }

    void UpdateLane()
    {
        float pos = (lineWidth * currentLane) + (lineWidth / 2);
        transform.position = new Vector3(pos, transform.position.y, transform.position.z); //This exclusively changes the x-coord
        newPos = transform.position;
    }

}
