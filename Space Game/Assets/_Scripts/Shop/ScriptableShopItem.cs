using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
        [Header("Use this if multiple modules need to be unlocked")]
        public List<PlayerModule> PlayerModulesToUnlock;
        public float Cost;
        public int MaxLvl;

        public virtual void UnlockModule()
        {
            ShopManager shopManager = ShopManager.Instance;
            if(PlayerModulesToUnlock.Count > 0)
            {
                foreach(PlayerModule module in PlayerModulesToUnlock)
                {
                    shopManager.UnlockPlayerModule(module, 0);
                }
            }
            else if(ModuleToUnlock != null)
            {
                ModuleToUnlock.Weapon.AddModuleShop(ModuleToUnlock, MaxLvl);
            }
            else if(PlayerModule != null)
            {
                shopManager.UnlockPlayerModule(PlayerModule, MaxLvl);
            }
        }
    }
}