using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public PlatformSpawner _platformSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckControls();
    }

    void CheckControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _platformSpawner.SpawnPlatform();
        }

        #region Arrow Keys
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //PlatformSizer sizer = _platformSpawner.FirstQueue().GetComponent<PlatformSizer>();

            _platformSpawner.Shift(-1, 0);

            //sizer.Shift(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //PlatformSizer sizer = _platformSpawner.FirstQueue().GetComponent<PlatformSizer>();

            _platformSpawner.Shift(1, 0);
            //sizer.Shift(1, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //PlatformSizer sizer = _platformSpawner.FirstQueue().GetComponent<PlatformSizer>();

            _platformSpawner.Shift(0, -1);
            //sizer.Shift(0, -1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //PlatformSizer sizer = _platformSpawner.FirstQueue().GetComponent<PlatformSizer>();

            _platformSpawner.Shift(0, 1);
            //sizer.Shift(0, 1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _platformSpawner.DeleteFirst();
        }
        #endregion
    }
}
