using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public bool isAttacking = false;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    void Update()
    {
        if (isAttacking) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // زر الهجوم
        if (Input.GetKeyDown(KeyCode.X))
        {
            isAttacking = true;
            // هنا لا نشغل الأنميشن، بل نتركه للابن عبر سكربت خاص
        }
    }

    // تُستدعى من سكربت الابن عند لحظة الضربة
    public void DoAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("ضرب العدو: " + enemy.name);
            // enemy.GetComponent<Enemy>()?.TakeDamage(1);
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
