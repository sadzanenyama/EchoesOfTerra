using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ship", menuName = "SO/Ship/Ship")]
public class ShipSO : ScriptableObject
{
    public float hullHealth;

    public float shieldHealth;
    public float shieldRechargeRate;
    public float shieldRechargeDelay;
}
