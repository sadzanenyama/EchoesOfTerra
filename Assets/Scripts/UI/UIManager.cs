using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Tooltip("INPlayUI")]
    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _gameCoverScreen;
    [SerializeField] private GameObject _settingsPage;
    [SerializeField] private GameObject _creditsPage;
    [SerializeField] private GameObject _levelSelectionPage;
    [SerializeField] private GameObject _upgradePanel;
    [SerializeField] private Transform _screensParent;

    [Tooltip("FPS")]
    [SerializeField] private GameObject _fpsMarker; 
    public static UIManager UIManagerInstance { get; private set; }
    private CanvasGroup _gameCoverCanvasGroup;

    private void Awake()
    {
        if (UIManagerInstance != null && UIManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            UIManagerInstance = this;
        }

   
        if (_gameCoverScreen != null)
        {
            _gameCoverCanvasGroup = _gameCoverScreen.GetComponent<CanvasGroup>();
            if (_gameCoverCanvasGroup == null)
            {
                _gameCoverCanvasGroup = _gameCoverScreen.AddComponent<CanvasGroup>();
            }
        }
    }

    private void Start()
    {
        
        string startScreen = PlayerPrefs.GetString("StartScreen", "MainMenu");

       
        if (startScreen == "SettingsPage")
        {
            SetAsActiveScreen(_settingsPage);
        }
        else if (startScreen == "CreditsPage")
        {
            SetAsActiveScreen(_creditsPage);
        }
        else if (startScreen == "LevelSelectionPage")
        {
            SetAsActiveScreen(_levelSelectionPage);
        }
        else
        {
            PlayerPrefs.DeleteAll(); 
            StartCoroutine(ShowGameCoverThenFadeOut());
        }
    }


    private IEnumerator ShowGameCoverThenFadeOut()
    {
  
        SetAsActiveScreen(_gameCoverScreen);
        yield return new WaitForSeconds(3);
        SetAsActiveScreen(_mainMenuScreen);
    }

    public bool FPSMarkerActive()
    {
        return _fpsMarker.activeSelf; 
    }
    public void SetLevelsScreen()
    {
        SetAsActiveScreen(_levelSelectionPage);
    }
    public void SetSettingsPage()
    {
        SetAsActiveScreen(_settingsPage);
    }

    public void SetCreditsPage()
    {
        SetAsActiveScreen(_creditsPage);
    }

    public void SetMainMenuPage()
    {
        SetAsActiveScreen(_mainMenuScreen);
    }
    public void QuitGameMenu()
    {
        StartCoroutine(QuitGame());
    }

    public void SetFPSActive(bool value)
    {
        _fpsMarker.gameObject.SetActive(value);
    }

  

    private IEnumerator QuitGame()
    {
        SetAsActiveScreen(_gameCoverScreen);
        yield return new WaitForSeconds(3);
        Application.Quit();
    }

    public void SetAsActiveScreen(GameObject screenToActivate, bool deactivateOthers = true)
    {
        if (deactivateOthers)
        {
           
            foreach (Transform screen in _screensParent)
            {
                screen.gameObject.SetActive(false);
            }
        }

        if (screenToActivate != null)
        {
            screenToActivate.SetActive(true);
            screenToActivate.transform.SetAsLastSibling();  // Ensure the active screen is on top if necessary
        }
    }

    public void ShowUpgradePanel()
    {
        SetAsActiveScreen(_upgradePanel,false);
    }

    public void StartLevelOne()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
