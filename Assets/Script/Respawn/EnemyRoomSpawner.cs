using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRoomSpawner : MonoBehaviour
{
[Header("Enemy Prefabs to spawn")]
    public GameObject[] enemyPrefabs;

    [Header("Room Settings")]
    public int maxEnemies = 3;
    public float respawnDelay = 5f;
    private Rect spawnRect;
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // Width & height of room
    [SerializeField] private float spawnZ = 0f;

    private List<GameObject> currentEnemies = new List<GameObject>();


    private void Awake()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);
        float minX = center.x - spawnAreaSize.x / 2f;
        float minY = center.y - spawnAreaSize.y / 2f;
        spawnRect = new Rect(minX, minY, spawnAreaSize.x, spawnAreaSize.y);
    }
    private void Start()
    {
        StartCoroutine(InitialSpawn());
    }

    private IEnumerator InitialSpawn()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            yield return new WaitForSeconds(0.2f); // Stagger spawns slightly
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || currentEnemies.Count >= maxEnemies) return;

        Vector3 spawnPos = GetRandomPositionInArea();
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        currentEnemies.Add(enemy);

      

        EnemyMove moveScript = enemy.GetComponent<EnemyMove>();

        if (moveScript != null)
        {
            // Optionally set movement bounds or other properties here
            moveScript.MovementsBounds = spawnRect;
        }
        else
        {
            Debug.LogWarning("Spawned enemy does not have an EnemyMove script.");
        }

      // Monitor death automatically
        EnemyDeathWatcher watcher = enemy.AddComponent<EnemyDeathWatcher>();
        watcher.spawner = this;
        
        }

    public void NotifyEnemyDied(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }

    private Vector3 GetRandomPositionInArea()
    {
       Vector2 offset = new Vector2(
        Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
        Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
    );

    Vector3 spawnPosition = transform.position + (Vector3)offset;
    spawnPosition.z = spawnZ; // force Z position
    return spawnPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
