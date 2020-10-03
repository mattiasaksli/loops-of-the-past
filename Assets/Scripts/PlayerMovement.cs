using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public SpriteRenderer playerSprite;
    public GameObject fireballPrefab;

    public LayerMask movementCollision;
    // TODO: create movement layer on tilemap

    public Animator anim;

    private enum Look
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }
    private Look lookDirection = Look.LEFT;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        UpdateMovement();
        UpdateShootProjectile();
    }

    private void UpdateMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (lookDirection == Look.RIGHT) playerSprite.flipX = true;
        else if (lookDirection == Look.LEFT) playerSprite.flipX = false;

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
                    lookDirection = horizontalInput < 0 ? Look.LEFT : Look.RIGHT;
                }
            }

            else if (Mathf.Abs(verticalInput) != 0f)
            {
                Vector3 verticalVec = new Vector3(0f, verticalInput, 0f);
                if (!Physics2D.OverlapCircle(movePoint.position + verticalVec, 0.2f, movementCollision))
                {
                    movePoint.position += verticalVec;
                    lookDirection = verticalInput < 0 ? Look.DOWN : Look.UP;
                }
            }
        }
    }

    private void UpdateShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 targetPoint = transform.position;
            Quaternion targetRotation = transform.rotation;
            Vector3 movementVector = new Vector3(0, 0, 0);
            if (lookDirection == Look.LEFT)
            {
                targetPoint.x -= 1;
                targetRotation = Quaternion.Euler(0, 0, 180);
                movementVector.x = -1;
            }
            else if (lookDirection == Look.RIGHT)
            {
                targetPoint.x += 1;
                movementVector.x = 1;
            }
            else if (lookDirection == Look.UP)
            {
                targetPoint.y += 1;
                targetRotation = Quaternion.Euler(0, 0, 90);
                movementVector.y = 1;
            }
            else if (lookDirection == Look.DOWN)
            {
                targetPoint.y -= 1;
                targetRotation = Quaternion.Euler(0, 0, -90);
                movementVector.y = -1;
            }
            
            Fireball fireball = Instantiate(fireballPrefab, targetPoint, targetRotation).GetComponent<Fireball>();
            fireball.movementTargetVector = movementVector;
        }
    }
}