using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;

    private Transform player;
    private SpaceShipManager playerHealth;
    private ParticleSystem particleSystem;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerHealth = player.GetComponent<SpaceShipManager>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void Explode(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        particleSystem.Play();

        float distance = Vector3.Distance(player.position, transform.position);

        float damageToDo = enemyStats.maxExplosionDamage - enemyStats.damageFallOff * (distance) * (distance);

        if (damageToDo < 0)
        {
            damageToDo = 0;
        }

        Debug.Log("Did " + damageToDo + " at a dist of " + distance);

        playerHealth.TakeDamage(damageToDo);
    }

}
