using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodlust : Spell
{
    // Boost Player Damage During Skill Duration.
    // Player Takes 200% Damage During Skill Duration.
    public SpellType Category = SpellType.Cursed;
    public Element Element = Element.None;
    public float BoostMultiplier = 2.0f;
    public float Duration = 15.0f;
    public float ExtraDamageTaken = 2.0f;

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
