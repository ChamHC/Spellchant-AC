using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritSiphon : Spell
{
    // Gradually Siphon Enemies Health In An Area.
    // Player Have Reduced Move Speed During Skill Duration.
    public SpellType Category = SpellType.Cursed;
    public Element Element = Element.None;
    public float Duration = 5.0f;
    public float Interval = 0.5f;
    public float Damage = 5f;
    public float Range = 10f;
    public float SlowMultiplier = 0.5f;

    public SpiritSiphon()
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
