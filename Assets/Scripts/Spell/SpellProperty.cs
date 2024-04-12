using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Spell Property")]
public class SpellProperty : ScriptableObject
{
    public GameObject InitPrefab;
    public GameObject ProjectilePrefab;
    public GameObject CollisionPrefab;

    [Header("Spell Attributes")]
    public float damage;
}
