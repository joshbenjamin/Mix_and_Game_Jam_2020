using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text text;

    public void SetScore(int score)
    {
        text.text = score+"";
    }
}
