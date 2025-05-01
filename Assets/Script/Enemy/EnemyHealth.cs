using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("EnemyHealth")]
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
      
    }
    private void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
 

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Die()
    {
       // ·«“„ ÌﬂÊ‰ ⁄‰œﬂ  —«‰“Ì‘‰ «”„Â "Die" ›Ì Animator
        Destroy(gameObject, 1f); // ‰Œ·ÌÂ Ì‰„”Õ »⁄œ 1 À«‰Ì… ⁄‘«‰ Ì‘ €· «·√‰Ì„Ì‘‰
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      

        if (collision.gameObject.CompareTag("Player"))
        {
           collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1); // √Ê √Ì ﬁÌ„… Õ”» ·⁄» ﬂ
        }
    }
}
