using UnityEngine;

public class EnemyShootingFar : MonoBehaviour
{
    [Header("TargetToFollow")]
    public float LineOfSite;
    private Transform player; // target

    [Header("Shoot")]
    public float shootingRange;
    public GameObject bullet; // bullet prefab
    public GameObject bulletParent; // spawn point
    [SerializeField] float shootCooldown = 1f;
    private float nextFireTime;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = GetClosestPlayer(players).transform;
    }

    void Update()
    {
        if (player == null) return;

        // Step 1: Log the player's position
        Debug.Log("?? Player is at: " + player.position);

        float distanceFormPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFormPlayer <= shootingRange && nextFireTime < Time.time)
        {
            // Step 2: Calculate direction to player
            Vector2 direction = (player.position - bulletParent.transform.position).normalized;

            // Step 3: Log direction
            Debug.Log("?? Bullet direction = " + direction);

            // Step 4: Calculate rotation angle (optional)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Step 5: Spawn bullet and log position
            GameObject b = Instantiate(bullet, bulletParent.transform.position, Quaternion.Euler(0, 0, angle));
            Debug.Log("?? Bullet spawned at: " + bulletParent.transform.position);

            // Step 6: Apply velocity
            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 7f;
                Debug.Log("?? Bullet velocity = " + rb.linearVelocity);
            }

            nextFireTime = Time.time + shootCooldown;
        }
    }

    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(p.transform.position, currentPos);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = p;
            }
        }

        return closest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}