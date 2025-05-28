using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    void Update()
    {
        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("Speed", speed);

        // التحقق من القفز
        animator.SetBool("IsJumping", rb.linearVelocity.y > 0.1f);
        animator.SetBool("IsFalling", rb.linearVelocity.y < -0.1f);
    }
}
