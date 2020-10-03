using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseAndResumeScript : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("PauseMenuScene", LoadSceneMode.Additive);
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseMenuScene");
        Debug.Log("Game Resumed");
    }
}
