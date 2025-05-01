using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform groundPoint;

    [SerializeField]
    private float pointRadius;

    [SerializeField]
    private LayerMask groundLayer;

    public void Jumping(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            PerformJump();
        }
    }

    void PerformJump()
    {
        Vector2 jumpDirection = new(0, jumpForce);
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
