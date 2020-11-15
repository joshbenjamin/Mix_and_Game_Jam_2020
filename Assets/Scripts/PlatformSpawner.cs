using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformGroup;

    public GameObject platform;
    public GameObject platformGap;

    private float gapBetween = 3f;

    private Queue<GameObject[]> _queuePlatforms;
    private List<GameObject> platforms;

    private List<GameObject> alreadyPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        _queuePlatforms = new Queue<GameObject[]>();
        platforms = new List<GameObject>();
        //SpawnPlatform(-2);

        alreadyPlatforms = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlatformGroup"));
        SortList();
    }

    void SortList()
    {
        for (int i = 0; i < alreadyPlatforms.Count-1; i++)
        {
            for (int j = 0; j < alreadyPlatforms.Count-1; j++)
            {
                if (alreadyPlatforms[j].transform.position.y > alreadyPlatforms[j + 1].transform.position.y)
                {
                    GameObject temp = alreadyPlatforms[j];
                    alreadyPlatforms[j] = alreadyPlatforms[j + 1];
                    alreadyPlatforms[j + 1] = temp;
                }
            }
        }
    }

    public Vector2 FindNext(GameObject through)
    {
        return new Vector2();
        //alreadyPlatforms.Find(through).pos;
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

    public void TriggerSpawn(Vector3 oldPos)
    {
        SpawnPlatform(oldPos.y);
    }

    public void SpawnPlatform(float minY)
    {
        Vector3 instantiationPoint = InstantiationPoint(minY);

        GameObject plat = Instantiate(platformGroup, instantiationPoint, Quaternion.identity, this.transform);
        platforms.Add(plat);

        /*

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

        */
    }

    Vector3 InstantiationPoint(float baseY)
    {
        float randX = Random.Range(-10, 5);
        float randY = Random.Range(baseY + -2, baseY + 3);

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
