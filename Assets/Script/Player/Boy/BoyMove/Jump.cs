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

    private Animator anim;

    private Rigidbody2D rb;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsGrounded())
        {
            anim.SetBool("IsJumping", false);
        }
    }

    
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
        rb.AddForce(jumpDirection, ForceMode2D.Impulse);
         anim.SetBool("IsJumping", true);
        // play animation here
         
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
