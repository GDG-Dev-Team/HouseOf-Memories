using UnityEngine;

public enum PlayerState { Ground, OnLadder }

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float ladderClimbSpeed = 3f;
    public float ladderJumpEntryForce = 2f;

    [Header("Components")]
    public LayerMask ladderLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private Collider2D currentLadder;
    private PlayerState state = PlayerState.Ground;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        CheckGround();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleInput()
    {
        // القفز
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // التفاعل مع الدرج
        if (Input.GetKeyDown(KeyCode.W)) TryEnterLadder();
        if (Input.GetKeyDown(KeyCode.S)) TryExitLadder();
    }

    void TryEnterLadder()
    {
        if (currentLadder == null) return;

        // الانتقال إلى حالة الصعود
        state = PlayerState.OnLadder;
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;

        // قفزة صغيرة للبداية
        rb.AddForce(Vector2.up * ladderJumpEntryForce, ForceMode2D.Impulse);

        // نقل اللاعب إلى مقدمة الدرج
        Vector2 entryPoint = currentLadder.bounds.min + new Vector3(0.1f, 0);
        transform.position = entryPoint;
    }

    void TryExitLadder()
    {
        if (state != PlayerState.OnLadder) return;

        // العودة إلى الحالة العادية
        state = PlayerState.Ground;
        rb.gravityScale = 1;
        currentLadder = null;
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (state == PlayerState.OnLadder)
        {
            // حركة على الدرج
            float verticalInput = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(moveInput * ladderClimbSpeed,
                                     verticalInput * ladderClimbSpeed);
        }
        else
        {
            // حركة عادية
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            currentLadder = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            currentLadder = null;
        }
    }
}