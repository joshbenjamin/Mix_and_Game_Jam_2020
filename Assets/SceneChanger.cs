using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Name of scene");
        SceneManager.LoadScene(name);
    }
    public void Exit()
    {
        Application.Quit();
    }
}