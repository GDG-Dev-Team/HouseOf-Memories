using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool wasGroundedLastFrame;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform groundPoint;

    [SerializeField]
    private float pointRadius;

    [SerializeField]
    private LayerMask groundLayer;

    private int jumpCount = 0;

    
    [SerializeField] private int maxConsecutiveJumps = 2;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();

        // Reset jump count if we've just landed
        if (isGrounded && !wasGroundedLastFrame)
        {
            jumpCount = 0;
        }

        wasGroundedLastFrame = isGrounded;
        
    }

    public void Jump1(InputAction.CallbackContext context)
    {
    
        if (context.performed && jumpCount < maxConsecutiveJumps)
        {
            JumpOnce();
            jumpCount++;
        }
        
    }

    void JumpOnce()
    {
         rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPoint.position, pointRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(groundPoint.position, pointRadius);
    }

}

