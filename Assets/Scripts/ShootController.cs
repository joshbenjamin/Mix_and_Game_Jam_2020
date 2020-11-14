using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DebugDrawLine(Vector3 fromPosition, Vector2 mouseClickPosition)
    {
        Debug.Log("Shooting from position: " + fromPosition);
        Debug.Log("Mouse position: " + mouseClickPosition);

        Vector3 point = cam.ScreenToWorldPoint(new Vector3(mouseClickPosition.x, mouseClickPosition.y, cam.nearClipPlane));
        Debug.Log("New mouse pos: " + point);

        Debug.DrawLine(fromPosition, point, Color.white, 2.5f);
    }
}
