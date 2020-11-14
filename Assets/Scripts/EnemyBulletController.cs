﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public Rigidbody2D body;
    private float damage = 20f;
    private float currentTime = 0f;
    private float timeToDie = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToDie)
        {
            Die();
        }
    }

    public void AddForce(Vector2 force)
    {
        body.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthController>().TakeDamage(damage);
        }

        Die();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
