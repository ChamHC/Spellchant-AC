using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [Header("Basic Attributes")]
    [SerializeField] public float CurrentHealth = 100f;
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float CurrentMana = 100f;
    [SerializeField] public float MaxMana = 100f;

    [Header("Unlocked Spells")]
    [SerializeField] public List<Spell> NoviceSpell;
    [SerializeField] public List<Spell> AdeptSpell;
    [SerializeField] public List<Spell> MasterSpell;
    [SerializeField] public List<Spell> CursedSpell;
    [SerializeField] public List<Spell> ArcaneSpell;

    [Header("Empowerment")]
    [SerializeField] public int VitalityInfusion = 0;
    [SerializeField] public int ManaOverflow = 0;
    [SerializeField] public int ArcanePotency = 0;
    [SerializeField] public int AgileStride = 0;
    [SerializeField] public int EssenceRejuvenation = 0;

    [Header("Malefaction")]
    [SerializeField] public int VampiricHunger = 0;
    [SerializeField] public int ManaDrain = 0;
    [SerializeField] public int FragileForm = 0;
    [SerializeField] public int BerserkerFury = 0;
    [SerializeField] public int ChaoticEssence = 0;

    [Header("Blessing")]
    [SerializeField] public int PhoenixRebirth = 0;
    [SerializeField] public int ElementalFusion = 0;
    [SerializeField] public int RealityWarp = 0;
    [SerializeField] public int CelestialIntervention = 0;
    [SerializeField] public int ShadowCloak = 0;

    [Header("Trials")]
    [Tooltip ("Complete the level within n seconds")]
    [SerializeField] public bool TrialOfFlowingTime = false;
    [Tooltip ("Complete the level without taking damage")]
    [SerializeField] public bool TrialOfUnscathedDefense = false;
    [Tooltip ("Complete the level without destoying n % of the environment")]
    [SerializeField] public bool TrialOfUnwaveringOrder = false;
    [Tooltip("Complete the level while destoying > n % of the environment")]
    [SerializeField] public bool TrialOfCataclysmicDestruction = false;
    [Tooltip ("Complete the level with n radius of vision")]
    [SerializeField] public bool TrialOfObscuredDarkness = false;
    [Tooltip ("Complete the level without using any healing spells")]
    [SerializeField] public bool TrialOfEnduringResolve = false;
    [Tooltip ("Complete the level without shooting any weakspots")]
    [SerializeField] public bool TrialOfCriticalRestraint = false;
    [Tooltip ("Defeat n enemies with a single spell")]
    [SerializeField] public bool TrialOfRuthlessAnnihilation = false;
    [Tooltip ("Defeat n enemies in air")]
    [SerializeField] public bool TrialOfSkyborneDominance = false;
    [Tooltip ("Defeat n enemies with weakspots")]
    [SerializeField] public bool TrialOfPreciseExecution = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
