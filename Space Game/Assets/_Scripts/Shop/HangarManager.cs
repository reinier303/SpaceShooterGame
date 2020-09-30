﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarManager : MonoBehaviour
{
    private PlayerData data;

    [SerializeField] private Transform weaponPanel, statPanel;

    [SerializeField] private GameObject weaponButton, weaponStat;


    // Start is called before the first frame update
    private void OnEnable()
    {
        data = ShopManager.Instance.Data;
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach (WeaponData weaponData in data.Weapons)
        {
            for (int i = 0; i < weaponPanel.childCount; i++)
            {
                Destroy(weaponPanel.GetChild(i).gameObject);
            }
            for (int i = 0; i < statPanel.childCount; i++)
            {
                //TODO: this is ugly make this better, either in hierarchy or code...
                if (statPanel.GetChild(i).name != "SaveButton")
                {
                    Destroy(statPanel.GetChild(i).gameObject);
                }
            }
            GameObject weapon = Instantiate(weaponButton, weaponPanel);
            ShopUIWeapon UIWeapon = weapon.GetComponent<ShopUIWeapon>();
            UIWeapon.weaponData = weaponData;
            UIWeapon.weaponModulePanel = statPanel;
            UIWeapon.Initialize();
        }
    }

    public void SaveWeaponData()
    {
        SaveLoad.Save<PlayerData>(data, "PlayerData.sav");
    }
}
