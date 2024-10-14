using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public EnemyStatsSO enemyStats;

    private Transform target; // The target to move towards
    private float speed = 5f; // Movement speed

    private Rigidbody rb;

    void Start()
    {
        speed = enemyStats.movementSpeed;
        target = GameObject.FindWithTag("Planet").transform;

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Rotate the object to face the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(targetRotation);

            // Move the object towards the target at a constant speed
            Vector3 move = direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }
}
