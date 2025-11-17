using UnityEngine;


public class Bullet : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] private float lifeTime = 0.8f;


    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet touched: " + collision.name);

        NewPlayerHealth health = collision.GetComponent<NewPlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
          
        }
        Destroy(gameObject);
  
        /*
                if (collision.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy TAG detected: " + collision.name);
                    EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
                    if (enemy != null)
                    {
                        Debug.Log("EnemyHealth FOUND on: " + collision.name);
                        enemy.TakeDamage(damage);
                    }
                    else
                    {
                        Debug.LogWarning("No EnemyHealth script found on: " + collision.name);
                    }

                    Destroy(gameObject);
                }
        */
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {


        NewPlayerHealth health = collision.collider.GetComponent<NewPlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    
 
 

        /*
        Debug.Log("Collision with: " + collision.collider.name);

        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
      }
        */
    }

}