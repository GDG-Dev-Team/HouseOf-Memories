//using UnityEngine;

//public class AttackEventBridge : MonoBehaviour
//{
//    public Animator animator;
//    public PlayerAttack playerAttack;

//    void Update()
//    {
//        // نشغل الأنميشن فقط إن طلب الأب ذلك
//        if (playerAttack.isAttacking)
//        {
//            animator.SetTrigger("Attack");
//        }
//    }

//    // تستدعى من Animation Event
//    public void DoAttack()
//    {
//        playerAttack.DoAttack();
//    }

//    // نهاية الأنميشن
//    public void EndAttack()
//    {
//        playerAttack.EndAttack();
//    }
//}
