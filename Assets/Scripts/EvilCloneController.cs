using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EvilCloneController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public SpriteRenderer evilCloneSprite;
    public LayerMask movementCollision;
    public GameObject fireballPrefab;
    public Sprite[] ActionImages;
    public Image NextActionSprite;

    private enum Look
    {
        UP,
        LEFT,
        DOWN,
        RIGHT
    }

    private Look lookDirection = Look.LEFT;

    public Stack<string> ActionList;

    private delegate void Action();

    private Action nextAction;

    void Start()
    {
        movePoint.parent = null;
        NextActionSprite = GameObject.FindGameObjectWithTag("CloneNextAction").GetComponent<Image>();
        SetNextAction();
    }

    void Update()
    {
        if (lookDirection == Look.RIGHT) evilCloneSprite.flipX = true;
        else if (lookDirection == Look.LEFT) evilCloneSprite.flipX = false;

        if (!(Vector3.Distance(transform.position, movePoint.position) <= 0.05f))
        {
            transform.position =
                Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        MovementEventManager.OnPlayerMove += DoNextAction;
    }

    private void OnDisable()
    {
        MovementEventManager.OnPlayerMove -= DoNextAction;
    }

    private void DoNextAction()
    {
        nextAction();
        SetNextAction();
    }

    private void SetNextAction()
    {
        if (ActionList.Count != 0)
        {
            string listAction = ActionList.Pop();
            switch (listAction)
            {
                case "left":
                    nextAction = MoveLeft;
                    NextActionSprite.transform.Rotate(new Vector3(0f, 0f, 1f), 180f);
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case "right":
                    nextAction = MoveRight;
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case "up":
                    nextAction = MoveUp;
                    NextActionSprite.transform.Rotate(new Vector3(0f, 0f, 1f), 90f);
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case "down":
                    nextAction = MoveDown;
                    NextActionSprite.transform.Rotate(new Vector3(0f, 0f, 1f), -90f);
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case "shoot":
                    nextAction = Shoot;
                    NextActionSprite.sprite = ActionImages[0];
                    break;
            }
        }
        else
        {
            int r = Random.Range(0, 5);
            switch (r)
            {
                case 0:
                    nextAction = MoveLeft;
                    NextActionSprite.transform.Rotate(new Vector3(0f, 0f, 1f), 180f);
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case 1:
                    nextAction = MoveRight;
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case 2:
                    nextAction = MoveUp;
                    NextActionSprite.transform.Rotate(new Vector3(0f, 0f, 1f), 90f);
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                case 3:
                    nextAction = MoveDown;
                    NextActionSprite.transform.Rotate(new Vector3(0f, 0f, 1f), -90f);
                    NextActionSprite.sprite = ActionImages[1];
                    break;
                default:
                    nextAction = Shoot;
                    NextActionSprite.sprite = ActionImages[0];
                    break;
            }
        }
    }

    private void MoveLeft()
    {
        Vector3 leftVec = new Vector3(-1f, 0f, 0f);
        lookDirection = Look.LEFT;
        if (!Physics2D.OverlapCircle(movePoint.position + leftVec, 0.2f, movementCollision))
        {
            movePoint.position += leftVec;
        }
    }

    private void MoveRight()
    {
        Vector3 rightVec = new Vector3(1f, 0f, 0f);
        lookDirection = Look.RIGHT;
        if (!Physics2D.OverlapCircle(movePoint.position + rightVec, 0.2f, movementCollision))
        {
            movePoint.position += rightVec;
        }
    }

    private void MoveUp()
    {
        Vector3 upVec = new Vector3(0f, 1f, 0f);
        lookDirection = Look.LEFT;
        if (!Physics2D.OverlapCircle(movePoint.position + upVec, 0.2f, movementCollision))
        {
            movePoint.position += upVec;
        }
    }

    private void MoveDown()
    {
        Vector3 downVec = new Vector3(0f, -1f, 0f);
        lookDirection = Look.LEFT;
        if (!Physics2D.OverlapCircle(movePoint.position + downVec, 0.2f, movementCollision))
        {
            movePoint.position += downVec;
        }
    }

    private void Shoot()
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