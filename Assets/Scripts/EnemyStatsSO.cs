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
    public float stoppingDistance;

    [Header("Melee")]
    public float explodeDistance;
    public float fuseTime;
    public float maxExplosionDamage;
    public float damageFallOff;
    public AudioClip deadMelee;

    [Header("Ranged")]
    public float shootDistance;
    public WeaponSO enemyWeapon;

    [Header("Trooper")]
    public float explodeDistanceTrooper;
    public float fuseTimeTrooper;
    public float damageFallOffTrooper;
    public int planetDamage; 
    public AudioClip deadTrooper;

    [Header("Health Stats")]
    public ShipSO shipStats;
}
