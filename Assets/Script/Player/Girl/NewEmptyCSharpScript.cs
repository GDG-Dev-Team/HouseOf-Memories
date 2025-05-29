using UnityEngine;

public class SimplePlayerAnim : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    private bool wasFalling = false;
    private bool wasJumping = false;

    void Update()
    {
        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", speed);

        float verticalVelocity = rb.linearVelocity.y;

        if (verticalVelocity > 0.1f && !wasJumping)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
            wasJumping = true;
            wasFalling = false;
        }
        else if (verticalVelocity < -0.1f && !wasFalling)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
            wasFalling = true;
            wasJumping = false;
        }
        else if (Mathf.Abs(verticalVelocity) <= 0.1f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            wasJumping = false;
            wasFalling = false;
        }
    }
}
