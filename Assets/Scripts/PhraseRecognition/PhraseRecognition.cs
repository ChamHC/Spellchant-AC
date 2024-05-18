using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PhraseRecognition : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI spellText;
    [SerializeField] private string[] _keywords;
    private PhraseRecognizer _recognizer;


    void Start()
    {
        _keywords = new string[] { "Ready", "Arcane Strike" };

        _recognizer = new KeywordRecognizer(_keywords, ConfidenceLevel.Low);
        _recognizer.OnPhraseRecognized += OnPhraseRecognized;
        _recognizer.Start();

        string[] micArray = Microphone.devices;
        if (micArray.Length > 0)
        {
            Debug.Log("Default microphone: " + micArray[0]);
        }
        else
        {
            Debug.LogError("No microphone detected");
        }
    }

    void Update()
    {
        if (_recognizer.IsRunning)
            spellText.color = Color.green;
        else
            spellText.color = Color.red;
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("Text: {0}\n", args.text.ToLower());
        builder.AppendFormat("Time Taken: {0} seconds   ||  Confidence {1}\n", args.phraseDuration.TotalSeconds, args.confidence);
        Debug.Log(builder.ToString());
        spellText.text = char.ToUpper(args.text[0]) + args.text.Substring(1);
    }
}
