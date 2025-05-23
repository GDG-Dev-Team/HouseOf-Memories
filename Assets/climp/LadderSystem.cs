using UnityEngine;

public class LadderSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerMove playerMovement; // افترض أن لديك سكريبت لحركة اللاعب

    [Header("Settings")]
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float detectionRadius = 0.3f;

    private bool isClimbing = false;
    private Collider2D currentLadder;
    private float verticalInput;

    void Update()
    {
        HandleLadderInput();
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            ClimbLadder();
        }
    }

    void HandleLadderInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        // الكشف عن السلم عند الضغط على W/S
        if (verticalInput != 0 && !isClimbing)
        {
            Collider2D ladder = Physics2D.OverlapCircle(transform.position, detectionRadius, ladderLayer);
            if (ladder != null)
            {
                StartClimbing(ladder);
            }
        }

        // إيقاف التسلق عند الوصول للنهاية
        if (isClimbing && currentLadder != null)
        {
            Bounds ladderBounds = currentLadder.bounds;
            if (transform.position.y > ladderBounds.max.y || transform.position.y < ladderBounds.min.y)
            {
                StopClimbing();
            }
        }
    }

    void StartClimbing(Collider2D ladder)
    {
        isClimbing = true;
        currentLadder = ladder;
        Physics2D.IgnoreCollision(playerCollider, ladder, true);

        // تعطيل حركة اللاعب الأفقي
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        playerRigidbody.gravityScale = 0;
        playerRigidbody.linearVelocity = Vector2.zero;
    }

    void ClimbLadder()
    {
        playerRigidbody.linearVelocity = new Vector2(0, verticalInput * climbSpeed);
    }

    void StopClimbing()
    {
        isClimbing = false;
        Physics2D.IgnoreCollision(playerCollider, currentLadder, false);

        // إعادة تفعيل حركة اللاعب
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
        playerRigidbody.gravityScale = 5;
        currentLadder = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}