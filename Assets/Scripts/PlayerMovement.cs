using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Attributes")]
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float drag = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag; // Set drag for deceleration
        rb.freezeRotation = true;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized; // Normalize to prevent faster diagonal movement

        rb.AddForce(movement * acceleration, ForceMode.Acceleration);

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Ignore y-axis (gravity)
        if (flatVelocity.magnitude > maxSpeed)
        {
            flatVelocity = flatVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(flatVelocity.x, rb.velocity.y, flatVelocity.z); // Keep y velocity for gravity
        }
    }
}
