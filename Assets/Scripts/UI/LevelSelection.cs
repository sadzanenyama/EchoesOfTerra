using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private GameObject lockedLevel;
    private bool unlockedState;
    [SerializeField] private Button buttonInteracted;
     public void SetLockedState(bool locked)
    {
        lockedLevel.SetActive(locked);

    }
    public void SetUnlocked(bool unlocked)
    {
        unlockedState = unlocked;
        buttonInteracted.interactable = unlocked;
    }
    public bool IsLocked() { return unlockedState; }
}
