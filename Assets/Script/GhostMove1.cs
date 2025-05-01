using UnityEngine;
using UnityEngine.UIElements;

public class GhostMove1 : MonoBehaviour
{
    //For Idle Stage
    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirction;

    //For Attack Up and Down Stage
    [Header("AttackUpNDown")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirction;

    //For Attack Player S tage 
    [Header("AttackPlayer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform Player;

    //Other
    [Header("Other")]
    [SerializeField] Transform GroundCheckUp;
    [SerializeField] Transform GroundCheckDown;
    [SerializeField] Transform GroundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goingUp = true;
    private bool FacingLeft = true;
    private Rigidbody2D enemyRB;


    void Start()
    {
        idleMoveDirction.Normalize();
        attackMoveDirction.Normalize();
        enemyRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
      isTouchingUp   = Physics2D.OverlapCircle(GroundCheckUp.position, groundCheckRadius, groundLayer);
      isTouchingDown = Physics2D.OverlapCircle(GroundCheckDown.position, groundCheckRadius, groundLayer);
      isTouchingWall = Physics2D.OverlapCircle(GroundCheckWall.position, groundCheckRadius, wallLayer);
        IdleState();
        //AttackUpNDown();
   }

    void IdleState()
    {
        if (isTouchingUp && goingUp) 
            ChangeDirection();
        else if (isTouchingDown && !goingUp)
            ChangeDirection();

        if (isTouchingWall)
        {
            if (FacingLeft)
                Flip();
            else if (!FacingLeft)
                Flip();
        }  
     enemyRB.linearVelocity = idleMoveSpeed * idleMoveDirction; 
    }

    //void AttackUpNDown()
    //{
    //    if (isTouchingUp && goingUp)
    //        ChangeDirection();
    //    else if (isTouchingDown && !goingUp)
    //        ChangeDirection();

    //    if (isTouchingWall)
    //    {
    //        if (FacingLeft)
    //            Flip();
    //        else if (!FacingLeft)
    //            Flip();
    //    }
    //    enemyRB.linearVelocity = attackMoveSpeed * attackMoveDirction;
    //}

    void ChangeDirection()
    {
        goingUp = !goingUp;
        idleMoveDirction.y *= -1;
        attackMoveDirction.y *= -1;
    }

    void Flip()
    {
        FacingLeft = !FacingLeft;
        idleMoveDirction.x *= -1;
        attackMoveDirction.x *= -1;
        transform.transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GroundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(GroundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(GroundCheckWall.position, groundCheckRadius);
    }

    void FixedUpdate()
    {
       // IdleState();
    }
}
