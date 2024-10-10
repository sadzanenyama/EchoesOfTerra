using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPage : MonoBehaviour
{
   public void Back()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick");
        UIManager.UIManagerInstance.SetMainMenuPage();
    }

    public void StartLevelOne()
    {
        UIManager.UIManagerInstance.ShowUpgradePanel();
    }


}
