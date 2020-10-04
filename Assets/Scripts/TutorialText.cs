using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;

    void Start()
    {
        StartCoroutine(ShowMessage("Space to attack, \nWASD to move, \nEsc to pause.", 5));
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        tutorialText.text = message;
        tutorialText.enabled = true;
        yield return new WaitForSeconds(delay);
        tutorialText.enabled = false;
    }
}