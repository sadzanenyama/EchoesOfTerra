using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private Image _gunHealth;
    [SerializeField] private Image _shipHealth;
    [SerializeField] private Image _shieldHealth;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _statsPanel;
    private SpaceShipManager player;
    private WeaponManager weaponPlayer;
    public ShipSO shipStats;
    private int enemiesKilled; 
    public static IngameUI Instance { get; private set; }

    private void Awake()
    {
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
        float heat = weaponPlayer.GetCurrentHeat();
        float maxHeat = weaponPlayer.GetMaxHeat();
        float normalizedHeat = Mathf.Clamp01(heat / maxHeat);
        _gunHealth.fillAmount = -(normalizedHeat - 1f);
        _shieldHealth.fillAmount = player.currentShield/ player.shipStats.shieldHealth;
        _shipHealth.fillAmount = player.currentHealth / player.shipStats.hullHealth;
        if (player.isDead)
        {
            DisplayStatsAndGameOver(); 
        }
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
