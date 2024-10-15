using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

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
    [SerializeField] private GameObject _loadingScreen; 
    [Tooltip("FPS")]
    [SerializeField] private GameObject _fpsMarker; 
    public static UIManager UIManagerInstance { get; private set; }
    private CanvasGroup _gameCoverCanvasGroup;

    public AudioMixer masterMixer;

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
        Time.timeScale = 1f;
        float sfxVol, musicVol;

        sfxVol = PlayerPrefs.GetFloat("SFXVolume");
        musicVol = PlayerPrefs.GetFloat("MusicVolume");

        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVol) * 20);
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);

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

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        Image loadingBar = _loadingScreen.GetComponent<LoadingScreen>().GetLoadingBar();
        SetAsActiveScreen(_loadingScreen);
        while(!operation.isDone) 
        { 
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            loadingBar.fillAmount = progressValue;  
            yield return null; 
        }
        
    }

    private IEnumerator QuitGame()
    {
       
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

    public void StartTutorial()
    {
        StartCoroutine(LoadSceneAsync(2));
    }
    public void StartLevelOne()
    {
        StartCoroutine(LoadSceneAsync(0)); 
    }
}
