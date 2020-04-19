using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text LivesText;
    public TMP_Text UnitsText;

    public PlayerEntity RPlayerEntity;

    public void TakeDamage(float damage)
    {
        LivesText.text = "Lives:" + RPlayerEntity.currentHealth;
    }

    //Damage number popup
    public void ShowPopUp()
    {

    }
}
