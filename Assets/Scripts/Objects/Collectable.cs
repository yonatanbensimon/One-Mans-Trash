using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int Points
    {
        get;
        private set;
    }
    
    private void Start()
    {
        Points = 10;
    }
}
