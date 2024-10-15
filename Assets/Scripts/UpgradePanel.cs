using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    private int maxUpgradePips = 5; 
    [Header("Visual Elements")]
    private int playerXPPoints = 0;
    public TextMeshProUGUI userXPPointsText;
    [Header("Upgrade system")]
    [SerializeField] private UpgradeOption _weaponSystem;
    [SerializeField] private UpgradeOption _engineSystem;
    [SerializeField] private UpgradeOption _shieldSystem;
    [SerializeField] private GameObject _noMoreUpgrades;

    [SerializeField] private AudioClip upgradeSuccess;
    [SerializeField] private AudioClip upgradeFailed;
    private AudioSource audioSource;

    private int _weaponIndexPips = 1;
    private int _shipIndexPips = 1;
    private int _engineIndexPips = 1;

    private int currentXPAssigned;

    string LevelToLoad;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _engineSystem.SetPipsActive(_weaponIndexPips);
        _weaponSystem.SetPipsActive(_shipIndexPips);
        _shieldSystem.SetPipsActive(_engineIndexPips);
    }

    public void SetLevelOneXP()
    {
        playerXPPoints = 4;
        currentXPAssigned = 4;
        userXPPointsText.text = playerXPPoints.ToString() + "XP";
    }

    public void SetLevelTwoXP()
    {
        playerXPPoints = 9;
        currentXPAssigned = 9;
        userXPPointsText.text = playerXPPoints.ToString() + "XP";
    }

    public void OnEnable()
    {
        userXPPointsText.text = playerXPPoints.ToString() + "XP";
        ResetUpgrades();
    }
    public void Back()
    {
        this.gameObject.SetActive(false);
        ResetUpgrades(); 
    }

    public void IncreaseWeapon()
    {
        if (!CheckPips(_weaponIndexPips) && playerXPPoints > 0)
        {
            _weaponIndexPips++;
            _weaponSystem.SetPipsActive(_weaponIndexPips);
            playerXPPoints -= 1;
            userXPPointsText.text = playerXPPoints.ToString() + "XP";
            audioSource.PlayOneShot(upgradeSuccess, 0.3f);
            PlayerPrefs.SetInt("WeaponUpgrades", _weaponIndexPips);
            Debug.Log("Setting weapon");
        }
        else
        {
            audioSource.PlayOneShot(upgradeFailed, 0.3f);
            // display cannot upgrade more 
            return;
        }
    }

    public void IncreaseEngine()
    {
        if (!CheckPips(_engineIndexPips) && playerXPPoints > 0)
        {
            _engineIndexPips++;
            _engineSystem.SetPipsActive(_engineIndexPips);
            playerXPPoints -= 1;
            userXPPointsText.text = playerXPPoints.ToString() + "XP";
            audioSource.PlayOneShot(upgradeSuccess, 0.3f);
            PlayerPrefs.SetInt("EngineUpgrades", _engineIndexPips);
            Debug.Log("Setting engine");
        }
        else
        {
            audioSource.PlayOneShot(upgradeFailed, 0.3f);
            // display cannot upgrade more 
            return; 
        }
    }
    public void IncreaseShield()
    {
        if (!CheckPips(_shipIndexPips) && playerXPPoints > 0)
        {
            _shipIndexPips++;
            _shieldSystem.SetPipsActive(_shipIndexPips);
            playerXPPoints -= 1;
            userXPPointsText.text = playerXPPoints.ToString() + "XP";
            audioSource.PlayOneShot(upgradeSuccess, 0.3f);
            PlayerPrefs.SetInt("ShieldUpgrades", _shipIndexPips);
            Debug.Log("Setting shield");
        }
        else
        {
            audioSource.PlayOneShot(upgradeFailed, 0.3f);
            // display cannot upgrade more 
            return;
        }

    }

    
    public void ResetUpgrades()
    {
        _engineSystem.ResetPips();
        _weaponSystem.ResetPips();
        _shieldSystem.ResetPips();
        playerXPPoints = currentXPAssigned;
        _weaponIndexPips = 1;
        _shipIndexPips = 1;
        _engineIndexPips = 1;
        PlayerPrefs.SetInt("EngineUpgrades", 1);
        PlayerPrefs.SetInt("ShieldUpgrades", 1);
        PlayerPrefs.SetInt("WeaponUpgrades", 1);
        userXPPointsText.text = playerXPPoints.ToString() + "XP";
    }

    public bool CheckPips(int value)
    {
        return value == maxUpgradePips; 
    }
    public void StartGame()
    {
        UIManager.UIManagerInstance.StartLevel(); 
    }
}
