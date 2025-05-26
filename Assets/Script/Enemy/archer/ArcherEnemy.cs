using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float arrowSpeed = 10f;
    public float shootCooldown = 2f;
    public float attackRange = 10f;
    public float maxLoseTargetRange = 14f;

    [Header("Patrol")]
    public float patrolSpeed = 2f;
    public float patrolDistance = 3f;

    private float nextFireTime;
    private Vector2 startPoint;
    private bool movingRight = true;
    private Transform target;
    private bool isPlayerInRange = false;

    [Header("Dodge When Player Too Close")]
    public float dangerDistance = 2f;
    public float jumpBackForce = 7f;
    public float jumpCooldown = 2f;

    private float nextJumpTime;
    private Rigidbody2D rb;

    void Start()
    {
        startPoint = transform.position;
        FindAndSetTargetOnce();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            isPlayerInRange = true;
            FaceTarget();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + shootCooldown;
            }
        }

        if (distance <= dangerDistance && Time.time >= nextJumpTime)
        {
            JumpBackFromPlayer();
            nextJumpTime = Time.time + jumpCooldown;
        }

        else if (distance > maxLoseTargetRange)
        {
            // اللاعب هرب → ندور واحد جديد
            isPlayerInRange = false;
            FindAndSetTargetOnce();
        }
        else
        {
            isPlayerInRange = false;
        }

        if (!isPlayerInRange)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float dir = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime * dir);

        float distanceFromStart = transform.position.x - startPoint.x;
        if (movingRight && distanceFromStart >= patrolDistance)
        {
            movingRight = false;
            Flip();
        }
        else if (!movingRight && distanceFromStart <= -patrolDistance)
        {
            movingRight = true;
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void FaceTarget()
    {
        if (target == null) return;

        Vector3 scale = transform.localScale;

        if (target.position.x > transform.position.x && scale.x < 0)
        {
            scale.x *= -1;
        }
        else if (target.position.x < transform.position.x && scale.x > 0)
        {
            scale.x *= -1;
        }

        transform.localScale = scale;
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

    void FindAndSetTargetOnce()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Transform closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(p.transform.position, currentPos);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = p.transform;
            }
        }

        target = closest;
    }


    void JumpBackFromPlayer()
    {
        if (target == null || rb == null) return;

        Vector2 directionAway = (transform.position - target.position).normalized;
        rb.AddForce(directionAway * jumpBackForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerDistance);


    }
}
