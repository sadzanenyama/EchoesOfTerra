using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponStats;
    [SerializeField] private Transform bulletSpawn;

    [SerializeField] private float timeTilNextShot;
    [SerializeField] private float currentHeat = 0f;
    [SerializeField] private const float maxHeat = 100f;

    [SerializeField] private bool overheated;

    public delegate void ShootAction();
    public event ShootAction OnShoot;

    private void Update()
    {
        currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

        timeTilNextShot -= Time.deltaTime * weaponStats.fireRate;
        if(currentHeat > 0)
            currentHeat -= Time.deltaTime * weaponStats.coolRate;

        if (currentHeat > maxHeat)
        {
            overheated = true;
        }

        if(currentHeat <= 0)
        {
            overheated = false;
        }
    }

    public void Shoot()
    {
        if(timeTilNextShot > 0 || overheated)
        {
            return; //Can't shoot
        }

        GameObject projectile = ObjectPooler.Singleton.GetPooledObjectByTag("Projectile");
        projectile.GetComponent<Projectile>().SetProjectile(bulletSpawn, weaponStats);

        timeTilNextShot = 1f;
        currentHeat += weaponStats.heatPerShot;
        OnShoot?.Invoke();
    }
}
