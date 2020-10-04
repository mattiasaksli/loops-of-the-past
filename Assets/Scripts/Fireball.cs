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

        if (movementTargetPoint.x > 7.5f)
        {
            movementTargetPoint = new Vector3(-9.5f, movementTargetPoint.y, movementTargetPoint.z);
            transform.position = movementTargetPoint;
        }
        else if (movementTargetPoint.x < -9.5f)
        {
            movementTargetPoint = new Vector3(7.5f, movementTargetPoint.y, movementTargetPoint.z);
            transform.position = movementTargetPoint;
        }
        else if (movementTargetPoint.y < -4f)
        {
            movementTargetPoint = new Vector3(movementTargetPoint.x, 5.5f, movementTargetPoint.z);
            transform.position = movementTargetPoint;
        }
        else if (movementTargetPoint.y > 5.5f)
        {
            movementTargetPoint = new Vector3(movementTargetPoint.x, -4f, movementTargetPoint.z);
            transform.position = movementTargetPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Slime>())
        {
            other.GetComponent<Slime>().Kill();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.GetComponent<EvilCloneController>())
        {
            other.GetComponent<EvilCloneController>().Kill();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}