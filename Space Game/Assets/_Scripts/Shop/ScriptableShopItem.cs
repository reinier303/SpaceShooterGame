using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableShopItem : ScriptableObject
{
    public Sprite ShopIcon;
    public string ShopItemName;
    public Module ModuleToUnlock;
    public float Cost;

    public void UnlockModule()
    {
        ModuleToUnlock.Weapon.AddModule(ModuleToUnlock);
    }
}
