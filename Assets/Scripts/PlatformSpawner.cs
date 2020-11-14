﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject platformGap;

    private float gapBetween = 3f;

    private Queue<GameObject[]> _queuePlatforms;

    // Start is called before the first frame update
    void Start()
    {
        _queuePlatforms = new Queue<GameObject[]>();
        SpawnPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_queuePlatforms.Count == 0)
        {
            SpawnPlatform();
        }
        */
    }

    public void SpawnPlatform()
    {
        Vector3 instantiationPoint = InstantiationPoint();

        GameObject platLeft = Instantiate(platform, instantiationPoint, Quaternion.identity, this.transform);

        GameObject platMid = Instantiate(
            platformGap,
            (instantiationPoint + new Vector3(platform.transform.localScale.x - 1f, 0f, 0f)),
            Quaternion.identity,
            this.transform
        );
        
            GameObject platRight = Instantiate(
            platform, 
            (instantiationPoint + new Vector3(gapBetween + platform.transform.localScale.x, 0f, 0f)), 
            Quaternion.identity, 
            this.transform
        );

        _queuePlatforms.Enqueue((new GameObject[] { platLeft, platMid, platRight }));
    }

    Vector3 InstantiationPoint()
    {
        float randX = Random.Range(-10, 5);
        float randY = Random.Range(-3, 5);

        return new Vector3(randX, randY, 0);
    }

    public GameObject[] FirstQueue()
    {
        if (_queuePlatforms.Count > 0)
        {
            return _queuePlatforms.Peek();
        }
        else
        {
            return null;
        }
        
    }

    public void Shift(float x, float y)
    {
        GameObject[] group = FirstQueue();

        if (group != null)
        {
            foreach (GameObject platform in group)
            {
                platform.GetComponent<PlatformSizer>().Shift(x, y);
            }
        }
    }

    public void DeleteFirst()
    {
        if (_queuePlatforms.Count > 0)
        {
            GameObject[] discardedPlatforms = _queuePlatforms.Dequeue();

            foreach (GameObject discPlat in discardedPlatforms)
            {
                Destroy(discPlat);
            }
        }
    }
}
