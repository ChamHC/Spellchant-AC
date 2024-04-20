using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttributes : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth = 100f;
    public float MaxMana = 100f;
    public float CurrentMana = 100f;
    private void Start()
    {
        
    }

    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
