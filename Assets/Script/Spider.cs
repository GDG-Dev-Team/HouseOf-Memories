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


    [Header("Other")]
    private Animator enemyAnim;
    private Rigidbody2D enemyRB;


    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {

        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        petrolling();



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


    }
}
