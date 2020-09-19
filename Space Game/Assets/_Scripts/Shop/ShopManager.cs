using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public PlayerData Data;

    public Weapon Bullets;

    private void Awake()
    {
        Instance = this;

        Data = SaveLoad.Load<PlayerData>("PlayerData");
        Bullets.SaveWeaponData();
        Bullets.AddBaseModules();
        Data.Weapons.Add(Bullets.RWeaponData);
    }

    public void RemoveUnits(float Units)
    {
        Data.TotalUnits -= Units;
    }

    public float GetUnits()
    {
        return Data.TotalUnits;
    }
}
