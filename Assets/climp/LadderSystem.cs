using UnityEngine;

public class LadderSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerMove playerMovement;

    [Header("Settings")]
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private float climbSpeed = 4f;

    private bool isOnLadder = false;
    private bool isClimbing = false;
    private Collider2D currentLadder;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & ladderLayer) != 0)
        {
            isOnLadder = true;
            currentLadder = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & ladderLayer) != 0)
        {
            isOnLadder = false;
            StopClimbing();
        }
    }

    void HandleLadderInput()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (isOnLadder)
        {
            if (verticalInput != 0 && !isClimbing)
            {
                StartClimbing();
            }
            else if (verticalInput == 0 && isClimbing)
            {
                StopClimbing();
            }
        }
    }

    void StartClimbing()
    {
        isClimbing = true;
        Physics2D.IgnoreCollision(playerCollider, currentLadder, true);
        playerRigidbody.gravityScale = 0;
        playerRigidbody.linearVelocity = Vector2.zero;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    void ClimbLadder()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        playerRigidbody.linearVelocity = new Vector2(0, verticalInput * climbSpeed);
    }

    void StopClimbing()
    {
        isClimbing = false;
        Physics2D.IgnoreCollision(playerCollider, currentLadder, false);
        playerRigidbody.gravityScale = 1;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }
}