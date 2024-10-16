using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    private EnemyStatsSO enemyStats;

    public enum RangedEnemyState {Following, Dodging };
    public RangedEnemyState currentState;

    private float distance;
    private Transform playerTrans;
    private Rigidbody rb;

    public float dodgeForce = 5f; // Speed of the dodge
    public float detectionRadius = 10f; // Radius within which the enemy detects projectiles
    public LayerMask projectileLayer; // Layer mask for player projectiles

    public float timeTilNextDodge = 0f;
    public float timeBetweenDodge = 1f;
    private NavMeshAgent agent;

    private WeaponManager[] weaponManager;

    private Transform model;

    private Quaternion targetRotation;

    private void Start()
    {
        model = transform.GetChild(0);
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        currentState = RangedEnemyState.Following;
        playerTrans = GameObject.FindWithTag("Player").transform;
        enemyStats = GetComponent<EnemyPathfinding>().enemyStats;

        weaponManager = GetComponentsInChildren<WeaponManager>();

        targetRotation = model.localRotation * Quaternion.Euler(0, 360, 0);
    }

    private void OnEnable()
    {
        currentState = RangedEnemyState.Following;
        timeTilNextDodge = 0f;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, playerTrans.position);

        if (distance < enemyStats.shootDistance)
        {
            for (int i = 0; i < weaponManager.Length; i++)
            {
                weaponManager[i].Shoot("EnemyProjectile");
            }
        }

        if(timeTilNextDodge > 0)
            timeTilNextDodge -= Time.deltaTime;

        if (currentState == RangedEnemyState.Dodging)
        {
            if(timeTilNextDodge <= 0f)
                StartCoroutine(Dodge());

            model.Rotate(Vector3.up, 900f * Time.deltaTime);
        }
        else
        {
            if(timeTilNextDodge <= 0f)
                DetectIncomingProjectiles();

            model.localRotation = Quaternion.RotateTowards(model.localRotation, targetRotation, 900f * Time.deltaTime);
        }
    }

    void DetectIncomingProjectiles()
    {
        Collider[] projectiles = Physics.OverlapSphere(transform.position, detectionRadius, projectileLayer);

        foreach (Collider proj in projectiles)
        {
            Rigidbody projRb = proj.GetComponent<Rigidbody>();
            if (projRb != null)
            {
                Vector3 toProjectile = proj.transform.position - transform.position;
                float distanceToProjectile = toProjectile.magnitude;

                // Check if the projectile is moving towards the enemy
                if (Vector3.Dot(projRb.velocity.normalized, -toProjectile.normalized) > 0.9f)
                {
                    // Initiate dodge in perpendicular direction
                    currentState = RangedEnemyState.Dodging;
                    break;
                }
            }
        }
    }

    IEnumerator Dodge()
    {
        agent.enabled = false;
        rb.isKinematic = false;
        Vector3 dodgeDirection = Random.Range(0, 2) == 0 ? -transform.right : transform.right;
        rb.AddForce(dodgeDirection * dodgeForce, ForceMode.VelocityChange);
        timeTilNextDodge = timeBetweenDodge + Random.Range(-timeBetweenDodge/2, timeBetweenDodge/2);

        yield return new WaitForSeconds(0.4f);

        rb.isKinematic = true;
        agent.enabled = true;
        currentState = RangedEnemyState.Following;
    }
}
