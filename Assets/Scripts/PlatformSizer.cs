using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSizer : MonoBehaviour
{
    private Camera _cam;

    private Transform shapeRect;
    private Vector3 currentScale;
    private Vector3 currentPos;

    private float xScaleMove = 0.2f;
    private float yScaleMove = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

        shapeRect = GetComponent<Transform>();
        currentScale = shapeRect.localScale;
        currentPos = shapeRect.localPosition;

        Debug.Log("I'm going back to " + CameraWidth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SizeWidth()
    {
        float randomShift = Random.Range(-3, 3);
    }

    float CameraWidth()
    {
        return _cam.scaledPixelWidth;
    }

    public void Shift(float x, float y)
    {
        Vector3 newPos = new Vector3((currentPos.x + (x * xScaleMove)), (currentPos.y + (y * yScaleMove)), 0f);

        shapeRect.transform.localPosition = newPos;
        currentPos = newPos;
    }
}
