using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizer : MonoBehaviour
{
    public Transform triangle;
    public Rigidbody2D body;



    // Start is called before the first frame update
    void Start()
    {
        triangle.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float vertVelocity = body.velocity.y;

        if (vertVelocity == 0.0f)
        {
            triangle.localScale = new Vector3(1, 1, 1);
        }
        else if (vertVelocity < 0.0f)
        {
            triangle.localScale = new Vector3(1, Mathf.Max(0.5f, 1 + ScaleChange(vertVelocity)), 1);
        }
        else if (vertVelocity > 0.0f)
        {
            triangle.localScale = new Vector3(1, 1 + ScaleChange(vertVelocity), 1);
        }
        //Debug.Log("Velocity: " + body.velocity);
    }

    float ScaleChange(float velocity)
    {
        float ret = velocity / 20f;
        return Mathf.Min(velocity / 20f, 0.9f);
    }
}
