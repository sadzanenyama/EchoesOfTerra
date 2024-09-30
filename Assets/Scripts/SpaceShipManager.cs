using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipManager : MonoBehaviour
{
    public ShipSO shipStats;
    public float currentHealth;
    public float currentShield;

    private float timeTilShieldRecharge;

    private void Awake()
    {
        currentHealth = shipStats.hullHealth;
        currentShield = shipStats.shieldHealth;
        timeTilShieldRecharge = 0;
    }

    public void TakeDamage(float damage)
    {
        if(currentShield > 0)
        {
            currentShield -= damage;
        }
        else
        {
            currentHealth -= damage;
        }

        timeTilShieldRecharge = shipStats.shieldRechargeDelay;

        if (currentHealth < 0)
        {
            Debug.Log("Die");
        }
    }

    private void Update()
    {
        currentShield = Mathf.Clamp(currentShield, 0, shipStats.shieldHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, shipStats.hullHealth);

        if (currentShield <= shipStats.shieldHealth)
        {
            timeTilShieldRecharge -= Time.deltaTime;

            if(timeTilShieldRecharge <= 0)
            {
                currentShield += shipStats.shieldRechargeRate * Time.deltaTime;
            }
        }
    }
}
