using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{
    private static PlayerMovement playerMovement;
    private static PlayerWeapon playerWeapon;
    private static PlayerAiming playerAiming;
    private static GameObject HUD;

    public static bool isPaused = false;

    public static void SetVariables()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerWeapon = GameObject.FindWithTag("Player").GetComponent<PlayerWeapon>();
        playerAiming = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<PlayerAiming>();
        HUD = GameObject.FindWithTag("UI");
    }

    public static void Resume(bool pauseTime = true)
    {
        playerMovement.enabled = true;
        playerWeapon.enabled = true;
        playerAiming.enabled = true;
        HUD.SetActive(true);

        isPaused = false;

        if (pauseTime)
            Time.timeScale = 1;
    }

    public static void Pause(bool pauseTime = true)
    {
        playerMovement.enabled = false;
        playerWeapon.enabled = false;
        playerAiming.enabled = false;
        HUD.SetActive(false);

        isPaused = true;

        if (pauseTime)
            Time.timeScale = 0;
    }
}
