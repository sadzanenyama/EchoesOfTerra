using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipManager : MonoBehaviour
{
    public ShipSO shipStats;
    public float currentHealth;
    public float currentShield;

    private float timeTilShieldRecharge;
    private bool isDead = false; 
    public delegate void DamageAction();
    public event DamageAction OnTakeDamage;

    bool isPlayer = false;
    bool playedShieldBreakSound;

    public AudioClip shieldBreakSound;
    private AudioSource audioSource;

    private void Awake()
    {
        isPlayer = gameObject.tag == "Player";
        currentHealth = shipStats.hullHealth;
        currentShield = shipStats.shieldHealth;
        timeTilShieldRecharge = 0;

        audioSource = GetComponent<AudioSource>();
    }
    public bool GetPlayerState()
    {
        return isDead; 
    }
    private void OnEnable()
    {
        currentHealth = shipStats.hullHealth;
        currentShield = shipStats.shieldHealth;
        isDead = false;
        timeTilShieldRecharge = 0;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        if (currentHealth <= 0)
        {
            isDead = true;
            Die();          
        }

        if (currentShield > 0)
        {
            currentShield -= damage;
        }
        else
        {
            currentHealth -= damage;
        }
        timeTilShieldRecharge = shipStats.shieldRechargeDelay;

        OnTakeDamage?.Invoke();
    }

    void Die()
    {
        GameObject explosion = ObjectPooler.Singleton.GetPooledObjectByTag("Explosion");
        explosion.GetComponent<ExplosionDamage>().Explode(transform.position);

        if(isPlayer)
        {
           IngameUI.Instance.StartCoroutine(IngameUI.Instance.GameOver());
        }

        gameObject.SetActive(false);
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

        if (!isPlayer)
            return;

        if (currentShield > 0.1f * shipStats.shieldHealth)
        {
            playedShieldBreakSound = false;
        }

        if (currentShield <= 0 && !playedShieldBreakSound)
        {
            audioSource.PlayOneShot(shieldBreakSound, 0.2f);
            playedShieldBreakSound = true;
        }
    }
}
