using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Camera cam;

    private Transform player;
    public GameObject rotationPoint;

    public GameObject bullet;
    private List<GameObject> bullets;
    private float bulletCurrentCooldown = 0f;
    private float bulletCooldownTime = 10f;
    private float cooldownMultiplier = 4f;

    private float currentShootAngle;
    private float transitionSpeed;

    public EnemySpawner enemySpawner;
    private int score = 0;
    public TextController textController;

    public PowerBarController barController;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        player = GetComponent<Transform>();
        currentShootAngle = 0f;
        transitionSpeed = 15f;

        bullets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateAimLine();

        bulletCurrentCooldown -= Time.deltaTime * cooldownMultiplier;
        if (bulletCurrentCooldown < 0)
        {
            bulletCurrentCooldown = 0f;
        }

        barController.ScaleBar(bulletCurrentCooldown, bulletCooldownTime);
    }

    public void DebugDrawLine(Vector3 fromPosition, Vector2 mouseClickPosition)
    {
        Vector3 point = WorldPoint(mouseClickPosition.x, mouseClickPosition.y);

        Debug.DrawLine(fromPosition, point, Color.white, 2.5f);

        DrawRaycast();
    }

    public void DrawRaycast()
    {
        Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * currentShootAngle), Mathf.Sin(Mathf.Deg2Rad * currentShootAngle));

        //Physics2D.Raycast()
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(rotationPoint.transform.position.x, rotationPoint.transform.position.y, 0), direction);

        Debug.DrawRay(player.position, direction, Color.white, 5f);

        if (hit.collider != null)
        {
            switch (hit.collider.gameObject.tag)
            {
                case "Platform":
                    Debug.Log("Hit platform");
                    break;

                case "PlatformGap":
                    Debug.Log("Found gap");
                    Vector2 startPoint = hit.point;
                    Debug.Log("Starts at: " + startPoint + "\n" + 
                              "Angle at: " + currentShootAngle);

                    float distance = Vector2.Distance(player.position, startPoint);
                    LaunchPlayer(startPoint, distance);

                    break;

                default:
                    Debug.Log("Hit something: " + hit.collider.gameObject.name);
                    break;
            }
        }
    }

    private Vector3 WorldPoint(float x, float y)
    {
        return cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
    }

    void RotateAimLine()
    {
        Vector3 fromPosition = rotationPoint.transform.position;
        Vector3 toPosition = WorldPoint(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 diference = toPosition - fromPosition;
        float sign = (toPosition.y < fromPosition.y) ? -1.0f : 1.0f;
        float angle = Vector2.Angle(Vector2.right, diference) * sign;

        float passedAngle = ClipAngle(angle);
        currentShootAngle = passedAngle;
        rotationPoint.transform.eulerAngles = new Vector3(0, 0, passedAngle);
    }

    float ClipAngle(float f)
    {
        if (f <= 0f && f >= -90f)
        {
            return 0f;
        }
        else if (f >= -180f && f < -90f)
        {
            return 180f;
        }
        else
        {
            return f;
        }
    }

    void LaunchPlayer(Vector2 toPosition, float distanceMultiplier)
    {
        float extraDist = 3f;

        Vector3 newTo = new Vector3(toPosition.x + Mathf.Cos(currentShootAngle * Mathf.Deg2Rad) * extraDist,
            toPosition.y + Mathf.Sin(currentShootAngle * Mathf.Deg2Rad) * extraDist,
            0);
        StartCoroutine(moveObject(newTo, 0.8f));
        
    }

    bool CloseTo(Vector3 a, Vector3 b)
    {
        float minDist = 0.5f;
        float dist = Vector3.Distance(a, b);

        if (dist <= minDist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator moveObject(Vector3 toPosition, float distanceMultiplier)
    {
        float totalMovementTime = 5f * distanceMultiplier; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        while (!CloseTo(player.position, toPosition))
        {
            currentMovementTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(player.position, toPosition, currentMovementTime / totalMovementTime);
            yield return null;
        }
    }

    public void ShootBullet()
    {
        if(bulletCurrentCooldown < bulletCooldownTime)
        {
            bulletCurrentCooldown += 2f;

            GameObject bull = Instantiate(bullet, rotationPoint.transform.position, Quaternion.identity, this.transform);

            bullets.Add(bull);

            bull.GetComponent<BulletController>().AddForce(currentShootAngle);
        }
        else
        {
            Debug.Log("Wait a bietjie my bru");
        }
    }

    public void DestroyBullet(GameObject bullet)
    {
        bullets.Remove(bullet);
        Destroy(bullet.gameObject);
    }

    public void DestroyEnemy(GameObject bullet, GameObject enemy)
    {
        score += 1;
        textController.SetScore(score);

        enemySpawner.DestroyEnemy(enemy);
        Destroy(bullet.gameObject);
    }

    public int GetScore()
    {
        return score;
    }
}
