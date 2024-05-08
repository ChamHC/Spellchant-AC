using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "Enemy/Level")]
public class LevelSetup : ScriptableObject
{
    [SerializeField] public int Level;
    [SerializeField] public WaveSetup[] WaveSetup;
}
