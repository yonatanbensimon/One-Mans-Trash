using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int points = 1;

    public int Points
    {
        get { return points; }
        protected set { points = value; }
    }

    public virtual void OnCollection()
    {

    }
}