using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private WeaponSO weaponStats;
    private Transform playerTrans;
    private Rigidbody playerRB;

    // Start is called before the first frame update
    void Start()
    {
        weaponStats = GetComponent<WeaponManager>().weaponStats;
        playerTrans = GameObject.FindWithTag("Player").transform;
        playerRB = playerTrans.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVelocity = playerRB.velocity;

        Vector3 leadPosition = CalculateLeadPosition(playerTrans.position, playerVelocity, weaponStats.projectileSpeed);
        Vector3 directionToLead = leadPosition - transform.position;
        directionToLead.y = 0; // Maintain enemy's current y-level

        // Rotate enemy to look at the lead position
        Quaternion lookRotation = Quaternion.LookRotation(directionToLead);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    Vector3 CalculateLeadPosition(Vector3 targetPosition, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector3 direction = targetPosition - transform.position;
        float distance = direction.magnitude;
        float targetSpeed = targetVelocity.magnitude;

        // Calculate the time to reach the target based on projectile speed and distance
        float timeToTarget = distance / projectileSpeed;

        // Calculate lead position based on player velocity and time to target
        return targetPosition + targetVelocity * timeToTarget;
    }
}
