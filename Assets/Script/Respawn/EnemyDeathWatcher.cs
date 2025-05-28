using UnityEngine;

public class EnemyDeathWatcher : MonoBehaviour
{
     public EnemyRoomSpawner spawner;

    private void OnDestroy()
    {
        if (spawner != null && Application.isPlaying)
        {
            spawner.NotifyEnemyDied(gameObject);
        }
    }
}
