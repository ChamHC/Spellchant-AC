using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Spell
{
    //Shoot Projectile That Deals Minor Damage
    [SerializeField, ReadOnly] public PlayerStateManager PSM;
    [SerializeField, ReadOnly] public SpellManager SM;
    [SerializeField, ReadOnly] public GameObject Projectile;

    [Header("Pebble Attributes")]
    public SpellType Category = SpellType.Primitive;
    public Element Element = Element.None;
    public float Damage = 5;

    public static string[] Chant = new string[] { "stone" };

    public Stone(GameObject source)
    {
        PSM = source.GetComponent<PlayerStateManager>();
        SM = source.GetComponent<SpellManager>();

        Projectile = GameObject.Instantiate(SM.StonePrefab, PSM.PlayerCam.transform.position + PSM.PlayerCam.transform.forward, Quaternion.identity);
        Projectile.transform.rotation = PSM.PlayerCam.transform.rotation;
        SM.ActiveSpells.Add(this);
    }

    public override void SpellUpdate()
    {

    }

    public override void Effect()
    {

    }

    public override void Penalty()
    {

    }

    public override void OnCollisionEnter(Collision collision)
    {

    }
}
