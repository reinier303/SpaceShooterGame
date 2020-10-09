using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

namespace SpaceGame
{
    public class ShopManager : MonoBehaviour
    {
        public static ShopManager Instance;

        public PlayerData Data;

        public Weapon Bullets;

        public Transform ShopPanel;

        public GameObject ShopItem;

        public Tooltip Tooltip;

        public TMP_Text UnitsText;

        public Dictionary<string, WeaponData> Weapons = new Dictionary<string, WeaponData>();

        private List<int> ShopItemsUnlocked = new List<int>();

        private void Awake()
        {
            Time.timeScale = 1;
            Instance = this;

            Data = SaveLoad.Load<PlayerData>("PlayerData.sav");

            RefreshWeapons();
            InitializeShop();
        }

        private void InitializeShop()
        {

            UnitsText.text = "Units:" + Data.TotalUnits;

            InitializeUnlockedItems();

            Object[] ScriptableItems = Resources.LoadAll("ShopItems", typeof(ScriptableShopItem));

            foreach (ScriptableShopItem item in ScriptableItems)
            {
                if (ShopItemsUnlocked.Contains(item.GetInstanceID()))
                {
                    continue;
                }
                GameObject itemObject = Instantiate(ShopItem, ShopPanel);
                itemObject.GetComponent<ShopUIItem>().scriptableShopItem = item;
            }
            Tooltip.transform.SetAsLastSibling();
        }

        private void InitializeUnlockedItems()
        {
            if (SaveLoad.SaveExists("ShopItemsUnlocked.sav"))
            {
                ShopItemsUnlocked = SaveLoad.Load<List<int>>("ShopItemsUnlocked.sav");
            }
            else
            {
                ShopItemsUnlocked = new List<int>();
                SaveUnlockedItems();
            }
        }

        public void RefreshWeapons()
        {
            Weapons.Clear();
            foreach (WeaponData weapon in Data.Weapons)
            {
                Weapons.Add(weapon.WeaponName, weapon);
            }
        }

        public void SaveWeapons()
        {
            Data.Weapons = Weapons.Values.ToList();
            SaveLoad.Save(Data, "PlayerData.sav");
        }

        public void RemoveUnits(float Units)
        {
            Data.TotalUnits -= Units;
            UnitsText.text = "Units:" + Data.TotalUnits;
        }

        public float GetUnits()
        {
            return Data.TotalUnits;
        }

        //TODO:Loading screen and helper loadscene method instead of here
        public void LoadSceneAsync(int scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }

        public void ItemBought(ScriptableShopItem item)
        {
            ShopItemsUnlocked.Add(item.GetInstanceID());
            SaveUnlockedItems();
        }

        public void SaveUnlockedItems()
        {
            SaveLoad.Save(ShopItemsUnlocked, "ShopItemsUnlocked.sav");
            SaveLoad.Save(Data, "PlayerData.sav");
        }
    }
}