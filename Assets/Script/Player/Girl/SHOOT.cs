using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public void FireBullet()
    {
        // الوصول إلى سكربت الأب الذي يحتوي على منطق الإطلاق
        PlayerGunAttack gunAttack = GetComponentInParent<PlayerGunAttack>();
        if (gunAttack != null)
        {
            gunAttack.FireBullet();
        }
        else
        {
            Debug.LogWarning("PlayerGunAttack not found in parent!");
        }
    }
}
