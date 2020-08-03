using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarManager : MonoBehaviour
{
    private PlayerData data;

    [SerializeField] private Transform weaponPanel, statPanel;

    [SerializeField] private GameObject weaponButton, weaponStat;

    // Start is called before the first frame update
    private void Start()
    {
        data = ShopManager.Instance.Data;
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach (WeaponData weapon in data.Weapons)
        {
            Instantiate(weaponButton, weaponPanel);
        }
    }
}
