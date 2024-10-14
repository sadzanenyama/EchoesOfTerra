using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public string MainMenuName = "MainMenu";
    [SerializeField] private CanvasManager canvasManager = new CanvasManager();
    [SerializeField] private GameObject pauseMenuPanel; 
    private void Awake()
    {
        canvasManager.SetCanvas(-1);
        Time.timeScale = 1;
        PauseManager.SetVariables();
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvasManager.canvas[0].activeInHierarchy)
            {
                pauseMenuPanel.SetActive(false);
                Resume();
            }
            else
            {
                if (!PauseManager.isPaused)
                {
                    pauseMenuPanel.SetActive(true);
                    Pause();
                }
            }
        }
    }

    public void Pause()
    {
        PauseManager.Pause();     
    }

    public void Resume()
    {
        PauseManager.Resume();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
