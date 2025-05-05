using UnityEngine;

public class Spider : MonoBehaviour
{
    [Header("For Petrolling")]
    [SerializeField] float moveSpeed;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private float moveDirection = 1f;
    private bool facingRight = true;
    private bool checkingGround;
    private bool checkingWall;

    [Header("TargetToFollow")]
    public float speed;
    public float LineOfSite;
    private Transform player;//target

    [Header("Other")]
    private Animator enemyAnim;
    private Rigidbody2D enemyRB;


    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = GetClosestPlayer(players).transform;
        enemyRB = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {

        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);

        float distanceFormPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFormPlayer < LineOfSite)
        {
            FollowPlayer();
        }
        else
        {
            petrolling();
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

        // íÊÍÑß ÈÇÊÌÇå ÇááÇÚÈ
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

            enemyRB.linearVelocity = new Vector2(moveSpeed * moveDirection, enemyRB.linearVelocity.y);
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


    }
}
