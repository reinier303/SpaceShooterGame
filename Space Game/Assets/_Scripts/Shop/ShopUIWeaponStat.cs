using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUIWeaponStat : MonoBehaviour
{
    [HideInInspector] public string ModuleName;

    public TextMeshProUGUI textComponent, PointsSpent;

    public ShopUIWeapon shopUIWeapon;

    public void Initialize()
    {
        textComponent.text = ModuleName;
        PointsSpent.text = "" + shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent;

    }

    public void AddStat()
    {
        if(shopUIWeapon.weaponData.CurrentPoints >= shopUIWeapon.weaponData.Modules[ModuleName].PointCost)
        {
            Debug.Log(shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent + ", Max:" + shopUIWeapon.weaponData.Modules[ModuleName].MaxPoints);

            if (shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent >= shopUIWeapon.weaponData.Modules[ModuleName].MaxPoints)
            {
                return;
            }
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
            shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent--;
            PointsSpent.text = "" + shopUIWeapon.weaponData.Modules[ModuleName].PointsSpent;
            shopUIWeapon.weaponData.CurrentPoints += shopUIWeapon.weaponData.Modules[ModuleName].PointCost;
            shopUIWeapon.AdjustPointsToSpend();
            Debug.Log(ModuleName + ": Removed, Points Left:" + shopUIWeapon.weaponData.CurrentPoints);

        }
    }
}
