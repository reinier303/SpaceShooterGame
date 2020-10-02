using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveSystem/Wave", order = 998)]
public class ScriptableWave : ScriptableObject
{
    public string WaveName;
    public Color WaveTextColor;
    public Material WaveTextMaterial;

    [Tooltip("Enemy Prefab and max amount alive")]
    public List<EnemyWaveData> EnemyPrefabs;
    public float StartSpawnRate = 1;
    public float SpawnRateRampPerSecond = -0.01f;
    public float MaxSpawnRate = 0.5f;
    [Header("This name has to be the same as in the pool")]
    public string BossName;
    public int EnemiesForBossSpawn;
}

[System.Serializable]
public struct EnemyWaveData
{
    [Header("This name has to be the same as in the pool")]
    public string Name;
    public int MaxAmount;
    public int Priority;
}
