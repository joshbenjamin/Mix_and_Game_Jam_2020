using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public PlatformSpawner _platformSpawner;

    public Transform player;

    private float movementMultiple = 4f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckControls();
        Movement();
    }

    void CheckControls()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
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
        #endregion

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            _platformSpawner.DeleteFirst();
        }
        
    }

    void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal");

        player.localPosition = new Vector3((player.localPosition.x + (xMovement * movementMultiple * Time.deltaTime)), player.localPosition.y, 0);
    }
}
