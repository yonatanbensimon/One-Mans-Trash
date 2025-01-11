using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int points = 10;

    public int Points
    {
        get { return points; }
        private set { points = value; }
    }
}