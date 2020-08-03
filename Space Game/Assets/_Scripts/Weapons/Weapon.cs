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
    public Dictionary<string, Module> Modules = new Dictionary<string, Module>();

    public float CurrentExperience;
    private float experienceNeeded;

    public int Level = 1;

    public WeaponData RWeaponData;

    public void GainExperience(float expGain)
    {
        //Calculate if any experience remains after leveling up.
        float remainingExp = expGain + CurrentExperience - experienceNeeded;

        CurrentExperience += expGain;

        //Check if level up.
        if(CurrentExperience >= experienceNeeded)
        {
            Level++;
            CurrentExperience = 0;

            //Repeat GainExperience untill expGain runs out.
            if(remainingExp > 0)
            {
                GainExperience(remainingExp);
            }
        }
    }

    public void SaveWeaponData()
    {
        RWeaponData.Modules = Modules;
        RWeaponData.WeaponName = WeaponName;
        RWeaponData.CurrentExperience = CurrentExperience;
        RWeaponData.experienceNeeded = experienceNeeded;
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
            Modules.Add(module.StatName, module);
        }
    }
}

[System.Serializable]
public struct WeaponData
{
    public Dictionary<string, Module> Modules;

    public string WeaponName;

    public float CurrentExperience;
    public float experienceNeeded;

    public int Level;
}

