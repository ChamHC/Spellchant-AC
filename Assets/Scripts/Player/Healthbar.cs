using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] public EntityAttributes PlayerAttributes;
    [SerializeField] public Slider HealthFill;

    void Start()
    {
        HealthFill = GetComponent<Slider>();
        PlayerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityAttributes>();
    }

    private void Update()
    {
        if (HealthFill != null && PlayerAttributes != null)
        {
            HealthFill.value = PlayerAttributes.CurrentHealth / PlayerAttributes.MaxHealth;
        }
    }
}
