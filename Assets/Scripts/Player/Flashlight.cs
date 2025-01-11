using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rotationSpeed = 5f;

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}