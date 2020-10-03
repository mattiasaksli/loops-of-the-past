using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 movementTargetVector;
    public Vector3 movementTargetPoint;
    
    //public MovementController movCon;

    // private SpriteRenderer spriteRenderer;
    //
    // public enum Look
    // {
    //     UP,
    //     LEFT,
    //     DOWN,
    //     RIGHT
    // }
    // public Look lookDirection = Look.LEFT;

    void Start()
    {
        //movCon = GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        // if (lookDirection == Look.RIGHT) spriteRenderer.flipX = false;
        // else if (lookDirection == Look.LEFT) spriteRenderer.flipX = true;
        // else if (lookDirection == Look.UP) transform.Rotate(new Vector3(0, 1, 0), 90f);
        // else if (lookDirection == Look.DOWN)
        // {
        //     transform.Rotate(new Vector3(0, 1, 0), 90f);
        //     spriteRenderer.flipX = true;
        // }
        
        transform.position = Vector3.MoveTowards(transform.position, movementTargetPoint, moveSpeed * Time.deltaTime);

        //transform.position += movementTargetVector * (Time.deltaTime * moveSpeed);
    }

    private void OnEnable()
    {
        MovementEventManager.OnPlayerMove += Move;
    }

    private void OnDisable()
    {
        MovementEventManager.OnPlayerMove -= Move;
    }

    private void Move()
    {
        movementTargetPoint += movementTargetVector;
    }
}
