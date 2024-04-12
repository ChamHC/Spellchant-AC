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
            Debug.Log("Collided with " + other.gameObject.name);

            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                Destroy(transform.parent.gameObject);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Entities"))
            {
                if (other.gameObject.tag == "Enemy")
                {
                    other.gameObject.GetComponent<EnemyAttributes>().CurrentHealth -= GameObject.Find("Player").GetComponent<SpellManager>().ArcaneStrikeSpell.damage;
                    Debug.Log("Enemy Health: " + other.gameObject.GetComponent<EnemyAttributes>().CurrentHealth);
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
