using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [System.Serializable]
    public class Weapon : ScriptableObject, IFire
    {
        public int ID;
        public string WeaponName;
        public string ProjectileName;
        public List<WeaponModule> BaseModules = new List<WeaponModule>();
        public Dictionary<string, ModuleData> Modules = new Dictionary<string, ModuleData>();

        public float CurrentExperience;
        public float experienceNeeded;
        public float PointsPerLevel;
        public float CurrentPoints;

        public int Level = 1;

        public WeaponData RWeaponData;

        private UIManager uIManager;

        public void InitializeUI()
        {
            uIManager = GameManager.Instance.RUIManager;
            float currentValue = CurrentExperience / experienceNeeded;
            uIManager.UpdateCurrentWeaponExp(currentValue);
            uIManager.UpdateCurrentWeaponLevel(RWeaponData.Level);
        }

        public void GetWeaponData(PlayerData data, int weaponIndex)
        {
            RWeaponData = data.Weapons[weaponIndex];
        }

        public void GainExperience(float expGain)
        {
            //Calculate if any experience remains after leveling up.
            float remainingExp = expGain + RWeaponData.CurrentExperience - RWeaponData.ExperienceNeeded;

            RWeaponData.CurrentExperience += expGain;

            //Check if level up.
            if (RWeaponData.CurrentExperience >= RWeaponData.ExperienceNeeded)
            {
                LevelUp();
                RWeaponData.CurrentExperience = 0;

                //Repeat GainExperience untill expGain runs out.
                if (remainingExp > 0)
                {
                    GainExperience(remainingExp);
                }
            }
            float currentValue = RWeaponData.CurrentExperience / RWeaponData.ExperienceNeeded;
            GameManager.Instance.RUIManager.UpdateCurrentWeaponExp(currentValue);
        }

        public void LevelUp()
        {
            RWeaponData.Level++;
            RWeaponData.CurrentPoints += PointsPerLevel;
            Debug.Log(Mathf.Clamp((1.25f + 0.01f * RWeaponData.Level), 1.25f, 1.65f));
            RWeaponData.ExperienceNeeded = RWeaponData.ExperienceNeeded * Mathf.Clamp((1.25f + 0.01f * RWeaponData.Level), 1.25f, 1.65f);
            GameManager.Instance.RUIManager.UpdateCurrentWeaponLevel(RWeaponData.Level);
            GameManager.Instance.RPlayer.SavePlayerData();
            Debug.Log(RWeaponData.ExperienceNeeded);
        }

        public void NewWeaponData()
        {
            RWeaponData.Modules = Modules;
            RWeaponData.WeaponName = WeaponName;
            RWeaponData.CurrentExperience = CurrentExperience;
            RWeaponData.ExperienceNeeded = experienceNeeded;
            RWeaponData.CurrentPoints = CurrentPoints;
            RWeaponData.Level = Level;
        }

        public void AddBaseModules()
        {
            foreach (WeaponModule module in BaseModules)
            {
                AddModule(module);
            }
        }

        public void AddModule(WeaponModule module)
        {
            if (!Modules.ContainsKey(module.StatName))
            {
                Modules.Add(module.StatName, module.GetModuleData());
            }
        }

        public void AddModuleShop(WeaponModule module, int maxPoints)
        {
            ShopManager shopManager = ShopManager.Instance;

            if (!shopManager.Weapons[module.Weapon.WeaponName].Modules.ContainsKey(module.StatName))
            {
                shopManager.Weapons[module.Weapon.WeaponName].Modules.Add(module.StatName, module.GetModuleData());
            }
            else if (maxPoints > shopManager.Weapons[module.Weapon.WeaponName].Modules[module.StatName].MaxPoints)
            {
                shopManager.Weapons[module.Weapon.WeaponName].Modules[module.StatName].MaxPoints = maxPoints;
            }

            shopManager.RefreshWeapons();
            shopManager.SaveWeapons();
        }

        public virtual void Fire(ObjectPooler objectPooler, Transform player)
        {
            //This method is meant to be overridden.
        }
    }

    public interface IFire
    {
        //Use Generic T for other optional parameters
        void Fire(ObjectPooler objectPooler, Transform player);
    }


    [System.Serializable]
    public class WeaponData
    {
        public Dictionary<string, ModuleData> Modules;

        public string WeaponName;

        public float CurrentExperience;
        public float ExperienceNeeded;
        public float CurrentPoints;

        public int Level;
    }

}