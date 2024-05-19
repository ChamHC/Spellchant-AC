using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CastManager : MonoBehaviour
{
    [Header("Hidden Attributes")]
    [SerializeField] public Camera PlayerCam;
    [SerializeField] public SpellManager SpellManager;
    [SerializeField] public PhraseRecognition PhraseRecognition;
    [SerializeField] public Image Overlay;

    private float t = 0;
    void Start()
    {
        PlayerCam = GetComponentInChildren<Camera>();
        SpellManager = GetComponent<SpellManager>();
        PhraseRecognition = GetComponent<PhraseRecognition>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))    // FOR DEBUG ONLY
        {
            SpellManager.ArcaneStrike();
        }
        
        switch (PhraseRecognition.spellText.text.ToLower())
        {
            case "arcane strike":
                if (Input.GetMouseButtonDown(0)){
                    SpellManager.ArcaneStrike();
                    PhraseRecognition.spellText.text = "No Spell";
                    Overlay.color = new Color(0, 0, 0, 0);
                    Time.timeScale = 1;
                    t = 0; 
            break;
                }
                break;
            case "i am ready":
                SpellManager.Ready();
                PhraseRecognition.spellText.text = "No Spell";
                break;
            default:
                break;
        }

        if (PhraseRecognition.spellText.text != "No Spell" && PhraseRecognition.spellText.text != "Ready" )
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(1f, 0.2f, t);

            Color currentColor = Overlay.color;
            float alpha = Mathf.Lerp(0, 0.5f, t*2);
            Overlay.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        }
    }
}
