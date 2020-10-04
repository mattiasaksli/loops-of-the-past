using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventManager : MonoBehaviour
{
    public delegate void EnemyKilledAction(GameObject enemy);

    public static EnemyKilledAction OnEnemyKilled;

    public static void TriggerEnemyKilled(GameObject enemy)
    {
        OnEnemyKilled?.Invoke(enemy);
    }
}
