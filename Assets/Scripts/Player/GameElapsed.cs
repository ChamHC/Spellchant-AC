using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameElapsed : MonoBehaviour
{
    private float gameElapsed;
    private TextMeshProUGUI timeDisplay;
    public LevelManager Lm;
    // Start is called before the first frame update
    void Start()
    {
        gameElapsed = 180f;
        timeDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Lm.PlayerIsReady)
        {
            gameElapsed -= Time.deltaTime;
        }
        else{
            gameElapsed = 180f;
        }
        timeDisplay.text = string.Format("{0:00}:{1:00}", Mathf.Floor(gameElapsed / 60), gameElapsed % 60);

        if (gameElapsed <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;  
            SceneManager.LoadScene("End Screen");
        }
    }
}
