using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Spawnpoints;
    public List<GameObject> EnemyPrefabs;
    public float SpawnRate = 5.0f;
    public int SpawnLimit = 5;

    [SerializeField, ReadOnly] public float _spawnTimer;

    void Start()
    {
        
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        //if(LevelOne)
        LevelOneSpawner();
    }

    void LevelOneSpawner()
    {
        if(_spawnTimer >= SpawnRate)
        {
            _spawnTimer = 0.0f;
            foreach(GameObject spawnpoint in Spawnpoints)
            {
                int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

                if (enemyCount < SpawnLimit)
                {
                    int randomEnemy = Random.Range(0, EnemyPrefabs.Count);
                    GameObject enemy = Instantiate(EnemyPrefabs[randomEnemy], spawnpoint.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
