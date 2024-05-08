using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int CurrentLevel;
    [SerializeField] public int CurrentWave;

    public LevelSetup[] LevelSetups;
    [SerializeField] public GameObject[] Spawnpoints;
    [SerializeField] private LevelSetup _currentLevel;
    public bool PlayerIsReady = true;
    private bool _hasSpawned = false;
    private bool _waveFound = true;
    private GameObject _player;

    void Start()
    {
        Spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        _player = GameObject.FindGameObjectWithTag("Player");

        CurrentLevel = 1;
        CurrentWave = 1;

        GetLevelData();
    }

    void Update()
    {
        if (!PlayerIsReady && _player.transform.position.y > -10f)
            _player.transform.position = new Vector3(0, -50f, 0);
        else if (PlayerIsReady && _player.transform.position.y < -10)
            _player.transform.position = new Vector3(0, 0, 0);

        if (!PlayerIsReady) return;

        WaveHandler();

        if (IsWaveFinished())
        {
            CurrentWave++;
            _hasSpawned = false;
        }

        if (!_waveFound)
            LevelHandler();
    }

    void LevelHandler()
    {
        Debug.Log("Finished level " + CurrentLevel);
        PlayerIsReady = false;
        CurrentLevel++;
        CurrentWave = 1;
        GetLevelData();
    }

    void WaveHandler()
    {
        if (_hasSpawned || _currentLevel.Level != CurrentLevel) return;
        _waveFound = false;
        for (int i = 0; i < _currentLevel.WaveSetup.Length; i++)
        {
            if (_currentLevel.WaveSetup[i].Wave == CurrentWave)
            {
                Spawn(_currentLevel.WaveSetup[i]);
                _waveFound = true;
            }
        }
        _hasSpawned = true;
    }

    void Spawn(WaveSetup waveData)
    {
        for (int i = 0; i < waveData.SquadSetup.Length; i++)
        {
            GameObject randomSpawnpoint = Spawnpoints[Random.Range(0, Spawnpoints.Length)];
            for (int j = 0; j < waveData.SquadSetup[i].EnemyPrefabs.Count; j++)
            {
                Instantiate(waveData.SquadSetup[i].EnemyPrefabs[j], randomSpawnpoint.transform.position, Quaternion.identity);
            }
        }

        Debug.Log("Finished spawning wave " + CurrentWave);
    }

    void GetLevelData()
    {
        foreach (LevelSetup levelSetup in LevelSetups)
        {
            if (levelSetup.Level == CurrentLevel)
            {
                _currentLevel = levelSetup;
                break;
            }
        }
    }

    bool IsWaveFinished()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && _hasSpawned;
    }
}
