using ArcaneStrike;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField, ReadOnly] public Transform Origin;

    [SerializeField] public SpellProperty ArcaneStrikeSpell;

    void Start()
    {
        if (GetComponentInChildren<Camera>() != null)
            Origin = GetComponentInChildren<Camera>().transform;
        else
            Origin = transform;
    }

    void Update()
    {
        ArcaneStrikeBehaviour();
    }

    #region Arcane Strike
    public void ArcaneStrike()
    {
        GameObject init = Instantiate(ArcaneStrikeSpell.InitPrefab, Origin.position + Origin.forward, Origin.rotation);
        init.transform.parent = gameObject.transform;

        GameObject projectile = Instantiate(ArcaneStrikeSpell.ProjectilePrefab, transform.position, Origin.rotation);
        projectile.GetComponentInChildren<ArcaneStrike.CollisionManager>().CollisionPrefab = ArcaneStrikeSpell.CollisionPrefab;
        projectile.tag = "Arcane Strike";
        projectile.transform.parent = gameObject.transform;
        projectile.GetComponentInChildren<CollisionManager>().Parent= gameObject;
        Debug.Log("Tag Check: " + projectile.GetComponentInChildren<CollisionManager>().Parent.tag);
        StartCoroutine(ArcaneStrikeDelayedBehaviour(projectile));
    }
    private IEnumerator ArcaneStrikeDelayedBehaviour(GameObject projectile)
    {
        yield return new WaitForSeconds(0.2f);

        if (projectile != null)
            projectile.transform.parent = null;
    }
    private void ArcaneStrikeBehaviour()
    {
        GameObject[] arcaneStrikeObjects = GameObject.FindGameObjectsWithTag("Arcane Strike");
        if (arcaneStrikeObjects.Length <= 0) return;

        foreach (GameObject projectile in arcaneStrikeObjects)
        {
            projectile.transform.position += projectile.transform.forward * Time.deltaTime * 10f;

        }
    }
    #endregion
}
