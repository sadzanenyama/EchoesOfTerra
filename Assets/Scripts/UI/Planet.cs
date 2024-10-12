using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private ScreenShake _screenShake;
    [SerializeField] private AudioManager _audioManager;
    public void Explode(Vector3 position)
    {
        transform.position = position;
        particleSystem.Stop();
        _audioManager.PlayAudioSFX("Explosion");
        particleSystem.Play();
        _screenShake.ShootShake();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.root.CompareTag("Trooper"))
        {
            Vector3 collisionPosition = collision.contacts[0].point;
            int planetDamage = collision.collider.transform.root.GetComponent<EnemyPathfinding>().enemyStats.planetDamage;
            Explode(collisionPosition);
            IngameUI.Instance.ReducePopulation(planetDamage);
            Destroy(collision.gameObject);
        }
    }

}
