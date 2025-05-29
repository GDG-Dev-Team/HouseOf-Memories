using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    public Animator animator; // من الابن

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

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(Attack());
        }
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackDuration / 3f);

        DoAttack();

        yield return new WaitForSeconds(attackDuration * 2f / 3f);

        isAttacking = false;
    }

    void DoAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("ضرب العدو: " + enemy.name);

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
