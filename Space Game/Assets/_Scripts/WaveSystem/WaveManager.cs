using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    //Script References
    private GameManager gameManager;
    private ObjectPooler RObjectPooler;

    //Initialisation Data
    private List<ScriptableWave> Waves = new List<ScriptableWave>();

    //Current Wave Data
    public int currentWave = 0;
    [SerializeField] private float currentSpawnTime;

    //General Data
    public int EnemiesAlive;
    public bool Spawning;

    //Background Renderer: info for spawn location
    public SpriteRenderer MapRenderer;

    private List<string> enemyNames = new List<string>();

    public int EnemiesKilledThisWave;

    public BossArrow BossArrowScript;

    private void Awake()
    {
        Object[] ScriptableWaves = Resources.LoadAll("Waves", typeof(ScriptableWave));
        foreach (ScriptableWave wave in ScriptableWaves)
        {
            //Check if pool info is filled.
            if (wave.StartSpawnRate > 0 && wave.EnemyPrefabs.Count > 0 && wave.WaveName != null)
            {
                Waves.Add(wave);
            }
            else
            {
                Debug.LogWarning("Wave: " + wave.name + " is missing some information. \n Please go back to Resources/Waves and fill in the information correctly");
            }
        }
        //currentWave++;
        GenerateRandomEnemyList();
    }

    private void Start()
    {
        RObjectPooler = ObjectPooler.Instance;
        gameManager = GameManager.Instance;
        currentSpawnTime = Waves[currentWave].StartSpawnRate;
        StartCoroutine(StartSpawning());
        StartCoroutine(RampSpawnRate());
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
            string enemy = enemyNames[Random.Range(0, enemyNames.Count)];
            RObjectPooler.SpawnFromPool(enemy, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator RampSpawnRate()
    {
        yield return new WaitForSeconds(1);
        currentSpawnTime += Waves[currentWave].SpawnRateRampPerSecond;
        if(currentSpawnTime >= Waves[currentWave].MaxSpawnRate)
        {
            StopCoroutine(RampSpawnRate());
        }
    }

    public void AdjustToBoss(float spawnMultiplier)
    {
        currentSpawnTime = Waves[currentWave].StartSpawnRate * spawnMultiplier;
        StopCoroutine(RampSpawnRate());
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

    private void GenerateRandomEnemyList()
    {
        enemyNames.Clear();
        for(int i = 0; i < Waves[currentWave].EnemyPrefabs.Count; i++)
        {
            for(int j = 0; j < Waves[currentWave].EnemyPrefabs[i].Priority; j++)
            {
                enemyNames.Add(Waves[currentWave].EnemyPrefabs[i].Name);
            }
        }
    }

    public void NextWave()
    {
        BossArrowScript.gameObject.SetActive(false);
        if(currentWave < Waves.Count -1)
        {
            currentWave++;
        }
        Debug.Log(currentWave);
        currentSpawnTime = Waves[currentWave].StartSpawnRate;
        GenerateRandomEnemyList();
        StartCoroutine(RampSpawnRate());
        gameManager.RUIManager.InitializeWaveUI();
    }

    public void EnemyKilled()
    {
        if(gameManager.BossAlive)
        {
            return;
        }

        EnemiesKilledThisWave++;
        if(EnemiesKilledThisWave >= Waves[currentWave].EnemiesForBossSpawn)
        {
            EnemiesKilledThisWave = 0;
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        BossArrowScript.gameObject.SetActive(true);
        Vector2 spawnPosition = GenerateSpawnPosition();
        GameObject Boss = RObjectPooler.SpawnFromPool(Waves[currentWave].BossName, spawnPosition, Quaternion.identity);
        BossArrowScript.Target = Boss.transform;
        BossArrowScript.bossIcon.sprite = Boss.GetComponent<BaseBoss>().BossIcon;
    }

    public ScriptableWave GetWave(int wave)
    {
        return Waves[wave];
    }
}
