using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExplode : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform explosionPosition;

    private float distanceToPlayer;
    private Transform player;
    private AudioSource audioSource; 

    private bool explode;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartExplode();
        }

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
        explode = true;
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(enemyStats.fuseTime);
        GameObject explosion = ObjectPooler.Singleton.GetPooledObjectByTag("Explosion");
        explosion.GetComponent<ExplosionDamage>().Explode(explosionPosition.position);
        audioSource.PlayOneShot(enemyStats.deadMelee,1f);
        gameObject.SetActive(false);
    }
}
