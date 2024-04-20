using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    [SerializeField, ReadOnly] public EntityAttributes PlayerAttributes;
    [SerializeField, ReadOnly] public Slider ManaFill;

    void Start()
    {
        ManaFill = GetComponent<Slider>();
        PlayerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityAttributes>();
    }

    private void Update()
    {
        if (ManaFill != null && PlayerAttributes != null)
        {
            ManaFill.value = PlayerAttributes.CurrentMana / PlayerAttributes.MaxMana;
        }
    }
}