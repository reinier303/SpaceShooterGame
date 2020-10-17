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
        public PlayerModule PlayerModule;
        public float Cost;
        public int MaxLvl;

        public virtual void UnlockModule()
        {
            if(ModuleToUnlock != null)
            {
                ModuleToUnlock.Weapon.AddModuleShop(ModuleToUnlock, MaxLvl);
            }
            else if(PlayerModule != null)
            {
                ShopManager.Instance.UnlockPlayerModule(PlayerModule, MaxLvl);
            }
        }
    }
}