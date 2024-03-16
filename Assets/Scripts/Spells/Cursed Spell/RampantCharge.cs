using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampantCharge : Spell
{
    // Charge Forward, Invulnerable And Knockback Enemies.
    // Player Cannot Attack During Skill Duration And Take 10% Max Health Damage After Skill Ends.
    public SpellType Category = SpellType.Cursed;
    public Element Element = Element.None;
    public float Cost = 10.0f;
    public float Duration = 5.0f;

    public RampantCharge()
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
