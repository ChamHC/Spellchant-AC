using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] public EntityAttributes PlayerAttributes;
    [SerializeField] public Slider HealthFill;

    void Start()
    {
        HealthFill = GetComponent<Slider>();
        PlayerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityAttributes>();
    }

    private void Update()
    {
        if (HealthFill != null && PlayerAttributes != null)
        {
            HealthFill.value = PlayerAttributes.CurrentHealth / PlayerAttributes.MaxHealth;
        }

        if (PlayerAttributes.CurrentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;  
            SceneManager.LoadScene("End Screen");
        }
    }
}
