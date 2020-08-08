using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    //Script References
    private ObjectPooler RObjectPooler;

    //Initialisation Data
    private List<ScriptableWave> Waves = new List<ScriptableWave>();

    //Current Wave Data
    public int currentWave;
    private float currentSpawnTime;
    private List<EnemyWaveData> currentEnemyWaveDatas;

    //General Data
    public int EnemiesAlive;
    public bool Spawning;

    //Background Renderer: info for spawn location
    public SpriteRenderer MapRenderer;

    private void Awake()
    {
        Object[] ScriptableWaves = Resources.LoadAll("Waves", typeof(ScriptableWave));
        foreach (ScriptableWave wave in ScriptableWaves)
        {
            //Check if pool info is filled.
            if (wave.SpawnRate > 0 && wave.EnemyPrefabs.Length > 0 && wave.Name != null)
            {
                Waves.Add(wave);
            }
            else
            {
                Debug.LogWarning("Wave: " + wave.name + " is missing some information. \n Please go back to Resources/Waves and fill in the information correctly");
            }
        }
    }

    private void Start()
    {
        RObjectPooler = ObjectPooler.Instance;
        currentWave = 0;
        currentSpawnTime = Waves[currentWave].SpawnRate;
        currentEnemyWaveDatas = Waves[currentWave].EnemyPrefabs.ToList();

        StartCoroutine(StartSpawning());
    }

    private IEnumerator StartSpawning()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(currentSpawnTime);
        StartCoroutine(StartSpawning());
    }

    private void SpawnEnemy()
    {
        if (Spawning)
        {
            EnemiesAlive++;
            Vector2 spawnPosition = GenerateSpawnPosition();
            string enemy = RandomEnemyName();
            GameObject Enemy = RObjectPooler.SpawnFromPool(enemy, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-MapRenderer.size.x, MapRenderer.size.x) / 2, Random.Range(-MapRenderer.size.y, MapRenderer.size.y) / 2);
        Vector2 viewPos = Camera.main.WorldToViewportPoint(spawnPosition);

        while (viewPos.x > 0 && viewPos.x < 1f && viewPos.y > 0 && viewPos.y < 1f)
        {
            spawnPosition = new Vector2(Random.Range(-MapRenderer.size.x, MapRenderer.size.x) / 2, Random.Range(-MapRenderer.size.y, MapRenderer.size.y) / 2);
            viewPos = Camera.main.WorldToViewportPoint(spawnPosition);
        }

        return spawnPosition;
    }

    private string RandomEnemyName()
    {
        string name = currentEnemyWaveDatas[Random.Range(0, currentEnemyWaveDatas.Count)].Name;
        return name;
    }

    private void NextWave()
    {
        currentWave++;
        currentSpawnTime = Waves[currentWave].SpawnRate;
        currentEnemyWaveDatas = Waves[currentWave].EnemyPrefabs.ToList();
    }
}
