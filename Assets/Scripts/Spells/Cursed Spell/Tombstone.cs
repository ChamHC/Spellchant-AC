using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : Spell
{
    // Summon Tombstone That Summons Weak Skeletons.
    // Player Takes 20% Max Health Damage.
    public SpellType Category = SpellType.Cursed;
    public Element Element = Element.None;
    public float Cost = 20.0f;
    public float Duration = 10.0f;
    public float Interval = 1f;

    public float SummonHP = 20f;
    public float SummonDamage = 10f;
    public float SummonSpeed = 10f;
    public float SummonDuration = 30f;

    public override void SpellStart()
    {

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
