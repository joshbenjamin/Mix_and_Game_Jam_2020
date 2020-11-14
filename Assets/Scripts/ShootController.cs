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
    }

    private Vector3 WorldPoint(float x, float y)
    {
        return cam.ScreenToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
    }

    void RotateAimLine()
    {
        Vector3 fromPosition = player.position;
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
