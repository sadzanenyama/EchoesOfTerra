using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private Image _shipHealth;
    [SerializeField] private Image _shieldHealth;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _popDeadText;
    [SerializeField] private GameObject _statsPanel;
    [SerializeField] TextMeshProUGUI _planetPopulationText;
    [SerializeField] private GameObject overheatText;
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private TextMeshProUGUI waveText;

    private SpaceShipManager player;
    private WeaponManager weaponPlayer;
    public ShipSO shipStats;
    int populationPlanet = 100; // this may need to change based on the scene we are in 
    private WeaponManager playerWeapon;
    private SpaceShipManager playerShip;

    private int enemiesKilled;

    public static IngameUI Instance { get; private set; }

    [SerializeField] private RectTransform _gunHeat;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color heatColor;
    private Image gunHeatBar;
    float heatBarHeight;
    float heatBarWidth;

    private void Awake()
    {
        _popDeadText.text = "";
        heatBarHeight = _gunHeat.GetComponent<RectTransform>().sizeDelta.y;
        heatBarWidth = _gunHeat.GetComponent<RectTransform>().sizeDelta.x;

        gunHeatBar = _gunHeat.GetComponent<Image>();

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


    public void ReducePopulation(int planetAmount)
    {
        populationPlanet -= planetAmount;
        _popDeadText.text = "-" + planetAmount.ToString();
        _popDeadText.gameObject.GetComponent<Animator>().Play("PopDeathAnim", -1, 0f);
        _planetPopulationText.text = populationPlanet.ToString(); 
    }

    public void OnEnable()
    {
        _audioManager.PlayMusic("SpaceSounds");
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
        _gunHeat.sizeDelta = new Vector2(heatBarWidth * (heat / maxHeat), heatBarHeight);

        waveText.text = WaveSpawner.instance.waveNumber.ToString() + "/" + WaveSpawner.instance.GetNumWaves();

        gunHeatBar.color = Color.Lerp(normalColor, heatColor, heat/maxHeat);

        _shieldHealth.fillAmount = playerShip.GetCurrentShield() / playerShip.shipStats.shieldHealth;
        _shipHealth.fillAmount = playerShip.GetCurrentHealth() / playerShip.shipStats.hullHealth;

        overheatText.SetActive(weaponPlayer.overheated);
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
