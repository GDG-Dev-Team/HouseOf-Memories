using UnityEngine;

public class AttackEventBridge : MonoBehaviour
{
    public PlayerAnim playerAnim; // ربط السكربت الأصلي

    public void EndAttack()
    {
        if (playerAnim != null)
        {
            playerAnim.EndAttack(); // استدعاء الدالة من الأب
        }
    }
}
