using UnityEngine;

public class SpaceShipManager : MonoBehaviour
{
    public static SpaceShipManager Instance { get; private set; }

    public ShipSO shipStats;
    public float currentHealth;
    public float currentShield;

    private float timeTilShieldRecharge;
    private bool isDead = false; 
    public delegate void DamageAction();
    public event DamageAction OnTakeDamage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentHealth = shipStats.hullHealth;
        currentShield = shipStats.shieldHealth;
        timeTilShieldRecharge = 0;
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            currentShield -= damage;
        }
        else
        {
            currentHealth -= damage;
        }

        timeTilShieldRecharge = shipStats.shieldRechargeDelay;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            currentHealth = 0;  
            IngameUI.Instance.DisplayStatsAndGameOver(); 
        }

        OnTakeDamage?.Invoke();
    }

    public float GetCurrentShield()
    {
        return currentShield;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Update()
    {
        currentShield = Mathf.Clamp(currentShield, 0, shipStats.shieldHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, shipStats.hullHealth);

        if (currentShield <= shipStats.shieldHealth)
        {
            timeTilShieldRecharge -= Time.deltaTime;

            if (timeTilShieldRecharge <= 0)
            {
                currentShield += shipStats.shieldRechargeRate * Time.deltaTime;
            }
        }
    }
}
