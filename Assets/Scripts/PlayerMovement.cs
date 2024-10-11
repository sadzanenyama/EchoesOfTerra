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
    public float dashMultiplier = 2f;
    public float dashDuration;
    public float dashHeat = 10f;

    private float originalAcceleration;
    private float originalMaxSpeed;

    private WeaponManager weaponManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag; // Set drag for deceleration
        rb.freezeRotation = true;

        originalAcceleration = acceleration;
        originalMaxSpeed = maxSpeed;

        weaponManager = GetComponent<WeaponManager>();
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

        if(Input.GetKeyDown(KeyCode.Space) && !weaponManager.overheated)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        // Temporarily boost speed and acceleration
        maxSpeed *= dashMultiplier;
        acceleration *= dashMultiplier;

        weaponManager.AddHeat(dashHeat);

        yield return new WaitForSeconds(dashDuration);

        // Reset speed and acceleration
        maxSpeed = originalMaxSpeed;
        acceleration = originalAcceleration;
    }
}
