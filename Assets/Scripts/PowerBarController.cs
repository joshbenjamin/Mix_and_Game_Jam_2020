using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarController : MonoBehaviour
{
    public Transform bar;
    public Transform barSprite;

    private Color barColorRed;
    private Color barColorRedFlash;
    private Color activeColor;

    private float currentTime = 0f;
    private float changeColorTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        barColorRed = Color.red;
        barColorRedFlash = Color.white;
        barColorRedFlash.a = 0.5f;
        activeColor = barColorRed;
    }

    // Update is called once per frame
    void Update()
    {
        barSprite.GetComponent<SpriteRenderer>().color = activeColor;

        currentTime += Time.deltaTime;
    }

    public void ScaleBar(float val, float outOf)
    {
        float result = val / outOf;
        if (result > 1f)
        {
            result = 1f;
        }

        if(result >= 0.8f)
        {
            FlashBar();
        }

        if (result < 0.8f)
        {
            activeColor = barColorRed;
        }

        bar.localScale = new Vector3(result, 1, 1);
    }

    void FlashBar()
    {
        if(Mathf.Round(currentTime * 100) % 2 == 0)
        {
            activeColor = barColorRedFlash;
        }
        else
        {
            activeColor = barColorRed;
        }

        /*
        if (activeColor == barColorRed)
        {
            activeColor = barColorRedFlash;
        }
        else
        {
            activeColor = barColorRed;
        }
        */
    }
}
