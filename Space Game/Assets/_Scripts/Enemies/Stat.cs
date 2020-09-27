using UnityEngine;

[System.Serializable]
public class Stat
{
    public string statName;

    [SerializeField] private float baseValue = 1;

    public float multiplier = 1;

    public Stat(string name, float value, float newMultiplier)
    {
        statName = name;
        baseValue = value;
        multiplier = newMultiplier;
    }

    public float GetValue()
    {
        return baseValue * multiplier;
    }
}
