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
    [SerializeField] private GameObject moralChoice; 


    private void Start()
    {
        Dialogue.instance.OnDialogueComplete += HandleDialogueEnd;
    }

    private void OnDestroy()
    {
        Dialogue.instance.OnDialogueComplete -= HandleDialogueEnd;
    }
    public void OnEnable()
    {
        moralChoice.SetActive(false);
        string levelUnlocked = PlayerPrefs.GetString("CurrentLevel");
        if(levelUnlocked != null) 
        { 
            if(levelUnlocked == "LevelOne")
            {
                levelTwoA.SetLockedState(true);
                levelTwoA.SetUnlocked(false);
                levelTwoB.SetLockedState(true);
                levelTwoB.SetUnlocked(false);
            }
            else if (levelUnlocked =="LevelTwoA")
            {
                levelTwoB.SetLockedState(true);
                levelTwoB.SetUnlocked(false);

            }
            else
            {
                levelTwoA.SetLockedState(true);
                levelTwoA.SetUnlocked(false);
            }
            string status = PlayerPrefs.GetString(levelUnlocked);
            if(status != null && status== "completed" && levelUnlocked == "LevelOne")
            {
                // show the dialogue display
                dialogue.gameObject.SetActive(true); 
                Dialogue.instance.DisplayMainMessage(); 
            
            }
            
        }
    }
    // display dialema choice
    public void HandleDialogueEnd()
    {
        moralChoice.gameObject.SetActive(true);
    }
    public void ProtectOldEarth()
    {
        levelTwoB.SetUnlocked(true);
        levelTwoB.SetLockedState(false);

    }
    public void SaveNewTerra()
    {
        levelTwoA.SetUnlocked(true);
        levelTwoA.SetLockedState(false);

    }
    public void Update()
    {
       
    }
    public void Back()
    {
        UIManager.UIManagerInstance.SetMainMenuPage();
    }

    public void StartLevelOne()
    {
        PlayerPrefs.SetString("CurrentLevel", "LevelOne");
        UIManager.UIManagerInstance.ShowUpgradePanel();
    }


}
