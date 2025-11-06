using UnityEngine;

public class GameMaster : MonoBehaviour
{

    [Header("Scene Control")]
    [Tooltip("The list of all enemy spawners this Game Master will control.")]
    public EnemyRoomSpawner[] controlledSpawners;

    [Tooltip("The delay in seconds after the scene starts before activating the spawners.")]
    public float initialSpawnDelay = 60f; // Default is 1 minute


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (EnemyRoomSpawner spawner in controlledSpawners)
        {
            if (spawner != null)
            {
                spawner.enabled = false;
            }
        }

        StartCoroutine(EnableSpawnersAfterDelay());
    }
    private System.Collections.IEnumerator EnableSpawnersAfterDelay()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        foreach (EnemyRoomSpawner spawner in controlledSpawners)
        {
            if (spawner != null)
            {
                spawner.enabled = true;
            }
        }
    }
}
