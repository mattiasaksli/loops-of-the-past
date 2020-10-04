using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 movementTargetVector;
    public Vector3 movementTargetPoint;

    private void Start()
    {
        AudioManager.Instance.Play("Fireball");
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movementTargetPoint, moveSpeed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Slime>())
        {
            other.GetComponent<Slime>().Kill();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}