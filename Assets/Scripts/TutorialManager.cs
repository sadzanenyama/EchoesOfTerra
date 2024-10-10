using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] uipopupPanel;
    public GameObject[] playerMovementPopUp;

    private int currentIndexInGame = 0;
    private int currentIndexPlayerMovement = 0;

    private bool inplayUIComplete = false;
    private bool inplayPlayerMovementUIComplete = false;

    public void Start()
    {
       
        DeactivateAllUI();
        DeactivateAllPlayerUI();
        ShowCurrentUIPanel();
    }

    public void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.Q) && !inplayUIComplete)
        {
            UpdateInt();
        }

     
        if (inplayUIComplete && !inplayPlayerMovementUIComplete)
        {

            DeactivateAllUI();
            ShowCurrentPlayerMovementPopup();

 
            if (Input.GetKeyDown(KeyCode.A))
            {
                UpdatePlayerMovement();
            }
        }
    }


    public void ShowCurrentUIPanel()
    {
        DeactivateAllUI();

        if (currentIndexInGame < uipopupPanel.Length)
        {
            uipopupPanel[currentIndexInGame].SetActive(true);
        }

        if (currentIndexInGame == uipopupPanel.Length)
        {
            inplayUIComplete = true;
        }
    }

    public void UpdateInt()
    {
        if (!inplayUIComplete)
        {
            currentIndexInGame++;
            ShowCurrentUIPanel();
        }
    }

  
    public void ShowCurrentPlayerMovementPopup()
    {
        DeactivateAllPlayerUI();

        if (currentIndexPlayerMovement < playerMovementPopUp.Length)
        {
            playerMovementPopUp[currentIndexPlayerMovement].SetActive(true);
        }
    }

    
    public void UpdatePlayerMovement()
    {
        currentIndexPlayerMovement++;

        if (currentIndexPlayerMovement < playerMovementPopUp.Length)
        {
            ShowCurrentPlayerMovementPopup();
        }
        else
        {
            StartCoroutine(EndPlayerMovementSequence());
        }
    }

   
    private IEnumerator EndPlayerMovementSequence()
    {
        yield return new WaitForSeconds(0.3f);
        DeactivateAllPlayerUI();
        inplayPlayerMovementUIComplete = true;
    }


    public void DeactivateAllUI()
    {
        foreach (GameObject uiPanel in uipopupPanel)
        {
            uiPanel.SetActive(false);
        }
    }

  
    public void DeactivateAllPlayerUI()
    {
        foreach (GameObject playerPopup in playerMovementPopUp)
        {
            playerPopup.SetActive(false);
        }
    }
}
