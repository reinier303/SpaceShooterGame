using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Tooltip : MonoBehaviour
{
    public TMP_Text ItemName;
    public TMP_Text Description;
    public TMP_Text Price;

    /// <summary>
    /// Replaces -Name- with itemname 
    /// </summary>
    public void SetTooltipInfo(string name, string description, float price, int maxLvl)
    {
        ItemName.text = name;
        description = description.Replace("-MaxLevel-", maxLvl + "");
        description = description.Replace("-Name-", name);
        Description.text = description;       
        Price.text = "" + price;
    }

}
