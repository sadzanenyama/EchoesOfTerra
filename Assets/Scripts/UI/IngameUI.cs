using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class IngameUI : MonoBehaviour
{
    [SerializeField] private Image _shipHealth;
    [SerializeField] private Image _shieldHealth;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _popDeadText;
    [SerializeField] private GameObject _statsPanel;
    [SerializeField] TextMeshProUGUI _planetPopulationText;
    [SerializeField] private GameObject overheatText;

    [SerializeField] private TextMeshProUGUI waveText;

    private SpaceShipManager player;
    private WeaponManager weaponPlayer;
    public ShipSO shipStats;
    public int populationPlanet = 100; // this may need to change based on the scene we are in 
    private WeaponManager playerWeapon;
    private SpaceShipManager playerShip;

  

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
        _planetPopulationText.text = populationPlanet.ToString();
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
        _gameOverPanel.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);

        PauseManager.Pause(true);
        IngameUI.Instance.GameOverPanel();
    }


    public void ReducePopulation(int planetAmount)
    {
        populationPlanet -= planetAmount;
        _popDeadText.text = "-" + planetAmount.ToString();
        _popDeadText.gameObject.GetComponent<Animator>().Play("PopDeathAnim", -1, 0f);

        if (populationPlanet <= 0)
        {
            StartCoroutine(GameOver());
            populationPlanet = 0;
        }

            _planetPopulationText.text = populationPlanet.ToString(); 

    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private void Update()
    {
        float heat = playerWeapon.GetCurrentHeat();
        float maxHeat = playerWeapon.GetMaxHeat();

        float heatNormalized = Mathf.Clamp01(heat / maxHeat);

        _gunHeat.sizeDelta = new Vector2(heatBarWidth * (heatNormalized), heatBarHeight);

        waveText.text = (WaveSpawner.instance!=null) ? WaveSpawner.instance.waveNumber.ToString() + "/" + WaveSpawner.instance.GetNumWaves():"1";

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
