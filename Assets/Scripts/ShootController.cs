using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Camera cam;

    private Transform player;
    public GameObject rotationPoint;

    private float currentShootAngle;
    private float transitionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        player = GetComponent<Transform>();
        currentShootAngle = 0f;
        transitionSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        RotateAimLine();
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

        /*
        RaycastHit2D hit;
        Debug.DrawRay(transform.position, , 0), Color.blue);
        if (Physics2D.Raycast(player.position, new Vector3(Mathf.Cos(currentShootAngle), Mathf.Sin(currentShootAngle), 0), out hit))
        {
            Debug.Log("Has been hit: " + hit.collider.gameObject.name);
        }
        */
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
        //player.GetComponentInParent<Rigidbody2D>().gravityScale;
        //player.position = Vector3.Lerp(player.position, toPosition, Time.deltaTime * transitionSpeed * distanceMultiplier);

        //float step = transitionSpeed * Time.deltaTime;
        //player.position = Vector3.MoveTowards(player.position, toPosition, step);


        //player.position = toPosition;

        /*
        bool notThereYet = true;
        while (notThereYet)
        {
            float step = transitionSpeed * Time.deltaTime;
            player.position = Vector3.MoveTowards(player.position, toPosition, step);

            if (CloseTo(player.position, toPosition))
            {
                notThereYet = false;
            }
        }
        */

        //StartCoroutine(MoveCoroutine(toPosition));
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

    IEnumerator MoveCoroutine(Vector3 toPosition)
    {
        while (!CloseTo(player.position, toPosition))
        {
            Vector3.Lerp(player.position, toPosition, transitionSpeed);
            yield return new WaitForEndOfFrame();
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
}
