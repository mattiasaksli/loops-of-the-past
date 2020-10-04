using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public SpriteRenderer playerSprite;
    public GameObject fireballPrefab;
    public GameObject playerMovementParticles;

    public LayerMask movementCollision;

    public Animator anim;

    private PlayerBehaviour playerBehaviour;

    private enum Look
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }

    private Look lookDirection = Look.LEFT;
    private bool isMoving;

    public Stack<string> futureCloneActions;

    void Start()
    {
        movePoint.parent = null;
        futureCloneActions = new Stack<string>();
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        UpdateMovementEvent();

        if (!playerBehaviour.isDead)
        {
            UpdateMovement();   
        }
        if (!isMoving)
        {
            UpdateShootProjectile();
        }

        anim.SetBool("Moving", isMoving);
    }

    private void UpdateMovement()
    {
        if (lookDirection == Look.RIGHT) playerSprite.flipX = true;
        else if (lookDirection == Look.LEFT) playerSprite.flipX = false;

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            isMoving = false;

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontalInput) != 0f && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                AudioManager.Instance.Play("PlayerWalking");
                isMoving = true;
                Vector3 horizontalVec = new Vector3(horizontalInput, 0f, 0f);
                lookDirection = horizontalInput < 0 ? Look.LEFT : Look.RIGHT;

                if (!Physics2D.OverlapCircle(movePoint.position + horizontalVec, 0.2f, movementCollision))
                {
                    movePoint.position += horizontalVec;
                }
            }

            else if (Mathf.Abs(verticalInput) != 0f && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)))
            {
                AudioManager.Instance.Play("PlayerWalking");
                isMoving = true;
                Vector3 verticalVec = new Vector3(0f, verticalInput, 0f);
                lookDirection = verticalInput < 0 ? Look.DOWN : Look.UP;

                if (!Physics2D.OverlapCircle(movePoint.position + verticalVec, 0.2f, movementCollision))
                {
                    movePoint.position += verticalVec;
                }
            }
        }
        
        transform.position =
            Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }

    private void UpdateShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 prefabPos = transform.position;
            Quaternion prefabRot = transform.rotation;
            Vector3 movementVector = new Vector3(0, 0, 0);
            if (lookDirection == Look.LEFT)
            {
                prefabPos.x -= 1;
                prefabRot = Quaternion.Euler(0, 0, 180);
                movementVector.x = -1;
            }
            else if (lookDirection == Look.RIGHT)
            {
                prefabPos.x += 1;
                movementVector.x = 1;
            }
            else if (lookDirection == Look.UP)
            {
                prefabPos.y += 1;
                prefabRot = Quaternion.Euler(0, 0, 90);
                movementVector.y = 1;
            }
            else if (lookDirection == Look.DOWN)
            {
                prefabPos.y -= 1;
                prefabRot = Quaternion.Euler(0, 0, -90);
                movementVector.y = -1;
            }

            Fireball fireball = Instantiate(fireballPrefab, transform.position, prefabRot).GetComponent<Fireball>();
            fireball.movementTargetVector = movementVector;
            fireball.movementTargetPoint = prefabPos;
        }
    }

    private void spawnMovementParticles(Vector3 legsPosition)
    {
        GameObject particles = Instantiate(playerMovementParticles, legsPosition, Quaternion.identity);
        particles.transform.parent = gameObject.transform;
        Destroy(particles, 0.5f);
    }

    private void UpdateMovementEvent()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            if (!Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 legsPosition = new Vector2(transform.position.x, transform.position.y - 0.25f);
                spawnMovementParticles(legsPosition);
            }

            MovementEventManager.TriggerMovement();

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            if (horizontalInput < 0) futureCloneActions.Push("left");
            else if (horizontalInput > 0) futureCloneActions.Push("right");
            else if (verticalInput < 0) futureCloneActions.Push("down");
            else if (verticalInput > 0) futureCloneActions.Push("up");
            else if (Input.GetKeyDown(KeyCode.Space)) futureCloneActions.Push("shoot");
        }
        
        else if (Input.GetKeyDown(KeyCode.C))
        {
            EvilCloneController evilClone = Instantiate(Resources.Load("EvilClone") as GameObject, new Vector3(0.5f, 0.5f, 0f), Quaternion.identity).GetComponent<EvilCloneController>();
            evilClone.ActionList = new Stack<string>(futureCloneActions);
        }
    }
}