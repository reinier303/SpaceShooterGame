using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponSystem/Weapon", order = 997)]
[System.Serializable]
public class Weapon : ScriptableObject
{
    public int ID;
    public string WeaponName;
    public string ProjectileName;
    public List<Module> BaseModules = new List<Module>();
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
        uIManager.UpdateCurrentWeapon(currentValue);
        uIManager.UpdateCurrentWeaponLevel(RWeaponData.Level);
    }

    public void GetWeaponData(PlayerData data, int weaponIndex)
    {
        Debug.Log(data.Weapons.Count);
        Debug.Log(data.Weapons[weaponIndex].WeaponName);
        RWeaponData = data.Weapons[weaponIndex];
    }

    public void GainExperience(float expGain)
    {
        //Calculate if any experience remains after leveling up.
        float remainingExp = expGain + RWeaponData.CurrentExperience - RWeaponData.experienceNeeded;

        RWeaponData.CurrentExperience += expGain;

        //Check if level up.
        if(RWeaponData.CurrentExperience >= RWeaponData.experienceNeeded)
        {
            LevelUp();
            RWeaponData.CurrentExperience = 0;

            //Repeat GainExperience untill expGain runs out.
            if(remainingExp > 0)
            {
                GainExperience(remainingExp);
            }
        }
        float currentValue = RWeaponData.CurrentExperience / RWeaponData.experienceNeeded;
        GameManager.Instance.RUIManager.UpdateCurrentWeapon(currentValue);
    }

    public void LevelUp()
    {
        Level++;
        RWeaponData.CurrentPoints += PointsPerLevel;
        GameManager.Instance.RUIManager.UpdateCurrentWeaponLevel(RWeaponData.Level);
        SaveWeaponData();
    }

    public void SaveWeaponData()
    {
        RWeaponData.Modules = Modules;
        RWeaponData.WeaponName = WeaponName;
        RWeaponData.CurrentExperience = CurrentExperience;
        RWeaponData.experienceNeeded = experienceNeeded;
        RWeaponData.CurrentPoints = CurrentPoints;
        RWeaponData.Level = Level;
    }

    public void AddBaseModules()
    {
        foreach(Module module in BaseModules)
        {
            AddModule(module);
        }
    }

    public void AddModule(Module module)
    {
        if(!Modules.ContainsKey(module.StatName))
        {
            Modules.Add(module.StatName, module.GetModuleData());
        }
    }
}

[System.Serializable]
public class WeaponData
{
    public Dictionary<string, ModuleData> Modules;

    public string WeaponName;

    public float CurrentExperience;
    public float experienceNeeded;
    public float CurrentPoints;

    public int Level;
}

