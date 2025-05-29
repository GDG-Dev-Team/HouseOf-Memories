//using UnityEngine;

//public class PlayerAttack : MonoBehaviour
//{
//    public Transform attackPoint;         // نقطة الضربة
//    public float attackRange = 0.5f;      // نصف قطر الضربة
//    public LayerMask enemyLayer;          // طبقة الأعداء

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.X))  // زر الهجوم
//        {
//            Attack();
//        }
//    }

//    void Attack()
//    {
//        // العثور على كل الأعداء ضمن منطقة الضربة
//        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

//        foreach (Collider2D enemyCollider in hitEnemies)
//        {
//            Enemy enemy = enemyCollider.GetComponent<Enemy>();
//            if (enemy != null)
//            {
//                enemy.TakeDamage(1); // أنقص من صحتهم 1
//            }
//        }
//    }

//    void OnDrawGizmosSelected()
//    {
//        if (attackPoint == null) return;
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//    }
//}
