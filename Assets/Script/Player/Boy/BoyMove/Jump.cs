using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
        private Rigidbody2D rb;
    private bool wasGroundedLastFrame;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private float pointRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxConsecutiveJumps = 2;

    private int jumpCount = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ✅ Handle input using the old Input system
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryJump();
        }

        // Ground check
        bool isGrounded = IsGrounded();
        if (isGrounded && !wasGroundedLastFrame)
        {
            jumpCount = 0;
        }

        wasGroundedLastFrame = isGrounded;
    }

    void TryJump()
    {
        if (jumpCount < maxConsecutiveJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundPoint.position, pointRadius, groundLayer);
    }

    void OnDrawGizmos()
    {
        if (groundPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(groundPoint.position, pointRadius);
        }
    }

}

