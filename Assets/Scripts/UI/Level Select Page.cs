using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPage : MonoBehaviour
{
   public void Back()
    {
        UIManager.UIManagerInstance.SetMainMenuPage();
    }
}
