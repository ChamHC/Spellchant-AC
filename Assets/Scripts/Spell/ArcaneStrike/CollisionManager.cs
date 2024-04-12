using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneStrike
{
    public class CollisionManager : MonoBehaviour
    {
        public GameObject CollisionPrefab;

        private void OnTriggerEnter(Collider other)
        {
            Instantiate(CollisionPrefab, transform.position, transform.rotation);
            Debug.Log("Collided");

            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
