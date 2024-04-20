using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Level;
    public int Wave;

    public LevelSetup[] LevelSetups;
    public GameObject[] Spawnpoints;

    private bool isSpawning = false;

    void Start()
    {
        Level = 0;
        Wave = 3;
        NextLevel();
    }

    void Update()
    {
        NextLevel();
        NextWave();
    }

    void NextWave()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !isSpawning)
        {
            Wave++;
            Spawn();
            Debug.Log("Next Wave.");
        }
    }

    void NextLevel()
    {
        if (Wave == 3 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            Level++;
            Wave = 1;
            Spawn();
            Debug.Log("Next Level.");
        }
    }

    void Spawn()
    {
        isSpawning = true;
        foreach (var levelSetup in LevelSetups)
        {
            if (levelSetup.Level == Level)
            {
                foreach (var waveSetup in levelSetup.WaveSetup)
                {
                    if (waveSetup.Wave == Wave)
                    {
                        foreach (var squad in waveSetup.SquadSetup)
                        {
                            var spawnpoint = Spawnpoints[Random.Range(0, Spawnpoints.Length)];
                            foreach (var enemy in squad.EnemyPrefabs)
                            {
                                Instantiate(enemy, spawnpoint.transform.position, Quaternion.identity);
                            }
                        }
                    }
                }
            }
        }
        isSpawning = false;
    }
}
