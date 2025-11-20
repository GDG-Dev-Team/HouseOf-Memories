using UnityEngine;

public class PlayerGunAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bulletSpeed = 30f;

    [SerializeField] private Animator animator;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    [Range(0f, 1f)]
    [SerializeField] private float shootVolume = 0.7f;

    private float shootCooldown = 0.3f;
    private float nextFireTime = 0f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime && Input.GetKeyDown(KeyCode.E))
        {
            if (animator != null) animator.SetTrigger("Shoot");
            nextFireTime = Time.time + shootCooldown;
        }
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float dir = playerTransform.localScale.x > 0 ? 1f : -1f;
            rb.linearVelocity = new Vector2(dir * bulletSpeed, 0f);
        }

        Vector3 scale = bullet.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(playerTransform.localScale.x);
        bullet.transform.localScale = scale;

        if (audioSource != null && shootClip != null)
            audioSource.PlayOneShot(shootClip, shootVolume);

        Destroy(bullet, 5f);
    }
}
