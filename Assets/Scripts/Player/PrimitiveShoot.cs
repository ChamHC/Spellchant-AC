using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrimitiveShoot : MonoBehaviour
{
    public Camera PlayerCam;
    public GameObject ProjectilePrefab;

    [Header("Hidden Attributes")]
    [SerializeField, ReadOnly] private List<GameObject> _projectiles;

    void Start()
    {
        PlayerCam = GetComponentInChildren<Camera>();
        _projectiles = new List<GameObject>();
    }

    void Update()
    {
        DebugShoot();
        /*
        if (_projectiles.Count > 5)
        {
            int _extras = _projectiles.Count - 5;
            for (int i = 0; i < _extras; i++)
            {
                _projectiles[i].transform.localScale -= new Vector3(0.15f, 0.15f, 0.15f) * Time.deltaTime;
                if (_projectiles[i].transform.localScale.x < 0f)
                {
                    Destroy(_projectiles[i]);
                    _projectiles.RemoveAt(i);
                }
            }
        }
        */
    }

    public void Shoot()
    {
        GameObject _projectile = Instantiate(ProjectilePrefab, PlayerCam.transform.position + PlayerCam.transform.forward, Quaternion.identity);
        _projectile.transform.rotation = PlayerCam.transform.rotation;
        //_projectile.GetComponent<Rigidbody>().AddForce(PlayerCam.transform.forward * 20, ForceMode.Impulse);
        _projectiles.Add(_projectile);
    }

    public void DebugShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}
