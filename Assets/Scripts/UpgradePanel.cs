using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [Header("UserWeapons")]
    public WeaponSO weaponUpgrades;
    public ShipSO ship;
    public int userXPPoints;
    private int maxUpgradePips = 10; 
    [Header("Visual Elements")]
    private int playerXPPoints = 50;
    public TextMeshProUGUI userXPPointsText;
    [Header("Upgrade system")]
    [SerializeField] private UpgradeOption _weaponSystem;
    [SerializeField] private UpgradeOption _engineSystem;
    [SerializeField] private UpgradeOption _shieldSystem;
    [SerializeField] private GameObject _noMoreUpgrades; 

    private int _weaponIndexPips = 0;
    private int _shipIndexPips = 0;
    private int _engineIndexPips = 0;
    public void OnEnable()
    {
        userXPPointsText.text = playerXPPoints.ToString() + "XP";
    }

    public void IncreaseWeapon()
    {
        if (!CheckPips(_weaponIndexPips))
        {
            AudioManager.instance.PlayAudioSFX("Pip");
            _weaponIndexPips++;
            _weaponSystem.SetPipsActive(_weaponIndexPips);
            playerXPPoints -= 1;
            userXPPointsText.text = playerXPPoints.ToString() + "XP";
        }
        else
        {
            // display cannot upgrade more 
            return;
        }
    }

    public void IncreaseEngine()
    {
        if (!CheckPips(_engineIndexPips))
        {
            AudioManager.instance.PlayAudioSFX("Pip");
            _engineIndexPips++;
            _engineSystem.SetPipsActive(_engineIndexPips);
            playerXPPoints -= 1;
            userXPPointsText.text = playerXPPoints.ToString() + "XP";
        }
        else
        {
            // display cannot upgrade more 
            return; 
        }
    }
    public void IncreaseShield()
    {
        if (!CheckPips(_shipIndexPips))
        {
            AudioManager.instance.PlayAudioSFX("Pip");
            _shipIndexPips++;
            _shieldSystem.SetPipsActive(_shipIndexPips);
            playerXPPoints -= 1;
            userXPPointsText.text = playerXPPoints.ToString() + "XP";
        }
        else
        {
            // display cannot upgrade more 
            return;
        }

    }

    public void ResetUpgrades()
    {
        _engineSystem.ResetPips();
        _weaponSystem.ResetPips();
        _shieldSystem.ResetPips();  
        _weaponIndexPips = 0;
        _shipIndexPips = 0;
        _engineIndexPips = 0;
    }

    public bool CheckPips(int value)
    {
        return value == maxUpgradePips; 
    }
    public void StartGame()
    {
        AudioManager.instance.PlayAudioSFX("StartGame");
        UIManager.UIManagerInstance.StartLevelOne(); 
    }
}
