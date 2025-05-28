using UnityEngine;

public class Spider : MonoBehaviour
{
    [Header("For Petrolling")]
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

    [Header("TargetToFollow")]
    public float speed;
    public float LineOfSite;
    private Transform player;//target

    [Header("Jumping")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpRange;
    [SerializeField] float jumpCooldown = 2f;
    private float lastJumpTime;

    [Header("Other")]
    private Animator enemyAnim;
    private Rigidbody2D enemyRB;

  private Animator EnemyAnim;
    void Start()
    {
       EnemyAnim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, wallLayer);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = GetClosestPlayer(players).transform;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < LineOfSite)
        {
            if (distanceFromPlayer < jumpRange && Time.time > lastJumpTime + jumpCooldown)
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
            petrolling();
        }
    }

    void JumpAtPlayer()
    {
        if (checkingGround) // Jump only if on ground
        {
            EnemyAnim.SetTrigger("IsAttacking");
            Vector2 jumpDirection = (player.position - transform.position).normalized;
            enemyRB.linearVelocity = new Vector2(0, 0); // optional: reset velocity before jump
            enemyRB.AddForce(new Vector2(jumpDirection.x, 1f) * jumpForce, ForceMode2D.Impulse);
        }
    }


    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject GirlOrBoy in players)
        {
            float distance = Vector3.Distance(GirlOrBoy.transform.position, currentPos);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = GirlOrBoy;
            }
        }

        return closest;
    }

    void FollowPlayer()
    {
        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }

        // ����� ������ ������
        enemyRB.linearVelocity = new Vector2(moveSpeed * moveDirection, enemyRB.linearVelocity.y);
    }


    void petrolling()
        {
            if (!checkingGround || checkingWall)
            {
                if (facingRight)
                {
                    Flip();
                }
                else if (!facingRight)
                {
                    Flip();
                }
            }
            EnemyAnim.SetBool("IsIdle", true);
        
            enemyRB.linearVelocity = new Vector2(moveSpeed * moveDirection, enemyRB.linearVelocity.y);
        }
    



    private void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }


    public void DealDamage()
    {
        if (Vector2.Distance(player.position, transform.position) <= 1.5f) // حسب منطقك
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Ground check + Wall check
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        // Line of sight (follow player)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        // Jump range (start jumping)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, jumpRange);


    }
}
