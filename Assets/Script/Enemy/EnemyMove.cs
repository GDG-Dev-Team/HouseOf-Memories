using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("For Respawn")]
    [SerializeField] public Rect MovementsBounds;

    [Header("For Patrolling")]
    [SerializeField] float patrolSpeed;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private float moveDirection = 1f;
    private bool facingRight = true;
    private bool checkingGround;
    private bool checkingWall;

    [Header("Target To Follow")]
    public float chaseSpeed;
    public float LineOfSite;
    private Transform player;

    [Header("For Attack")]
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackCooldown = 1f;
    private float lastAttackTime = -Mathf.Infinity;
    private Animator anim;

    [Header("Other")]
    private Rigidbody2D enemyRB;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, wallLayer);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) return;

        player = GetClosestPlayer(players).transform;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < LineOfSite)
        {
            if (distanceFromPlayer <= attackRange)
            {
                enemyRB.linearVelocity = Vector2.zero;

                // يهجم فقط إذا انتهى وقت الانتظار بين الهجمات
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    anim.SetBool("isAttacking", true);
                    lastAttackTime = Time.time; // نبدأ العدّ من جديد بعد كل ضربة
                }
                else
                {
                    anim.SetBool("isAttacking", false);
                }
            }
            else
            {
                anim.SetBool("isAttacking", false);
                FollowPlayer();
            }
        }
        else
        {
            anim.SetBool("isAttacking", false);
            Patrolling();
        }
    }




        void LateUpdate()
    {
        if (MovementsBounds.width == 0) return;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, MovementsBounds.xMin, MovementsBounds.xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, MovementsBounds.yMin, MovementsBounds.yMax);
        transform.position = clampedPosition;
    }

    public void DealDamage()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (player != null && Vector2.Distance(player.position, transform.position) <= attackRange)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1);
                    lastAttackTime = Time.time;
                }
            }
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

    void FollowPlayer()
    {
        moveDirection = (player.position.x > transform.position.x) ? 1f : -1f;

        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();

        enemyRB.linearVelocity = new Vector2(chaseSpeed * moveDirection, enemyRB.linearVelocity.y);
    }

    void Patrolling()
    {
        if (!checkingGround || checkingWall)
            Flip();

        enemyRB.linearVelocity = new Vector2(patrolSpeed * moveDirection, enemyRB.linearVelocity.y);
    }

    private void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

     

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
