using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(menuName = "Shop/Item", order = 995)]
    public class ScriptableShopItem : ScriptableObject
    {
        public int ID;
        public bool IDSet = false;
        public Sprite ShopIcon;
        public string ShopItemName;
        [TextArea(3, 20)]
        [Header("Replaces -Name- with ShopItemName and -MaxLevel- with MaxLvl")]
        public string Description;
        public WeaponModule ModuleToUnlock;
        public float Cost;
        public int MaxLvl;

        public void UnlockModule()
        {
            ModuleToUnlock.Weapon.AddModuleShop(ModuleToUnlock, MaxLvl);
        }
    }
}