using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
      private Rigidbody2D rb;
    private bool wasGroundedLastFrame;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundPoint;
    [SerializeField] private float pointRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxConsecutiveJumps = 2;

 
  [Header("Sound Effects")]
    [SerializeField] private AudioClip jumpSound; // The sound clip for jumping.
    private AudioSource audioSource;
    private int jumpCount = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();

        if (isGrounded && !wasGroundedLastFrame)
        {
            jumpCount = 0;
        }

        wasGroundedLastFrame = isGrounded;
    }

    // ✅ No callback context — plain void method
    public void HandleJump1()
    {
         Debug.Log("Jump input received");
 if (jumpSound != null)
               {  audioSource.PlayOneShot(jumpSound); }

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

