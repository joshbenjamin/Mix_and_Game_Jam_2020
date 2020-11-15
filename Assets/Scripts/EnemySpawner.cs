using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Camera cam;

    public GameObject enemy;
    private List<GameObject> enemies;

    private AudioSource dieSound;

    //private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        enemies = new List<GameObject>();
        dieSound = GetComponent<AudioSource>();

        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPoint();

        GameObject ene = Instantiate(enemy, spawnPosition, Quaternion.identity, this.transform);
        enemies.Add(ene);
    }

    Vector3 GetSpawnPoint()
    {
        return new Vector3(0, -2, 0);
    }

    public void DestroyEnemy(GameObject enemy)
    {
        dieSound.Play();
        enemies.Remove(enemy);
        Destroy(enemy);
    }
}
