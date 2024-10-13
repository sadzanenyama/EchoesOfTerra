using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
  
        public void MainMenu()
        {
            PlayerPrefs.SetString("StartScreen", "MainMenu");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Main");

        }

    public void ExitGame()
    {
        Application.Quit(); 
    }
    
}
