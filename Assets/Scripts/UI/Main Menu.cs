using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public void StartGame()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick");
        UIManager.UIManagerInstance.SetLevelsScreen();
    }

    public void SettingsPage()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick");
        UIManager.UIManagerInstance.SetSettingsPage();
    }

    public void CreditsPage()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick");
        UIManager.UIManagerInstance.SetCreditsPage();
    }
    public void QuitGameMenu()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick");
        UIManager.UIManagerInstance.SetFPSActive(false);
        UIManager.UIManagerInstance.QuitGameMenu();
    }
}
