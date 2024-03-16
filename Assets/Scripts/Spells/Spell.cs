using UnityEngine;

public abstract class Spell
{
    public abstract void SpellUpdate();

    public abstract void Effect();

    public abstract void Penalty();

    public abstract void OnCollisionEnter(Collision collision);
}

public enum SpellType
{
    Primitive, Novice, Adept, Master, Cursed, Arcane
}

public enum Element
{
    Water, Fire, Earth, None
}

#region Status Effects
#region Drown
// Deal Massive Damage When Stack Reaches 5
#endregion
#region Burn
// Deal Damage Over Time, Can Stack Up To 5
#endregion
#region Stun
// Stun The Enemy For A Short Duration, Does Not Stack
#endregion
#endregion
