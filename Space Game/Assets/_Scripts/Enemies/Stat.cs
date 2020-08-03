using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private string statName;

    [SerializeField] private float baseValue = 1;

    [HideInInspector] public float multiplier;

    public float GetValue()
    {
        return baseValue;
    }
}
