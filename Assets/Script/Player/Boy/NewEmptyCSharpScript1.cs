using UnityEngine;
using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    public Animator animator; // من الابن
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    [Header("Attack Settings")]
    public float attackDuration = 0.5f;
    public Transform attackPoint; // نقطة الضرب
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public int damage = 1;

    private bool isAttacking = false;

    void Update()
    {
        if (isAttacking) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // انتبهي: velocity وليس linearVelocity

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());
        }
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // ننتظر لحظة ليتزامن مع الضربة
        yield return new WaitForSeconds(attackDuration / 3f);

        DoAttack();

        // ننتظر بقية الأنميشن قبل السماح بالحركة
        yield return new WaitForSeconds(attackDuration * 2f / 3f);

        isAttacking = false;
    }

    void DoAttack()
    {
        // كشف كل الأعداء ضمن دائرة الضرب
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("ضرب العدو: " + enemy.name);

            // إرسال الضرر للعدو
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
