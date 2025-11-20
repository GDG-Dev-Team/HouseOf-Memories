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

    [Header("Audio")]
    [SerializeField] private AudioClip jumpSound;
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
            jumpCount = 0;

        wasGroundedLastFrame = isGrounded;
    }

    public void HandleJump1()
    {
        if (jumpCount < maxConsecutiveJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;

            // 🔊 صوت القفز
            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
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
