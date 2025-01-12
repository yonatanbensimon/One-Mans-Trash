using UnityEngine;

public class CursedCollectible : Collectable
{
    private void Start()
    {
        Points = Mathf.Min(Points, -Points);
    }
    public override void OnCollection()
    {
        if (Camera.main.TryGetComponent(out CameraShake cs))
        {
            cs.TriggerShake();
        }
    }
}
