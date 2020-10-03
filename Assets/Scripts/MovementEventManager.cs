using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementEventManager : MonoBehaviour
{
    public delegate void MoveAction();

    public static event MoveAction OnPlayerMove;

    public static void TriggerMovement()
    {
        if (OnPlayerMove != null)
        {
            OnPlayerMove();
        }
    }
}