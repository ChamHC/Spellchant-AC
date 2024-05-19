using ArcaneStrike;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelDestruction;

public class SpellManager : MonoBehaviour
{
    [SerializeField] public Transform Origin;
    [SerializeField] public SpellProperty ArcaneStrikeSpell;

    void Start()
    {
        Origin = transform;
    }

    void Update()
    {
        ArcaneStrikeBehaviour();
    }

    #region Ready
    public void Ready()
    {
        if (Origin.gameObject != null){
            GameObject player = Origin.gameObject;
            if (player.transform.position.y < 0)
            {
                FindObjectOfType<LevelManager>().PlayerIsReady = true;
            }
            VoxelObject[] VoxelObjects = FindObjectsOfType<VoxelObject>();;
            foreach (VoxelObject voxelObject in VoxelObjects)
            {
                voxelObject.ResetModel(true);
            }
        }
    }
    #endregion

    #region Arcane Strike
    public void ArcaneStrike()
    {
        GameObject init;
        GameObject projectile;
        if (transform.gameObject.GetComponentInChildren<Camera>() != null){
            Transform camera = transform.gameObject.GetComponentInChildren<Camera>().transform;
            init = Instantiate(ArcaneStrikeSpell.InitPrefab, camera.position + camera.forward, camera.rotation);
            init.transform.parent = gameObject.transform;

            projectile = Instantiate(ArcaneStrikeSpell.ProjectilePrefab, camera.position, camera.rotation);
            projectile.GetComponent<CollisionManager>().CollisionPrefab = ArcaneStrikeSpell.CollisionPrefab;
            projectile.tag = "Arcane Strike";
            projectile.transform.parent = camera;
            projectile.GetComponent<CollisionManager>().Parent= gameObject;
        }
        else{
            init = Instantiate(ArcaneStrikeSpell.InitPrefab, Origin.position + Origin.forward, Origin.rotation);
            init.transform.parent = gameObject.transform;

            projectile = Instantiate(ArcaneStrikeSpell.ProjectilePrefab, transform.position, Origin.rotation);
            projectile.GetComponent<CollisionManager>().CollisionPrefab = ArcaneStrikeSpell.CollisionPrefab;
            projectile.tag = "Arcane Strike";
            projectile.transform.parent = gameObject.transform;
            projectile.GetComponent<CollisionManager>().Parent= gameObject;
        }
        Physics.IgnoreCollision(projectile.GetComponentInChildren<Collider>(), Origin.gameObject.GetComponent<Collider>());
        projectile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        StartCoroutine(ArcaneStrikeDelayedBehaviour(projectile));
    }
    private IEnumerator ArcaneStrikeDelayedBehaviour(GameObject projectile)
    {
        yield return new WaitForSeconds(0.1f);

        if (projectile != null)
            projectile.transform.parent = null;
    }
    private void ArcaneStrikeBehaviour()
    {
        List<GameObject> arcaneStrikeObjects = GetArcaneStrikeObjects();
        if (arcaneStrikeObjects.Count <= 0) return;

        foreach (GameObject projectile in arcaneStrikeObjects)
        {
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 500f, ForceMode.Impulse);
        }
    }

    private List<GameObject> GetArcaneStrikeObjects()
    {
        List<GameObject> arcaneStrikeObjects = new List<GameObject>();

        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("Arcane Strike");
        foreach (GameObject gameObject in gameObjectsWithTag)
        {
            CollisionManager collisionManager = gameObject.GetComponent<CollisionManager>();
            if (collisionManager != null && collisionManager.Parent == this.gameObject)
            {
                arcaneStrikeObjects.Add(gameObject);
            }
        }

        return arcaneStrikeObjects;
    }
    #endregion
}
