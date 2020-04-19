using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveSystem/Wave", order = 998)]
public class ScriptableWave : ScriptableObject
{
    public string Name;
    [Tooltip("Enemy Prefab and max amount alive")]
    public EnemyWaveData[] EnemyPrefabs;
    public float SpawnRate = 1;
}

[System.Serializable]
public struct EnemyWaveData
{
    [Header("This name has to be the same as in the pool")]
    public string Name;
    public int MaxAmount;
}
