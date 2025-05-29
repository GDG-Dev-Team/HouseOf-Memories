using UnityEngine;

public class PlayerGunAttack : MonoBehaviour
{
    [Header("Gun Variables")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float shootCooldown = 0.3f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private float nextFireTime = 0f;

    private void Start()
    {
        // البحث عن Animator في الأبناء إن لم يتم تعيينه يدويًا
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        if (Time.time >= nextFireTime && Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null)
                animator.SetTrigger("Shoot"); // سيشغّل أنميشن يحتوي على Event

            nextFireTime = Time.time + shootCooldown;
        }
    }

    // يتم استدعاؤها من Animation Event
    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = playerTransform.localScale.x > 0 ? 1f : -1f;
            rb.linearVelocity = new Vector2(direction * bulletSpeed, 0f);
        }

        // تدوير الطلقة بحسب اتجاه اللاعب
        Vector3 scale = bullet.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(playerTransform.localScale.x);
        bullet.transform.localScale = scale;

        Destroy(bullet, 5f);
    }
}
