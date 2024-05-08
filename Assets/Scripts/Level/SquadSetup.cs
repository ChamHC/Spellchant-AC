using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySquad", menuName = "Enemy/Squad")]
public class SquadSetup : ScriptableObject
{
    [SerializeField] public List<GameObject> EnemyPrefabs;
}
