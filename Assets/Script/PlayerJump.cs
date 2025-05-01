using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform groundPoint;

    [SerializeField]
    private float pointRadius;

    [SerializeField]
    private LayerMask groundLayer;

    private int jumpCount = 0;
    private int maxJumps = 2;

    void Update()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && jumpCount < maxJumps)
        {
            PerformJump();
            jumpCount++;
        }
    }

    void PerformJump()
    {
        Vector2 jumpDirection = new(0, jumpForce);
        PlayerMove.rb2d.linearVelocity = new Vector2(PlayerMove.rb2d.linearVelocity.x, 0);
        PlayerMove.rb2d.AddForce(jumpDirection, ForceMode2D.Impulse);
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
