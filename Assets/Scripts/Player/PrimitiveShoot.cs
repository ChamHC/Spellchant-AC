using System.Collections.Generic;
using UnityEngine;

public class PhraseRecognizer : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    private List<GameObject> _projectiles;

    void Start()
    {
        _projectiles = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject _projectile = Instantiate(ProjectilePrefab, transform.position + transform.forward, Quaternion.identity);
            _projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
            _projectiles.Add(_projectile);
        }

        if (_projectiles.Count > 5)
        {
            Destroy(_projectiles[0]);
            _projectiles.RemoveAt(0);
        }
    }
}
