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
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // Width & height of room
    [SerializeField] private float spawnZ = 0f;

    private List<GameObject> currentEnemies = new List<GameObject>();

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
