using UnityEngine;

public class EnemyMove : MonoBehaviour
{
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
    private Animator anim;
    private bool isAttacking = false;

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
        player = GetClosestPlayer(players).transform;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < LineOfSite)
        {
            if (distanceFromPlayer <= attackRange)
            {
                // enemyRB.linearVelocity = Vector2.zero;
                anim.SetBool("isAttacking", true);
            }
            else
            {
                anim.SetBool("isAttacking", false);
                FollowPlayer();
            }

        }
        else
        {
            isAttacking = false;
            Patrolling(); // يرجع يدوّر
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
            moveDirection = (player.position.x > transform.position.x) ? 1f : -1f;
            if (player.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
            else if (player.position.x < transform.position.x && facingRight)
            {
                Flip();
            }

            enemyRB.linearVelocity = new Vector2(chaseSpeed * moveDirection, enemyRB.linearVelocity.y);
        }

        void Patrolling()
        {
            if (!checkingGround || checkingWall)
            {
                Flip();
            }

            enemyRB.linearVelocity = new Vector2(patrolSpeed * moveDirection, enemyRB.linearVelocity.y);
        }
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
