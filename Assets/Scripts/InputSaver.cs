using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSaver : MonoBehaviour
{
    public static InputSaver saver;
    private Stack<string> inputActions = new Stack<string>();

    private void Awake()
    {
        if (saver == null)
        {
            DontDestroyOnLoad(gameObject);
            saver = this;
        }
        else if (saver != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetInputActions(Stack<string> actions)
    {
        inputActions = actions;
    }

    public Stack<string> GetInputActions()
    {
        return inputActions;
    }
}
