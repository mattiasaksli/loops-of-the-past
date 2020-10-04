using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;
    public GameObject pauseMenu;
    public PlayerMovement PlayerMovement;
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        PlayerMovement.enabled = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        PlayerMovement.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
}
