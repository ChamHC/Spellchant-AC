using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField, ReadOnly] public PlayerStateManager PlayerStateManager;

    [Header("Basic Attributes")]
    [SerializeField] public float CurrentHealth = 100f;
    [SerializeField] public float MaxHealth = 100f;
    [SerializeField] public float CurrentMana = 100f;
    [SerializeField] public float MaxMana = 100f;

    void Start()
    {
        PlayerStateManager = GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile Test")
        {
            CurrentHealth -= 10f;
        }
    }
}
