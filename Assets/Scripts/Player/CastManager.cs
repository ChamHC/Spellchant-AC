using AllIn1VfxToolkit.Demo.Scripts;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class CastManager : MonoBehaviour
{
    [Header("Hidden Attributes")]
    [SerializeField] public Camera PlayerCam;
    [SerializeField] public SpellManager SpellManager;
    [SerializeField] public PhraseRecognition PhraseRecognition;

    void Start()
    {
        PlayerCam = GetComponentInChildren<Camera>();
        SpellManager = GetComponent<SpellManager>();
        PhraseRecognition = GetComponent<PhraseRecognition>();
    }

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))    // FOR DEBUG ONLY
        {
            SpellManager.ArcaneStrike();
        }
        */
        switch (PhraseRecognition.RecognizedPhrase)
        {
            case "shoot":
                SpellManager.ArcaneStrike();
                SpellIsCasted();
                break;
            case "ready":
            case "ray":
                SpellManager.Ready();
                SpellIsCasted();
                break;
        }
    }

    void SpellIsCasted()
    {
        PhraseRecognition.RecognizedPhrase += " ( Casted )";
    }
}
