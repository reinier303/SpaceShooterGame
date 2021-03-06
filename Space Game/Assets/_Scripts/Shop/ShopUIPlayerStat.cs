﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceGame
{
    public class ShopUIPlayerStat : MonoBehaviour
    {
        [HideInInspector] public string ModuleName;

        public TextMeshProUGUI textComponent, PointsSpent;

        private ShopManager shopManager;

        [HideInInspector] public HangarManager hangarManager;
        
        private GameObject saveWarningText;

        public void Initialize()
        {
            shopManager = ShopManager.Instance;
            textComponent.text = ModuleName;
            PointsSpent.text = "" + shopManager.Data.PlayerModules[ModuleName].PointsSpent;
            saveWarningText = shopManager.saveStatWarningText;
        }

        public void AddStat()
        {
            if (shopManager.Data.CurrentPoints >= shopManager.Data.PlayerModules[ModuleName].PointCost)
            {
                if (shopManager.Data.PlayerModules[ModuleName].PointsSpent >= shopManager.Data.PlayerModules[ModuleName].MaxPoints)
                {
                    return;
                }
                saveWarningText.SetActive(true);
                shopManager.Data.PlayerModules[ModuleName].PointsSpent++;
                PointsSpent.text = "" + shopManager.Data.PlayerModules[ModuleName].PointsSpent;
                shopManager.Data.CurrentPoints -= shopManager.Data.PlayerModules[ModuleName].PointCost;
                hangarManager.AdjustPlayerPoints();
            }
        }

        public void RemoveStat()
        {
            if (shopManager.Data.PlayerModules[ModuleName].PointsSpent > 0)
            {
                saveWarningText.SetActive(true);
                shopManager.Data.PlayerModules[ModuleName].PointsSpent--;
                PointsSpent.text = "" + shopManager.Data.PlayerModules[ModuleName].PointsSpent;
                shopManager.Data.CurrentPoints += shopManager.Data.PlayerModules[ModuleName].PointCost;
                hangarManager.AdjustPlayerPoints();
            }
        }
    }
}