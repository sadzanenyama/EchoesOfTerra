using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip ExplosionSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Explode()
    {
        particleSystem.Stop();
        _audioSource.PlayOneShot(ExplosionSound, 0.4f);
        particleSystem.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.root.CompareTag("Trooper"))
        {
            int planetDamage = collision.collider.transform.root.GetComponent<MoveTowardsTarget>().enemyStats.planetDamage;
            Explode();
            IngameUI.Instance.ReducePopulation(planetDamage);
            Destroy(collision.gameObject);
        }
    }

}
