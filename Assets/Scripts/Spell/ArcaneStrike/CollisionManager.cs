using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneStrike
{
    public class CollisionManager : MonoBehaviour
    {
        public GameObject CollisionPrefab;
        public GameObject Parent;

        private void OnCollisionEnter(Collision other) {
            Instantiate(CollisionPrefab, transform.position, transform.rotation);
            //Debug.Log("Collided with " + other.gameObject.name);

            if (other.gameObject.layer == LayerMask.NameToLayer("Entities"))
            {
                if (other.gameObject.tag != Parent.tag)
                {
                    other.gameObject.GetComponent<EntityAttributes>().CurrentHealth -= Parent.GetComponent<SpellManager>().ArcaneStrikeSpell.damage;
                    //Debug.Log(other.gameObject.tag + " Health: " + other.gameObject.GetComponent<EntityAttributes>().CurrentHealth);
                }
            }

            //Debug.Log("Collision Detected");
            Destroy(gameObject);
        }
    }
}
