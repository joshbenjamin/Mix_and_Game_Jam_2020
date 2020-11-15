using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float currentTime = 0f;
    private float endTime = 2f;

    private CircleCollider2D collider;
    public Rigidbody2D body;

    private float power = 10f;

    private ShootController shootController;
    //public EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        //body = GetComponent<Rigidbody2D>();

        shootController = GetComponentInParent<ShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        ShouldDie();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            shootController.DestroyBullet(this.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            shootController.DestroyEnemy(this.gameObject, collision.gameObject);
            //Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "EnemyBullet")
        {
            shootController.DestroyBullet(this.gameObject);
            collision.gameObject.GetComponent<EnemyBulletController>().Die();
        }
        else if(collision.gameObject.tag == "Ball")
        {
            shootController.Win();
            Debug.Log("You win");
        }
    }

    public void AddForce(float angle)
    {
        body.AddForce(new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * power, ForceMode2D.Impulse);
    }

    private void ShouldDie()
    {
        if(currentTime >= endTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
