using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : MonoBehaviour
{

    public TextMeshProUGUI tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowMessage("Press space to attack, use WASD to move. Watch out for th...", 5));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     IEnumerator ShowMessage (string message, float delay) {
     tutorialText.text = message;
     tutorialText.enabled = true;
     yield return new WaitForSeconds(delay);
     tutorialText.enabled = false;
 }
}
