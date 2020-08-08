using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack RPlayerAttack;
    public PlayerMovement RPlayerMovement;
    public PlayerEntity RPlayerEntity;

    public PlayerData Data;

    private Weapon CurrentWeapon;

    private void Awake()
    {
        RPlayerAttack = GetComponent<PlayerAttack>();
        RPlayerMovement = GetComponent<PlayerMovement>();
        RPlayerEntity = GetComponent<PlayerEntity>();
    }

    public void SavePlayerData()
    {
        CurrentWeapon.SaveWeaponData();
        foreach(WeaponData data in Data.Weapons)
        {
            if(data.WeaponName == CurrentWeapon.WeaponName)
            {
                Data.Weapons.Remove(data);
                Data.Weapons.Add(CurrentWeapon.RWeaponData);
            }
        }
        SaveLoad.Save<PlayerData>(Data, "PlayerData.sav");
    }
}

[System.Serializable]
public struct PlayerData
{
    public List<WeaponData> Weapons;
    public float Units;
    public float TotalUnits;
}
