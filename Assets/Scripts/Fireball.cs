using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireball : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 movementTargetVector;
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

    
    void Update()
    {
        // if (lookDirection == Look.RIGHT) spriteRenderer.flipX = false;
        // else if (lookDirection == Look.LEFT) spriteRenderer.flipX = true;
        // else if (lookDirection == Look.UP) transform.Rotate(new Vector3(0, 1, 0), 90f);
        // else if (lookDirection == Look.DOWN)
        // {
        //     transform.Rotate(new Vector3(0, 1, 0), 90f);
        //     spriteRenderer.flipX = true;
        // }
        
        //transform.position = Vector3.MoveTowards(transform.position, movementTargetVector, moveSpeed * Time.deltaTime);

        transform.position += movementTargetVector * (Time.deltaTime * moveSpeed);
    }
}
