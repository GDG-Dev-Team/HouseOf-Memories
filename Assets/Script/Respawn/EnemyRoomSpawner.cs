using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRoomSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs to spawn")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    public float boundaryPadding = 2f;

    [Header("Room Settings")]
    public int maxEnemies = 3;
    public float respawnDelay = 5f;
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // Width & height of room
   

    private List<GameObject> currentEnemies = new List<GameObject>();
    private Rect confinementRect;
    
   // private Rect spawnRect;
    
   // [SerializeField] private float spawnZ = 0f;

   // private void Awake()
   // {
   //      Vector2 center = new Vector2(transform.position.x, transform.position.y);
   // <summary>
   //    float minX = center.x - spawnAreaSize.x / 2f;
   // </summary>
   //     float minY = center.y - spawnAreaSize.y / 2f;
   //     spawnRect = new Rect(minX, minY, spawnAreaSize.x, spawnAreaSize.y);
   // }
   
    private void Awake()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);
        float minX = center.x - spawnAreaSize.x / 2f;
        float minY = center.y - spawnAreaSize.y / 2f;
        confinementRect = new Rect(minX, minY, spawnAreaSize.x, spawnAreaSize.y);
    }
   

    private void Start()
    {

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("ERROR: No spawn points assigned to the EnemyRoomSpawner on object '" + gameObject.name + "'. Please assign some in the Inspector.");
            this.enabled = false; // Disable the script to prevent errors.
            return;
        }

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

        Vector3 spawnPos = GetRandomSpawnPointPosition();
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);
        currentEnemies.Add(enemy);

      

        EnemyMove moveScript = enemy.GetComponent<EnemyMove>();

        if (moveScript != null)
        {
            // Optionally set movement bounds or other properties here
           // moveScript.MovementsBounds = spawnRect;
           moveScript.MovementsBounds = confinementRect;
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

    private Vector3 GetRandomSpawnPointPosition()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
       
       // 2. Get the Transform at that random index.
       Transform selectedPoint = spawnPoints[randomIndex];
       
       // 3. Return its position.
       return selectedPoint.position;
    }

    private void OnDrawGizmosSelected()
    {
       // Draw the confinement area (the RED BOX)
        Gizmos.color = new Color(1, 0, 0, 0.5f); // Red
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);

        // Draw the spawn points (the GREEN SPHERES)
        Gizmos.color = new Color(0, 1, 0, 0.7f); // Green
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            foreach (Transform point in spawnPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawSphere(point.position, 0.5f);
                }
            }
        }
    }
}
