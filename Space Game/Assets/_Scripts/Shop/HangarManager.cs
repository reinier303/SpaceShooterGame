using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceGame
{
    public class HangarManager : MonoBehaviour
    {
        private ShopManager shopManager;

        private PlayerData data;

        [SerializeField] private Transform weaponPanel, statPanel, playerModulePanel;

        [SerializeField] private GameObject weaponButton, weaponStat, playerModuleStat, lockedButton;

        [SerializeField] private TMP_Text playerPoints;

        private void OnEnable()
        {
            shopManager = ShopManager.Instance;
            data = shopManager.Data;
            InitializeWeapons();
            InitializePlayerModules();
            AdjustPlayerPoints();
        }

        private void InitializeWeapons()
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
            foreach (WeaponData weaponData in data.Weapons)
            {
                GameObject weapon = Instantiate(weaponButton, weaponPanel);
                ShopUIWeapon UIWeapon = weapon.GetComponent<ShopUIWeapon>();
                UIWeapon.weaponData = weaponData;
                UIWeapon.weaponModulePanel = statPanel;
                UIWeapon.Initialize();
            }
        }

        private void InitializePlayerModules()
        {
            for (int i = 0; i < playerModulePanel.childCount; i++)
            {
                //TODO: this is ugly make this better, either in hierarchy or code...
                if (playerModulePanel.GetChild(i).name != "SaveButton")
                {
                    Destroy(playerModulePanel.GetChild(i).gameObject);
                }
            }
            foreach (ModuleData moduleData in data.PlayerModules.Values)
            {
                if (data.PlayerModules[moduleData.StatName].MaxPoints == 0)
                {
                    Instantiate(lockedButton, playerModulePanel);
                    continue;
                }
                GameObject module = Instantiate(playerModuleStat, playerModulePanel);
                ShopUIPlayerStat playermodule = module.GetComponent<ShopUIPlayerStat>();
                playermodule.ModuleName = moduleData.StatName;
                playermodule.hangarManager = this;
                playermodule.Initialize();
            }
        }

        public void AdjustPlayerPoints()
        {
            playerPoints.text = "" + shopManager.Data.CurrentPoints;
        }

        public void SaveWeaponData()
        {
            SaveLoad.Save<PlayerData>(data, "PlayerData.sav");
        }
    }
}