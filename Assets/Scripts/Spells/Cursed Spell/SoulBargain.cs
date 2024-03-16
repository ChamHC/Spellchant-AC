using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBargain : Spell
{
    // Summon A Demon That Fights For The Player. 
    // Player Takes 10% Max Health Damage On Summon And 1% Max Health Damage Per Second.
    public SpellType Category = SpellType.Cursed;
    public Element Element = Element.None;
    public float InitialCost = 10.0f;
    public float Interval = 1.0f;
    public float Cost = 1.0f;

    public float SummonHealth = 500.0f;
    public float SummonDamage = 100.0f;
    public float SummonAttackSpeed = 1.0f;
    public float SummonMoveSpeed = 7.0f;

    public SoulBargain()
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
