using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private float damage;
    private TrailRenderer tr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
    }

    public void SetProjectile(Transform spawnPoint, WeaponSO stats)
    {
        tr.Clear();
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
        if (collision.transform.root.gameObject.GetComponent<SpaceShipManager>())
        {
            collision.transform.root.gameObject.GetComponent<SpaceShipManager>().TakeDamage(damage);
            GameObject hitEffect = ObjectPooler.Singleton.GetPooledObjectByTag("HitEffect");
            hitEffect.SetActive(true);
            hitEffect.transform.position = collision.GetContact(0).point;
            hitEffect.GetComponent<ParticleSystem>().Play();
        }

        gameObject.SetActive(false);
    }
}
