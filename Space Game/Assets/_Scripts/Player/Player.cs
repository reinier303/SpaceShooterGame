using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Player : MonoBehaviour
    {
        //Player script references
        public PlayerAttack RPlayerAttack;
        public PlayerMovement RPlayerMovement;
        public PlayerEntity RPlayerEntity;

        //Script references
        private UIManager RUIManager;

        //Saving and loading
        public PlayerData Data;

        //Weapons
        //private Weapon CurrentWeapon;
        public Weapon StartWeapon;
        public List<Weapon> Weapons;

        //Leveling variables
        public float ExperienceNeeded;
        public float PointsPerLevel;

        public List<PlayerModule> playerBaseModules;

        private void Awake()
        {
            GetSaveData();

            RPlayerAttack = GetComponent<PlayerAttack>();
            RPlayerMovement = GetComponent<PlayerMovement>();
            RPlayerEntity = GetComponent<PlayerEntity>();
            RUIManager = GameManager.Instance.RUIManager;

            StartWeapon = (BulletWeapon)Resources.Load("Weapons/Bullet/BulletWeapon", typeof(Weapon));
        }

        private void GetSaveData()
        {
            if (SaveLoad.SaveExists("PlayerData.sav"))
            {
                Data = SaveLoad.Load<PlayerData>("PlayerData.sav");
            }
            else
            {
                SaveLoad.NewSave();
                Data = SaveLoad.Load<PlayerData>("PlayerData.sav");
            }
        }

        public void SetWeapons(List<Weapon> weapons)
        {
            Weapons = weapons;
        }

        public void AddExperience(float expGain, int weaponIndex)
        {
            Weapons[weaponIndex].GainExperience(expGain);
            GainExperience(expGain);
        }

        public void GainExperience(float expGain)
        {
            //Calculate if any experience remains after leveling up.
            float remainingExp = expGain + Data.CurrentExperience - Data.ExperienceNeeded;

            Data.CurrentExperience += expGain;

            //Check if level up.
            if (Data.CurrentExperience >= Data.ExperienceNeeded)
            {
                LevelUp();
                Data.CurrentExperience = 0;

                //Repeat GainExperience untill expGain runs out.
                if (remainingExp > 0)
                {
                    GainExperience(remainingExp);
                }
            }
            float currentValue = Data.CurrentExperience / Data.ExperienceNeeded;
            RUIManager.UpdatePlayerExp(currentValue);
        }

        public void LevelUp()
        {
            Data.Level++;
            Data.CurrentPoints += PointsPerLevel;
            Data.ExperienceNeeded = Data.ExperienceNeeded * Mathf.Clamp((1.3f + 0.01f * Data.Level), 1.3f, 1.85f);
            RUIManager.UpdatePlayerLevel(Data.Level);
            GameManager.Instance.RPlayer.SavePlayerData();
        }

        public void AddUnits(float units)
        {
            Data.Units += units;
            RUIManager.UpdateUnits(units);
        }

        public void AddUnitsToTotal()
        {
            Data.TotalUnits += Data.Units;
            Data.Units = 0;
        }

        public void SavePlayerData()
        {
            AddUnitsToTotal();
            for (int i = 0; i < Data.Weapons.Count; i++)
            {
                for(int j = 0; j < Weapons.Count; j++)
                {
                    Debug.Log("OUT: Data" + Data.Weapons[i].WeaponName + "Weapons" + Weapons[j].WeaponName);

                    if (Data.Weapons[i].WeaponName == Weapons[j].WeaponName)
                    {
                        Debug.Log("IN: Data" + Data.Weapons[i].WeaponName + "Weapons" + Weapons[j].WeaponName);
                        Data.Weapons.RemoveAt(i);
                        Data.Weapons.Add(Weapons[i].RWeaponData);
                    }
                }

            }
            SaveLoad.Save<PlayerData>(Data, "PlayerData.sav");
        }

        protected virtual void OnBecameVisible()
        {

        }

        protected virtual void OnBecameInvisible()
        {

        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public List<WeaponData> Weapons;

        public Dictionary<string, ModuleData> PlayerModules;
        public float CurrentExperience;
        public float ExperienceNeeded;
        public float CurrentPoints;
        public int Level;

        public float Units;
        public float TotalUnits;
    }
}