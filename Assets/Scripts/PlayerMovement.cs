using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask movementCollision;
    // TODO: create movemnt layer on tilemmap

    public Animator anim;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) != 0f)
            {
                Vector3 horizontalVec = new Vector3(horizontalInput, 0f, 0f);
                if (!Physics2D.OverlapCircle(movePoint.position + horizontalVec, 0.2f, movementCollision))
                {
                    movePoint.position += horizontalVec;
                }
            }

            else if (Mathf.Abs(verticalInput) != 0f)
            {
                Vector3 verticalVec = new Vector3(0f, verticalInput, 0f);
                if (!Physics2D.OverlapCircle(movePoint.position + verticalVec, 0.2f, movementCollision))
                {
                    movePoint.position += verticalVec;
                }
            }

            anim.SetBool("moving", false);
        }

        else
        {
            anim.SetBool("moving", true);
        }
    }
}