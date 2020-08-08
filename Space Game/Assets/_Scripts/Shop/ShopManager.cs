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
        Data.Weapons.Add(Bullets.RWeaponData);

        Debug.Log(Data.Weapons);
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
