using UnityEngine;

public class lightShoot : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 30f;
    [SerializeField] float bulletLifetime = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;

        Destroy(gameObject, bulletLifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // غيري الرقم لو بدك ضرر أكثر
            }
        }

        Destroy(gameObject); // تتدمر بعد أول اصطدام
    }
}
