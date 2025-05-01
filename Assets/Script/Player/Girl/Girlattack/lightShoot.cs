using Mono.Cecil;
using UnityEngine;

public class lightShoot : MonoBehaviour
{
    [SerializeField] float bulletDamage = 10f;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] float lifeTime = 3f;
    private Rigidbody2D rb;
    

    

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 force = transform.right * bulletSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);

       
    }

    
    private void OnCollisionEnter2D(Collider2D collision)
    {
       Destroy(gameObject);
        if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemycomponent))
        {
            enemycomponent.TakeDamage(1);
        }

       
    }


    private void OnTrigerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemycomponent))
        {
            enemycomponent.TakeDamage(1);
        }


    }

}


