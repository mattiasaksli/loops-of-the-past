using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public static HUD Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void RemoveLife()
    {
        Destroy(gameObject.GetComponentsInChildren<Image>()[1]);
    }

}
