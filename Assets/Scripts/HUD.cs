using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public static HUD Instance;
    public GameObject LifePanel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void RemoveLife()
    {
        Destroy(LifePanel.GetComponentsInChildren<Image>()[1]);
    }

}
