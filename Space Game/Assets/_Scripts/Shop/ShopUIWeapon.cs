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

    public TextMeshProUGUI textComponent;

    public void ShowWeaponModules()
    {
        for(int i = 0; i < weaponModulePanel.childCount; i++)
        {
            Destroy(weaponModulePanel.GetChild(i).gameObject);
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
}
