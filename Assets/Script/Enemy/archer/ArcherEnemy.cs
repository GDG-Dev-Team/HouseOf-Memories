using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float arrowSpeed = 10f;
    public float shootCooldown = 2f;
    public float attackRange = 15f;

    private float nextFireTime;
    private Transform target;

    void Update()
    {
        FindClosestPlayer();

        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + shootCooldown;
        }
    }

    void Shoot()
    {
        Vector2 direction = (target.position - shootPoint.position).normalized;

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * arrowSpeed;
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDist = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject player in players)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = player.transform;
            }
        }

        target = closest;
    }
}
