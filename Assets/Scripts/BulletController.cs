﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float currentTime = 0f;
    private float endTime = 2f;

    private CircleCollider2D collider;
    public Rigidbody2D body;

    private float power = 10f;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        //body = GetComponent<Rigidbody2D>();
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
            Debug.Log("Hit a platform");
            Destroy(this.gameObject);
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
