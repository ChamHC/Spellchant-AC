using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RebakeNavMesh : MonoBehaviour
{
    public NavMeshSurface NavMeshSurface;

    private void Start()
    {
        NavMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void Rebake()
    {
        NavMeshSurface.BuildNavMesh();

        RecalculatePath();
    }

    private void RecalculatePath()
    {
        List<EnemyStateManager> enemies = new List<EnemyStateManager>(FindObjectsOfType<EnemyStateManager>());
        foreach (EnemyStateManager enemy in enemies)
        {
            enemy.NavMeshAgent.SetDestination(enemy.Target.position);
        }
    }
}