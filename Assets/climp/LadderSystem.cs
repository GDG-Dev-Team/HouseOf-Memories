using UnityEngine;

public class LadderSystem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private LayerMask ladderLayer;

    [Header("Settings")]
    [SerializeField] private float detectionRange = 0.5f;

    private bool isOnLadder = false;
    private Collider2D currentLadder;

    void Update()
    {
        HandleLadderInteraction();
        HandleJump();
    }

    void HandleLadderInteraction()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        // الكشف عن السلالم في النطاق
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.up,
            detectionRange,
            ladderLayer
        );

        if (hit.collider != null)
        {
            currentLadder = hit.collider;
            if (verticalInput != 0)
            {
                IgnoreCollision(true);
                isOnLadder = true;
            }
        }
        else if (isOnLadder)
        {
            IgnoreCollision(false);
            isOnLadder = false;
            currentLadder = null;
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && !isOnLadder)
        {
            // الكشف عن السلالم خلف اللاعب
            Vector2 direction = transform.right * (transform.localScale.x > 0 ? 1 : -1);
            RaycastHit2D behindHit = Physics2D.Raycast(
                transform.position,
                -direction,
                detectionRange,
                ladderLayer
            );

            if (behindHit.collider != null)
            {
                IgnoreCollision(false);
            }
        }
    }

    void IgnoreCollision(bool ignore)
    {
        if (currentLadder != null)
        {
            Physics2D.IgnoreCollision(playerCollider, currentLadder, ignore);
        }
    }

    void OnDrawGizmos()
    {
        // رسم خطوط للمساعدة في التصحيح
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * detectionRange);

        Vector2 direction = transform.right * (transform.localScale.x > 0 ? 1 : -1);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(-direction * detectionRange));
    }
}