using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform enemy;

    #region Movement Variables
    private Vector3 startPosition;
    private bool moveRight;
    private bool moveUp;
    private float moveSpeedHorizontal = 5f;
    private float moveSpeedVertical = 2f;
    private float maxWidthMove = 3f;
    private float maxHeightMove = 0.5f;
    #endregion

    private float timeLastShot = 0f;
    private float shootEvery;
    public GameObject enemyBullet;
    public Transform shotPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = enemy.position;

        shootEvery = Random.Range(1f, 2f);

        int horiz = Random.Range(0, 2);
        int vert = Random.Range(0, 2);
        moveRight = horiz == 1 ? true : false;
        moveUp = vert == 1 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoves();
        MoveEnemy();
        ControlShot();
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

    void ControlShot()
    {
        timeLastShot += Time.deltaTime;

        if(timeLastShot > shootEvery)
        {
            timeLastShot = 0f;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerObject");
        Vector3 playerPos = player.transform.position;

        Vector3 shotDirection = playerPos - shotPosition.position;
        Debug.DrawLine(shotPosition.position, playerPos, Color.white, 2f);
        GameObject enBul = Instantiate(enemyBullet, shotPosition.position, Quaternion.identity);
        enBul.GetComponent<EnemyBulletController>().AddForce(shotDirection);
    }
}
