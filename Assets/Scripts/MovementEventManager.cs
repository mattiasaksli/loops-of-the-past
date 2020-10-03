using UnityEngine;

public class MovementEventManager : MonoBehaviour
{
    public delegate void MoveAction();

    public static event MoveAction OnPlayerMove;

    public static void TriggerMovement()
    {
        OnPlayerMove?.Invoke();
    }
}