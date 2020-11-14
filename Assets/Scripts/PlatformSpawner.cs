using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;

    private float gapBetween = 3f;

    private Queue<GameObject[]> _queuePlatforms;

    // Start is called before the first frame update
    void Start()
    {
        _queuePlatforms = new Queue<GameObject[]>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlatform()
    {
        Vector3 instantiationPoint = InstantiationPoint();

        GameObject platLeft = Instantiate(platform, instantiationPoint, Quaternion.identity, this.transform);
        GameObject platRight = Instantiate(
            platform, 
            (instantiationPoint + new Vector3(gapBetween, 0f, 0f) + new Vector3(platform.transform.localScale.x, 0, 0)), 
            Quaternion.identity, 
            this.transform
        );

        _queuePlatforms.Enqueue((new GameObject[] { platLeft, platRight }));
    }

    Vector3 InstantiationPoint()
    {
        return new Vector3(-3, 0, 0);
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
        foreach (GameObject platform in FirstQueue())
        {
            platform.GetComponent<PlatformSizer>().Shift(x, y);
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
