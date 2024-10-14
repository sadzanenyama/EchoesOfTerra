using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPage : MonoBehaviour
{
    public void Back()
    {
        UIManager.UIManagerInstance.SetMainMenuPage();
    }
}
