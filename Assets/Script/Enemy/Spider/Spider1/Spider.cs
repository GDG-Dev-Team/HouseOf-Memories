using UnityEngine;

public class Spider : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private float moveDirection = 1f;
    private bool facingRight = true;
    private bool checkingGround;
    private bool checkingWall;

    [Header("Player Detection")]
    public float lineOfSight = 8f;
    public float attackRange = 2f;
    private Transform player;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpCooldown = 2f;
    private float lastJumpTime = -Mathf.Infinity;

    [Header("Other")]
    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, wallLayer);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = GetClosestPlayer(players).transform;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < lineOfSight)
        {
            if (distance <= attackRange && Time.time > lastJumpTime + jumpCooldown)
            {
                JumpAtPlayer();
                lastJumpTime = Time.time;
            }
            else
            {
                FollowPlayer();
            }
        }
        else
        {
            Patrol();
        }
    }

    void JumpAtPlayer()
    {
        if (!checkingGround) return;

        anim.SetTrigger("IsAttacking");

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(direction.x, 1f) * jumpForce, ForceMode2D.Impulse);
    }

    void FollowPlayer()
    {
        if (player.position.x > transform.position.x && !facingRight) Flip();
        if (player.position.x < transform.position.x && facingRight) Flip();

        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    void Patrol()
    {
        if (!checkingGround || checkingWall) Flip();
        anim.SetBool("IsIdle", true);
        rb.linearVelocity = new Vector2(moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject closest = null;
        float shortest = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject p in players)
        {
            float dist = Vector3.Distance(p.transform.position, currentPos);
            if (dist < shortest)
            {
                shortest = dist;
                closest = p;
            }
        }

        return closest;
    }

    // Called from Animation Event
    public void DealDamage()
    {
        if (player != null && Vector2.Distance(player.position, transform.position) <= attackRange)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
    }
}
