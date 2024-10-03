using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [SerializeField] private EnemyStatsSO enemyStats;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();

        agent.speed = enemyStats.movementSpeed;
        agent.speed -= Random.Range(-enemyStats.speedVariation, enemyStats.speedVariation);
        agent.angularSpeed = enemyStats.turningSpeed;
        agent.acceleration = enemyStats.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
