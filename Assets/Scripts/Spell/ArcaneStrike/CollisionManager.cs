using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneStrike
{
    public class CollisionManager : MonoBehaviour
    {
        public GameObject CollisionPrefab;
        public GameObject Parent;

        private void OnTriggerEnter(Collider other)
        {
            Instantiate(CollisionPrefab, transform.position, transform.rotation);
            //Debug.Log("Collided with " + other.gameObject.name);

            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                Destroy(transform.parent.gameObject);
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("Entities"))
            {
                if (other.gameObject.tag != Parent.tag)
                {
                    other.gameObject.GetComponent<EntityAttributes>().CurrentHealth -= Parent.GetComponent<SpellManager>().ArcaneStrikeSpell.damage;
                    //Debug.Log(other.gameObject.tag + " Health: " + other.gameObject.GetComponent<EntityAttributes>().CurrentHealth);
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }
}
