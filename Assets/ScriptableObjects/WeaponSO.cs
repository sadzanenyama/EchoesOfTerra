using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "SO/Ship/Weapon")]
public class WeaponSO : ScriptableObject
{
    [Header("Base Stats")]
    public float projectileSpeed;
    public float fireRate;
    public float accuracy;
    public float damage;

    [Header("Heat")]
    public float heatPerShot;
    public float coolRate;

    [Header("Other")]
    public AudioClip shootSound;
    public AudioClip overheatSound;


    [Header("Upgraded stats")]
    private float projectileSpeedUpgraded;
    private float fireRateUpgraded;
    private float accuracyUpgraded;
    private float damageUpgraded;

}
