using Mono.Cecil;
using UnityEngine;

public class lightShoot : MonoBehaviour
{
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float bulletLifetime = 2f;
    private Rigidbody2D rb;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 force = transform.right * bulletSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);
        Destroy(gameObject, bulletLifetime);

    }


    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
        }

        Destroy(gameObject);
    }
}




