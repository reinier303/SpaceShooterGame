using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "WeaponSystem/Player Module", order = 997)]
public class PlayerModule : ScriptableObject
{
    public int ID;
    public string StatName;
    public Stat stat;
    public int PointsSpent;
    public float PointValue;
    public int PointCost;
    public int MaxPoints;

    public ModuleData GetModuleData()
    {
        ModuleData data = new ModuleData();
        data.ID = ID;
        //data.Weapon = Weapon;
        data.StatName = StatName;
        data.stat = stat;
        data.PointsSpent = PointsSpent;
        data.PointValue = PointValue;
        data.PointCost = PointCost;
        data.MaxPoints = MaxPoints;

        return data;
    }
}
