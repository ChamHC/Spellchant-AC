using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PhraseRecognition : MonoBehaviour
{
    [SerializeField, ReadOnly] public string RecognizedPhrase;
    [SerializeField] private string[] _keywords;
    [SerializeField] private Image _vadIndicator;
    private KeywordRecognizer _recognizer;

    void Start()
    {
        _keywords = new string[] { "Stone"};

        _recognizer = new KeywordRecognizer(_keywords, ConfidenceLevel.Low);
        _recognizer.OnPhraseRecognized += OnPhraseRecognized;
        _recognizer.Start();
    }

    void Update()
    {
        if (_recognizer.IsRunning)
        {
            _vadIndicator.color = Color.green;
        }
        else
        {
            _vadIndicator.color = Color.red;
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("Text: {0}\n", args.text.ToLower());
        builder.AppendFormat("Time Taken: {0} seconds   ||  Confidence {1}\n", args.phraseDuration.TotalSeconds, args.confidence);
        Debug.Log(builder.ToString());

        RecognizedPhrase = args.text.ToLower();
    }
}
