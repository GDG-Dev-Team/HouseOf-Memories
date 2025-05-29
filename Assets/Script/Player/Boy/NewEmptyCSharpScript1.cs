using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    public Animator animator; // من الابن
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float attackDuration = 0.5f; // مدة أنميشن الهجوم (اضبطها يدويًا)

    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());
        }
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // انتظر مدة الأنميشن ثم ارجع للحركة
        yield return new WaitForSeconds(attackDuration);

        isAttacking = false;
    }
}
