using UnityEngine;

public class AttackEventBridge : MonoBehaviour
{
    public PlayerAnim playerAnim; // هذا هو الأب الذي يحمل الدالة

    public void DoAttack()
    {
        if (playerAnim != null)
        {
            playerAnim.DoAttack(); // ننفذ الدالة في الأب
        }
    }

    public void EndAttack()
    {
        if (playerAnim != null)
        {
            playerAnim.EndAttack();
        }
    }
}
