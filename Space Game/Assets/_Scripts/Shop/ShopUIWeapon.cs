using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ShopUIWeapon : MonoBehaviour
{
    public WeaponData weaponData;

    [SerializeField] private GameObject weaponButton;

    [HideInInspector] public Transform weaponModulePanel;

    public TextMeshProUGUI TextComponent;
    public TextMeshProUGUI PointsToSpend;

    public void Initialize()
    {
        TextComponent.text = weaponData.WeaponName;
        PointsToSpend.text = "" + weaponData.CurrentPoints;
    }

    public void ShowWeaponModules()
    {
        for(int i = 0; i < weaponModulePanel.childCount; i++)
        {
            //TODO: this is ugly make this better, either in hierarchy or code...
            if(weaponModulePanel.GetChild(i).name != "SaveButton")
            {
                Destroy(weaponModulePanel.GetChild(i).gameObject);
            }
        }
        for(int i = 0; i < weaponData.Modules.Count; i++)
        {
            GameObject weaponStat = Instantiate(weaponButton, weaponModulePanel);
            ShopUIWeaponStat weaponStatScript = weaponStat.GetComponent<ShopUIWeaponStat>();
            weaponStatScript.ModuleName = weaponData.Modules.ElementAt(i).Key;
            weaponStatScript.shopUIWeapon = this;
            weaponStatScript.Initialize();
        }
    }

    public void AdjustPointsToSpend()
    {
        PointsToSpend.text = "" + weaponData.CurrentPoints;
    }
}
