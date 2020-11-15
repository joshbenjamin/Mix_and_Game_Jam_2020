using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    public TMP_Text text;

    private int ourGoals = 0;
    private int theirGoals = 0;

    private void Start()
    {
        SetScore();
    }

    public void WeScore()
    {
        ourGoals += 1;
        SetScore();
    }

    public void TheyScore()
    {
        theirGoals += 1;
        SetScore();
    }

    public void SetScore()
    {
        text.text = ourGoals + " - " + theirGoals;
    }

    public string GetScore()
    {
        return text.text;
    }

    public void Win()
    {
        text.text = "YOU WIN MOTHERFUCKER";
    }

    public void JustCall()
    {

    }
}
