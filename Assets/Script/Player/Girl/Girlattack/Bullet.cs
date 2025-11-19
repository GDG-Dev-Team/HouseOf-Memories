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
                if (collision.CompareTag("Boy"))
                {
                    Debug.Log("Enemy TAG detected: " + collision.name);
                    NewPlayerHealth health = collision.GetComponent<NewPlayerHealth>();
                    if (health != null)
                    {
                        Debug.Log("EnemyHealth FOUND on: " + collision.name);
                        health.TakeDamage(damage);
                    }
                    else
                    {
                        Debug.LogWarning("No EnemyHealth script found on: " + collision.name);
                    }

                    Destroy(gameObject);
                }
        
    }



  
}