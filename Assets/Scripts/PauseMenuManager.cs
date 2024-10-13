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
                pauseMenuPanel.SetActive(true);
                Pause();
            }
        }
    }

    public void Pause()
    {


        canvasManager.SetCanvas(0);
        PauseManager.Pause();     
    }

    public void Resume()
    {

   
        canvasManager.SetCanvas(-1);
        PauseManager.Resume();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuName);
    }

    public void OptionsMenu()
    {
        canvasManager.SetCanvas(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToBase()
    {
        canvasManager.BackToBaseCanvas();
    }
}
