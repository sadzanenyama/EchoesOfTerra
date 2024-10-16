using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelectPage : MonoBehaviour
{
    [SerializeField] private LevelSelection levelOne;
    [SerializeField] private LevelSelection levelTwoA;
    [SerializeField] private LevelSelection levelTwoB;
    [SerializeField] private Dialogue dialogue;

    [SerializeField] private GameObject newTerraText;
    [SerializeField] private GameObject oldEarthText;

    public void Start()
    {
        Dialogue.instance.gameObject.SetActive(false);
        int LevelOneCompleted = PlayerPrefs.GetInt("SampleScene", 0);

        if (LevelOneCompleted == 0)
        {
            levelTwoA.SetLockedState(true);
            levelTwoA.SetUnlocked(false);
            levelTwoB.SetLockedState(true);
            levelTwoB.SetUnlocked(false);
            newTerraText.SetActive(false);
            oldEarthText.SetActive(false);
        }
        else if (LevelOneCompleted == 1)
        {
            Dialogue.instance.DisplayMainMessage();
            levelTwoA.SetLockedState(false);
            levelTwoA.SetUnlocked(true);
            levelTwoB.SetLockedState(false);
            levelTwoB.SetUnlocked(true);
            newTerraText.SetActive(true);
            oldEarthText.SetActive(true);
        }
    }

    public void Back()
    {
        UIManager.UIManagerInstance.SetMainMenuPage();
    }

    public void StartLevelOne()
    {
        UIManager.UIManagerInstance.LevelToLoad = "Cutscene";
        UIManager.UIManagerInstance.ShowUpgradePanel();
    }

    public void StartLevelNewTerra()
    {
        UIManager.UIManagerInstance.LevelToLoad = "NewTerra";
        UIManager.UIManagerInstance.ShowUpgradePanel();
    }

    public void StartLevelOldEarth()
    {
        UIManager.UIManagerInstance.LevelToLoad = "OldEarth";
        UIManager.UIManagerInstance.ShowUpgradePanel();
    }
}
