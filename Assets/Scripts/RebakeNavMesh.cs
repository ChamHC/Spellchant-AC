using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RebakeNavMesh : MonoBehaviour
{
    [SerializeField, ReadOnly] public NavMeshSurface NavMeshSurface;
    public GameObject origin;
    public float rebuildRadius = 200f;

    private NavMeshData navMeshData;

    void Start()
    {
        NavMeshSurface = GetComponent<NavMeshSurface>();
        navMeshData = new NavMeshData();
        navMeshData = NavMeshSurface.navMeshData;
        StartCoroutine(RebuildNavMesh());
    }

    IEnumerator RebuildNavMesh()
    {
        while (true)
        {
            var sources = new List<NavMeshBuildSource>();
            var markups = new List<NavMeshBuildMarkup>();
            var bounds = new Bounds(origin.transform.position, Vector3.one * rebuildRadius); 

            NavMeshBuilder.CollectSources(bounds, NavMeshSurface.layerMask, NavMeshSurface.useGeometry, NavMeshSurface.defaultArea, markups, sources);
            NavMeshBuilder.UpdateNavMeshDataAsync(navMeshData, NavMeshSurface.GetBuildSettings(), sources, bounds);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
