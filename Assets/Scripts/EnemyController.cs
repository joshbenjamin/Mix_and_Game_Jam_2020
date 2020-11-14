using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform enemy;

    private Vector3 startPosition;
    private bool moveRight;
    private bool moveUp;
    private float moveSpeedHorizontal = 5f;
    private float moveSpeedVertical = 2f;
    private float maxWidthMove = 3f;
    private float maxHeightMove = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = enemy.position;
        moveRight = true;
        moveUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoves();
        MoveEnemy();
    }

    void CheckMoves()
    {
        if (enemy.position.x > (startPosition.x + maxWidthMove))
        {
            moveRight = false;
        }
        else if (enemy.position.x < (startPosition.x - maxWidthMove))
        {
            moveRight = true;
        }

        if (enemy.position.y > (startPosition.y + maxHeightMove))
        {
            moveUp = false;
        }
        else if (enemy.position.y < (startPosition.y - maxHeightMove))
        {
            moveUp = true;
        }
    }

    void MoveEnemy()
    {
        if (moveRight)
        {
            enemy.position += new Vector3(moveSpeedHorizontal * Time.deltaTime, 0, 0);
        }
        else
        {
            enemy.position -= new Vector3(moveSpeedHorizontal * Time.deltaTime, 0, 0);
        }

        if (moveUp)
        {
            enemy.position += new Vector3(0, moveSpeedVertical * Time.deltaTime, 0);
        }
        else
        {
            enemy.position -= new Vector3(0, moveSpeedVertical * Time.deltaTime, 0);
        }
    }
}
