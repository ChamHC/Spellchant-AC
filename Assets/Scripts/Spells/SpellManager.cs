using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField, ReadOnly] public List<Spell> ActiveSpells = new List<Spell>();
    [SerializeField, ReadOnly] public PhraseRecognition PR;

    [Header("Prefabs")]
    [SerializeField] public GameObject StonePrefab;

    // Start is called before the first frame update
    void Start()
    {
        PR = GetComponent<PhraseRecognition>();


    }

    // Update is called once per frame
    void Update()
    {
        InitiateSpells();
    }

    private void InitiateSpells()
    {
        if (Stone.Chant.Contains(PR.RecognizedPhrase))
        {
            ActiveSpells.Add(new Stone(this.gameObject));
            PR.RecognizedPhrase = "";
        }
    }
}
