using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{
    public static void Save<T>(T objectToSave, string key)
    {
        string path = Application.persistentDataPath + "/saveData/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(path + key + ".sav", FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
        }
    }

    public static T Load<T>(string key)
    {
        string path = Application.persistentDataPath + "/saveData/";
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        if(File.Exists(path + key + ".sav"))
        {
            using (FileStream fileStream = new FileStream(path + key + ".sav", FileMode.Open))
            {
                returnValue = (T)formatter.Deserialize(fileStream);
            }
        }

        return returnValue;
    }

    public static bool SaveExists(string key)
    {
        string path = Application.persistentDataPath + "/saveData/" + key + ".sav";
        return File.Exists(path);
    }

    public static void DeleteSavedData()
    {
        string path = Application.persistentDataPath + "/saveData/";
        DirectoryInfo directory = new DirectoryInfo(path);
    }

    public static void NewSave()
    {
        PlayerData data = new PlayerData();
        data.TotalUnits = 0;
        data.Units = 0;

        data.ExperienceNeeded = GameManager.Instance.RPlayer.ExperienceNeeded;
        data.CurrentExperience = 0;
        data.CurrentPoints = 0;

        data.Weapons = new List<WeaponData>();
        Weapon weapon = (Weapon)Resources.Load("Weapons/Bullet/Bullets", typeof(Weapon));
        weapon.AddBaseModules();
        weapon.NewWeaponData();
        data.Weapons.Add(weapon.RWeaponData);

        Save(data, "PlayerData.sav");
        Debug.Log("New Save Made" + "Weapon 0:" + data.Weapons[0].WeaponName);
    }
}
