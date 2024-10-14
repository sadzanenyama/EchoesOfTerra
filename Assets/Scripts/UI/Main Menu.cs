using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void StartGame()
    {
        UIManager.UIManagerInstance.SetLevelsScreen();
    }

    public void SettingsPage()
    {
        UIManager.UIManagerInstance.SetSettingsPage();
    }

    public void Tutorial()
    {
        UIManager.UIManagerInstance.StartTutorial();       
    }

    public void CreditsPage()
    {
        UIManager.UIManagerInstance.SetCreditsPage();
    }
    public void QuitGameMenu()
    {
        Application.Quit();
    }
}
