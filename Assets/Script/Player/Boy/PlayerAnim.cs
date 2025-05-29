using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    private bool hasJumped = false;
    private bool hasFallen = false;
    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking) return; // 🚫 منع كل شيء أثناء الهجوم

        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        float yVelocity = rb.linearVelocity.y;

        if (yVelocity > 0.1f && !hasJumped)
        {
            animator.SetTrigger("Jump");
            hasJumped = true;
            hasFallen = false;
        }

        if (yVelocity < -0.1f && !hasFallen)
        {
            animator.SetTrigger("Fall");
            hasFallen = true;
        }

        if (IsGrounded() && Mathf.Abs(yVelocity) < 0.01f)
        {
            hasJumped = false;
            hasFallen = false;
        }

        // الهجوم
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Attack");
            isAttacking = true; // 🚫 نمنع الحركة
        }
    }

    public void EndAttack()
    {
        isAttacking = false; // ✅ السماح بالحركة بعد انتهاء الأنميشن
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }
}
