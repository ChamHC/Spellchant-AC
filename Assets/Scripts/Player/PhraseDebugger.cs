using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class PhraseDebugger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Hypotheses;
    [SerializeField] private Image _vadIndicator;

    private DictationRecognizer m_DictationRecognizer;

    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
            m_Hypotheses.text += text;
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        m_DictationRecognizer.Start();
        m_Hypotheses.text = "Result: ";
    }

    private void Update()
    {
        if (m_DictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            _vadIndicator.color = Color.green;
        }
        else
        {
            _vadIndicator.color = Color.red;
        }
    }
}
