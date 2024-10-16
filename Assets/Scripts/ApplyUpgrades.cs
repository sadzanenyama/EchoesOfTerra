using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyUpgrades : MonoBehaviour
{
    public int weaponUpgrades;
    public int engineUpgrades;
    public int shipUpgrades;

    public PlayerMovementSO[] movementUpgradesStats;
    public WeaponSO[] weaponUpgradesStats;
    public ShipSO[] shipUpgradesStats;

    private void Awake()
    {
       // weaponUpgrades = PlayerPrefs.GetInt("WeaponUpgrades", 1);
        //engineUpgrades = PlayerPrefs.GetInt("EngineUpgrades", 1);
      //  shipUpgrades = PlayerPrefs.GetInt("ShieldUpgrades", 1);

        GameObject player = GameObject.FindWithTag("Player");

        player.GetComponent<PlayerMovement>().movementStats = movementUpgradesStats[engineUpgrades - 1];
        player.GetComponent<SpaceShipManager>().shipStats = shipUpgradesStats[shipUpgrades - 1];
        player.GetComponent<WeaponManager>().weaponStats = weaponUpgradesStats[weaponUpgrades - 1];
    }
}
