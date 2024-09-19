using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float rotateSpeed;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world position (Player is on the XZ plane, Y is constant)
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // A plane at Y = 0
        float rayDistance;

        // Cast the ray to the plane
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 targetPoint = ray.GetPoint(rayDistance);

            // Calculate direction from the object to the mouse
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0; // Keep the rotation only on the Y-axis

            // Calculate the desired rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rotate the object to face the target smoothly (optional)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }
}
