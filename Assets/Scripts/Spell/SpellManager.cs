using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField, ReadOnly] public Transform Origin;

    [SerializeField] public SpellProperty ArcaneStrikeSpell;

    void Start()
    {
        Origin = GetComponentInChildren<Camera>().transform;
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

        GameObject projectile = Instantiate(ArcaneStrikeSpell.ProjectilePrefab, Origin.position, Origin.rotation);
        projectile.GetComponentInChildren<ArcaneStrike.CollisionManager>().CollisionPrefab = ArcaneStrikeSpell.CollisionPrefab;
        projectile.tag = "Arcane Strike (Friendly)";
        projectile.transform.parent = gameObject.transform;
        StartCoroutine(ArcaneStrikeDelayedBehaviour(projectile));
    }
    private IEnumerator ArcaneStrikeDelayedBehaviour(GameObject projectile)
    {
        yield return new WaitForSeconds(0.2f);
        projectile.transform.parent = null;
    }
    private void ArcaneStrikeBehaviour()
    {
        GameObject[] arcaneStrikeObjects = GameObject.FindGameObjectsWithTag("Arcane Strike (Friendly)");
        if (arcaneStrikeObjects.Length <= 0) return;

        foreach (GameObject projectile in arcaneStrikeObjects)
        {
            projectile.transform.position += projectile.transform.forward * Time.deltaTime * 10f;

        }
    }
    #endregion
}
