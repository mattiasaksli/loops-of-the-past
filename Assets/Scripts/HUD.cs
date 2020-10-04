using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public static HUD Instance;
    public GameObject heartParticles;
    
    void Start()
    {
        Instance = this;
    }

    public void RemoveLife()
    {
        Image objectToDestroy = gameObject.GetComponentsInChildren<Image>()[1];
        Destroy(objectToDestroy);
    }

}
