using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "WeaponSystem/Module", order = 997)]
public class Module : ScriptableObject
{
    public int ID;
    public Weapon Weapon;
    public string StatName;
    public Stat stat;
    public int PointsSpent;
    public float PointValue;
    public int PointCost;
    public int MaxSpent;

    public float GetStatValue()
    {
        return stat.GetValue() + (PointsSpent * PointValue);
    }
}
