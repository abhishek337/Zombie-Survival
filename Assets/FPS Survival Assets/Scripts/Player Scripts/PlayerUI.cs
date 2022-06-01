using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    public Image Health_Ui, Stamina_Ui;

    public void health_ui(float healthvalue)
    {
        healthvalue /= 100;

        Health_Ui.fillAmount = healthvalue;
    }

    public void stamina_ui(float staminavalue)
    {
        staminavalue /= 100;

        Stamina_Ui.fillAmount = staminavalue;
    }
}
