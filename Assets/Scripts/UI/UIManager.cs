using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuScreen;
    [SerializeField] private GameObject _gameCoverScreen;
    [SerializeField] private GameObject _settingsPage;
    [SerializeField] private GameObject _creditsPage;
    [SerializeField] private GameObject _levelSelectionPage;
    [SerializeField] private Transform _screensParent;

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
        StartCoroutine(ShowGameCoverThenFadeOut());
    }

   
    private IEnumerator ShowGameCoverThenFadeOut()
    {
  
        SetAsActiveScreen(_gameCoverScreen);
        yield return new WaitForSeconds(3);
        SetAsActiveScreen(_mainMenuScreen);
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
    private IEnumerator QuitGame()
    {
        SetAsActiveScreen(_gameCoverScreen);
        yield return new WaitForSeconds(3);
        Application.Quit();
    }

    public void SetAsActiveScreen(GameObject screenToActivate)
    {
        foreach (Transform screen in _screensParent)
        {
            screen.gameObject.SetActive(false);
        }

        if (screenToActivate != null)
        {
            screenToActivate.SetActive(true);
            screenToActivate.transform.SetAsLastSibling();
        }
    }
}
