using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetProjectile(Transform spawnPoint, WeaponSO stats)
    {
        gameObject.SetActive(true);
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
        float randomAngle = Random.Range(-stats.accuracy / 2, stats.accuracy / 2);

        transform.rotation *= Quaternion.Euler(0, randomAngle, 0);

        rb.velocity = transform.forward * stats.projectileSpeed;
        damage = stats.damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SpaceShipManager>())
            collision.gameObject.GetComponent<SpaceShipManager>().TakeDamage(damage);

        gameObject.SetActive(false);
    }
}
