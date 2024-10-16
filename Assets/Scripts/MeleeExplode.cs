using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExplode : MonoBehaviour
{
    private EnemyStatsSO enemyStats;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform explosionPosition;

    private float distanceToPlayer;
    private Transform player;
    private AudioSource audioSource; 

    private bool explode;

    private void Start()
    {
        enemyStats = GetComponent<EnemyPathfinding>().enemyStats;

        player = GameObject.FindWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        explode = false;
    }

    private void Update()
    {
        if (explode)
            return;

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if(distanceToPlayer <= enemyStats.explodeDistance && !explode)
        {
            StartExplode();
        }
    }

    void StartExplode()
    {       
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        explode = true;

        yield return new WaitForSeconds(enemyStats.fuseTime);
        GameObject explosion = ObjectPooler.Singleton.GetPooledObjectByTag("Explosion");
        explosion.GetComponent<ExplosionDamage>().Explode(explosionPosition.position);

        gameObject.SetActive(false);
    }
}
