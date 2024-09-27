using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPage : MonoBehaviour
{

    public void Back()
    {
        UIManager.UIManagerInstance.SetMainMenuPage();
    }
}
