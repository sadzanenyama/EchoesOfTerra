using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private Image _gunHeat;
    [SerializeField] private Image _shipHealth;
    [SerializeField] private Image _shieldHealth;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _statsPanel;
    private SpaceShipManager player;
    private WeaponManager weaponPlayer;
    public ShipSO shipStats;

    private WeaponManager playerWeapon;
    private SpaceShipManager playerShip;

    private int enemiesKilled; 
    public static IngameUI Instance { get; private set; }

    private void Awake()
    {
        playerWeapon = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
        playerShip = GameObject.FindWithTag("Player").GetComponent<SpaceShipManager>();

        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SpaceShipManager>();
        weaponPlayer = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();

    }


    public void OnEnable()
    {
        AudioManager.instance.PlayMusic("SpaceSounds");
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        PlayerPrefs.SetString("StartScreen", "LevelSelectionPage");  
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
      
    }

    private void Update()
    {
        float heat = playerWeapon.GetCurrentHeat();
        float maxHeat = playerWeapon.GetMaxHeat();
        _gunHeat.fillAmount = heat / maxHeat;
        _shieldHealth.fillAmount = playerShip.GetCurrentShield() / playerShip.shipStats.shieldHealth;
        _shipHealth.fillAmount = playerShip.GetCurrentHealth() / playerShip.shipStats.hullHealth;
    }


    public void DisplayStatsAndGameOver()
    {
        _statsPanel.SetActive(true);
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {

        yield return new WaitForSeconds(5f);
        _statsPanel.SetActive(false);
        _gameOverPanel.SetActive(true);
    }
}
