using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    public void Back()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick"); 
        UIManager.UIManagerInstance.SetMainMenuPage();
    }
}
