using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void CreditsPage()
    {
        UIManager.UIManagerInstance.SetCreditsPage();
    }
    public void QuitGameMenu()
    {
        UIManager.UIManagerInstance.SetFPSActive(false);
        UIManager.UIManagerInstance.QuitGameMenu();
    }
}
