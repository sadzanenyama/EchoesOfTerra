using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class ScreenShake : MonoBehaviour
{
    private SpaceShipManager spaceShip;
    private WeaponManager playerShoot;
    [SerializeField] private ShakeData hitShake;
    [SerializeField] private ShakeData shootShake;

    private void Start()
    {
        spaceShip = GetComponent<SpaceShipManager>();
        playerShoot = GetComponent<WeaponManager>();
        spaceShip.OnTakeDamage += DamageShake;
        playerShoot.OnShoot += ShootShake;
    }

    private void OnDestroy()
    {
        spaceShip.OnTakeDamage -= DamageShake;
        playerShoot.OnShoot -= ShootShake;
    }

    void DamageShake()
    {
        CameraShakerHandler.Shake(hitShake);
    }

    void ShootShake()
    {
        CameraShakerHandler.Shake(shootShake);
    }
}
