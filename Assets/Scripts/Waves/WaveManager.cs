using NUnit.Framework.Internal.Filters;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [SerializeField]
    private LevelWavesSO wavesForLevel;
    [SerializeField]
    private float spawnDistanceFromPlayer = 15f;
    private bool wasBossSpawned;

    public float GetTimeFromStartToBoss()
    {
        return wavesForLevel.waveTime * (wavesForLevel.waves.Count - 1);
    }

    private Vector2 GetRandomPointOnCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        return center + new Vector2(x, y);
    }

    private void SpawnWave(int wave)
    {
        WaveEnemies waveData = wavesForLevel.waves[wave - 1];
        foreach (EnemyWithAmmount enemyData in waveData.enemies)
        {
            for (int i = 0; i < enemyData.amount; i++)
            {
                SpawnEnemy(enemyData.enemy);
            }
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        GameObject spawnedEnemy = Instantiate(enemy);
        if (PlayerController.Instance != null)
        {
            spawnedEnemy.transform.position = GetRandomPointOnCircle(PlayerController.Instance.transform.position, spawnDistanceFromPlayer);
        }
        else
        {
            Debug.LogError("Nie można odnaleźć gracza w celu ustalenia pozycji tworzonego przeciwnika!");
        }
    }

    private void SpawnBoss()
    {
        wasBossSpawned = true;

        SpawnEnemy(wavesForLevel.bossEnemy);
    }

    private IEnumerator WavesCoroutine()
    {
        int currentWave = 1;
        WaitForSeconds wait = new WaitForSeconds(wavesForLevel.waveTime);
        while (true) 
        { 
            SpawnWave(currentWave);
            yield return wait;
            currentWave++;
            if (currentWave >= wavesForLevel.waves.Count)
            {
                if (!wasBossSpawned)
                {
                    SpawnBoss();
                }
                currentWave = wavesForLevel.waves.Count;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        wasBossSpawned = false;
    }

    private void Start()
    {
        StartCoroutine(WavesCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (PlayerController.Instance != null)
        {
            Gizmos.DrawWireSphere(PlayerController.Instance.transform.position, spawnDistanceFromPlayer);
        }
    }
}
