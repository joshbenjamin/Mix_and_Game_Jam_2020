using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public PlatformSpawner _platformSpawner;
    private ShootController shootController;
    private HealthController healthController;

    public Transform player;

    private float movementMultiple = 4f;

    private float jumpPower = 0f;
    private float maxJumpPower = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();

        shootController = GetComponent<ShootController>();
        healthController = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckControls();
        Movement();
        Shake();
    }

    void CheckControls()
    {
        #region Platform Control
        #region Creation/Deletion
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            _platformSpawner.SpawnPlatform();
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            _platformSpawner.DeleteFirst();
        }
        #endregion

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
        #endregion

        #region Mouse
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mouseClickPosition = Input.mousePosition;

            //shootController.DebugDrawLine(player.position, mouseClickPosition);
            //shootController.DrawRaycast();
        }
        if (Input.GetMouseButton(1))
        {
            jumpPower += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(1))
        {
            jumpPower = Mathf.Min(jumpPower, maxJumpPower);
            shootController.Jump(jumpPower);
            jumpPower = 0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            shootController.ShootBullet();
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.P))
        {
            healthController.TakeDamage(10);
        }
    }

    void Movement()
    {
        float xMovement = Input.GetAxis("Horizontal");

        player.localPosition = new Vector3((player.localPosition.x + (xMovement * movementMultiple * Time.deltaTime)), player.localPosition.y, 0);
    }

    void Shake()
    {

    }
}
