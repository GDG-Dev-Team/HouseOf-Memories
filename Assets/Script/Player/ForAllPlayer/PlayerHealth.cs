using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    private int currentHealth;

    public float invincibleTime = 1f;
    private float lastDamageTime;

        
    void Start()
    {

        currentHealth = maxHealth;

    }



    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Remaining Health: " + currentHealth);


        if (currentHealth <= 0)
        {
            Die();
            Debug.Log("1111111111111d");
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }

    //private void GetTarget()
    //{
    //    target = GameObject.FindGameObjectWithTag("Player").transform;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            maxHealth--;
        Debug.Log(maxHealth);
        if (maxHealth <= 0)
            Debug.Log("playerdie");
        Destroy(gameObject);
    }



}
