using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PhraseRecognition : MonoBehaviour
{
    [SerializeField] private PrimitiveShoot _primitiveShoot;
    [SerializeField] private string[] m_Keywords;
    [SerializeField] private Image _vadIndicator;
    private KeywordRecognizer m_Recognizer;

    void Start()
    {
        m_Recognizer = new KeywordRecognizer(m_Keywords, ConfidenceLevel.Low);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    void Update()
    {
        if (m_Recognizer.IsRunning)
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
        builder.AppendFormat("Text: {0}\n", args.text);
        builder.AppendFormat("Time Taken: {0} seconds   ||  Confidence {1}\n", args.phraseDuration.TotalSeconds, args.confidence);
        Debug.Log(builder.ToString());

        _primitiveShoot.Shoot();
    }
}
