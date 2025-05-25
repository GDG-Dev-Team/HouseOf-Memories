using UnityEngine;

public class EnemyShootingFar : MonoBehaviour
{
    [Header("TargetToFollow")]
    public float LineOfSite;
    private Transform player;

    [Header("Shoot")]
    public float shootingRange = 50f; // Increased for testing
    public GameObject bullet;
    public GameObject bulletParent;
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

        float distanceFormPlayer = Vector2.Distance(player.position, transform.position);

        // Log the player’s position and distance to them
        Debug.Log("🧍 Player is at: " + player.position);
        Debug.Log("📏 Distance to player = " + distanceFormPlayer);

        // ✅ Now using both distance and time conditions
        if (distanceFormPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Vector2 direction = (player.position - bulletParent.transform.position).normalized;
            Debug.Log("📐 Bullet direction = " + direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject b = Instantiate(bullet, bulletParent.transform.position, Quaternion.Euler(0, 0, angle));
            Debug.Log("🚀 Bullet spawned at: " + bulletParent.transform.position);

            Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 7f;
                Debug.Log("🎯 Bullet velocity = " + rb.linearVelocity);
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
