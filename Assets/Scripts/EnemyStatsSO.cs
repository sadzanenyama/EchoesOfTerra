using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Movement")]
    public float movementSpeed;
    public float speedVariation;
    public float turningSpeed;
    public float acceleration;

    [Header("Melee")]
    public float explodeDistance;
    public float fuseTime;
    public float maxExplosionDamage;
    public float damageFallOff;
    public AudioClip deadMelee; 

    [Header("Health Stats")]
    public ShipSO shipStats;
}
