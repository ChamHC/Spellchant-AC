using AllIn1VfxToolkit.Demo.Scripts;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class PrimitiveShoot : MonoBehaviour
{
    [Header("Hidden Attributes")]
    [SerializeField, ReadOnly] public Camera PlayerCam;
    [SerializeField, ReadOnly] public SpellManager SpellManager;

    void Start()
    {
        PlayerCam = GetComponentInChildren<Camera>();
        SpellManager = GetComponent<SpellManager>();
    }

    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootDetail();
        }
    }

    public void ShootDetail()
    {
        SpellManager.ArcaneStrike();
    }
}
