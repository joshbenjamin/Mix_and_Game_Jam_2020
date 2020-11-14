using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Camera cam;

    private Transform player;
    public GameObject rotationPoint;

    private float currentShootAngle;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        player = GetComponent<Transform>();
        currentShootAngle = 0f;
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
}
