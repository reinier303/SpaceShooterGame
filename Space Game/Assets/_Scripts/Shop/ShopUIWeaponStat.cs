using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceGame
{
    public class ShopUIWeaponStat : MonoBehaviour
    {
        [HideInInspector] public string ModuleName;

        public TextMeshProUGUI textComponent, PointsSpent;

        public ShopUIWeapon shopUIWeapon;

        private GameObject saveWarningText;

        public void Initialize()
        {
            textComponent.text = ModuleName;
            PointsSpent.text = "" + shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent;
            saveWarningText = ShopManager.Instance.saveStatWarningText;
        }

        public void AddStat()
        {
            if (shopUIWeapon.weaponData.CurrentPoints >= shopUIWeapon.weaponData.Modules[ModuleName].PointCost)
            {
                if (shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent >= shopUIWeapon.weaponData.Modules[ModuleName].MaxPoints)
                {
                    return;
                }
                saveWarningText.SetActive(true);
                shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent++;
                PointsSpent.text = "" + shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent;
                shopUIWeapon.weaponData.CurrentPoints -= shopUIWeapon.weaponData.Modules[ModuleName].PointCost;
                shopUIWeapon.AdjustPointsToSpend();
                Debug.Log(ModuleName + ": Added, Points Left:" + shopUIWeapon.weaponData.CurrentPoints);
            }
        }

        public void RemoveStat()
        {
            if (shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent > 0)
            {
                saveWarningText.SetActive(true);
                shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent--;
                PointsSpent.text = "" + shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent;
                shopUIWeapon.weaponData.CurrentPoints += shopUIWeapon.weaponData.Modules[ModuleName].PointCost;
                shopUIWeapon.AdjustPointsToSpend();
                Debug.Log(ModuleName + ": Removed, Points Left:" + shopUIWeapon.weaponData.CurrentPoints);

            }
        }
    }
}