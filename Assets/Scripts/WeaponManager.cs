using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{ 
    public WeaponSO weaponStats;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private float timeTilNextShot;
    [SerializeField] private float currentHeat = 0f;
    [SerializeField] private const float maxHeat = 100f;

    public bool overheated;

    public delegate void ShootAction();
    public event ShootAction OnShoot;

    private AudioSource audioSource;

    bool isPlayer;

    private void Awake()
    {
        isPlayer = gameObject.tag == "Player";
        audioSource = GetComponent<AudioSource>();
    }

    public float GetCurrentHeat()
    {
        return currentHeat; 
    }

    public void AddHeat(float heat)
    {
        currentHeat += heat;
    }

    public float GetMaxHeat()
    {
        return maxHeat;
    }

    private void Update()
    {
        if (currentHeat > maxHeat)
        {
            if (!overheated)
            {
                audioSource.PlayOneShot(weaponStats.overheatSound, 0.8f);
                overheated = true;
            }
        }

        currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

        timeTilNextShot -= Time.deltaTime * weaponStats.fireRate;
        if (currentHeat > 0)
            currentHeat -= Time.deltaTime * weaponStats.coolRate;

        if (currentHeat <= 0)
        {
            overheated = false;
        }
    }

    public void Shoot(string projectileType = "Projectile")
    {
        if (timeTilNextShot > 0 || overheated)
        {
            return; //Can't shoot
        }

        muzzleFlash.Play(true);

        GameObject projectile = ObjectPooler.Singleton.GetPooledObjectByTag(projectileType);
        projectile.GetComponent<Projectile>().SetProjectile(bulletSpawn, weaponStats);

        timeTilNextShot = 1f;
        currentHeat += weaponStats.heatPerShot;
        audioSource.PlayOneShot(weaponStats.shootSound, 0.2f);
        if(isPlayer)
            OnShoot?.Invoke();
    }
}
