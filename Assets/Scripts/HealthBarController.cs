using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public Transform bar;
    public Transform barSprite;

    // Start is called before the first frame update
    void Start()
    {
        ScaleBar(100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleBar(float health)
    {
        float val = health / 100.0f;
        bar.localScale = new Vector3(val, 1, 1);
    }

}
