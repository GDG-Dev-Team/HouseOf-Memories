using UnityEngine;

public class PlayerAttackshoot : MonoBehaviour
{
    [Header("Gun Variable")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float shootCooldown = 0.3f; // بين كل طلقة وطلقة
    private float nextFireTime = 0f;



    void Update()
    {
        if (Time.time >= nextFireTime && Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
            nextFireTime = Time.time + shootCooldown;
        }
    }


    private void Shoot()
    {
        // إنشاء الطلقة
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // تحديد اتجاه الطلقة حسب لف البنت
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = playerTransform.localScale.x > 0 ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);
        }

        // تعديل شكل الطلقة بصريًا (إذا فيها Sprite)
        Vector3 scale = bullet.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(playerTransform.localScale.x);
        bullet.transform.localScale = scale;
    }
}
