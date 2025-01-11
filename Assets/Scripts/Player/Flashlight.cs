using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private Runner runner;
    [SerializeField] private float pointSpeedRatio = 0.1f;
    
    private RaycastHit hit;

    void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            hit = new RaycastHit();
            targetPoint = transform.position + ray.direction;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    void OnAttack(InputValue value)
    {
        if (hit.collider != null)
        {
            Collectable collectable = hit.transform.GetComponent<Collectable>();
            if (collectable != null)
            {
                int points = collectable.Points;
                runner.Speed += points * pointSpeedRatio;
                hit.transform.gameObject.SetActive(false);
            }
        }
    }
}