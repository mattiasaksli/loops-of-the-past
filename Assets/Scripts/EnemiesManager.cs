using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    public PlayerController player;
    public List<GameObject> enemyObjects;
    public GameObject WinUI;
    public GameObject NextActionSprite;
    private int scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        WinUI = GameObject.FindGameObjectWithTag("WinUI");
        WinUI.SetActive(false);
        
        NextActionSprite = GameObject.FindGameObjectWithTag("CloneNextAction");
        NextActionSprite.SetActive(false);

        if (scene > 1)
        {
            NextActionSprite.SetActive(true);
            
            EvilCloneController evilClone = Instantiate(Resources.Load("EvilClone") as GameObject, new Vector3(1.5f, 1.5f, 0f), Quaternion.identity).GetComponent<EvilCloneController>();
            evilClone.ActionList = new Stack<string>(InputSaver.saver.GetInputActions());
            evilClone.gameObject.transform.parent = transform;
            
            InputSaver.saver.SetInputActions(new Stack<string>());
        }

        enemyObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }


    private void OnEnable()
    {
        EnemyEventManager.OnEnemyKilled += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        EnemyEventManager.OnEnemyKilled -= RemoveEnemyFromList;
    }

    private void RemoveEnemyFromList(GameObject enemy)
    {
        enemyObjects.Remove(enemy);
        if (enemyObjects.Count == 0)
        {
            WinLevel();
        }
    }

    private void WinLevel()
    {
        InputSaver.saver.SetInputActions(player.futureCloneActions);
        WinUI.SetActive(true);
        PlayerController.isInputDisabled = true;
    }
}
