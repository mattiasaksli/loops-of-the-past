using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    void Start()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        if (scene == 1)
        {
            StartCoroutine(ShowMessage("Space to attack, \nWASD to move, \nEsc to pause.", 5));
        }
        else if (scene == 2)
        {
            StartCoroutine(ShowMessage("Beware the actions of your past self!", 5));
        }
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        tutorialText.text = message;
        tutorialText.enabled = true;
        yield return new WaitForSeconds(delay);
        tutorialText.enabled = false;
    }
}