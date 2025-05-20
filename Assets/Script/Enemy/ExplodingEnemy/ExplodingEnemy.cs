using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject explosionEffect;
     private ExplodingEnemyMove enemyMove;

    [Header("Explosion Settings")]
    public float explosionDelay = 2f;
    public float explosionRadius = 2f;
    public float damage = 30f;
    public LayerMask playerLayer;
    private bool isExploding = false;
    [SerializeField] private int damageAmount = 1;
   

    void Start()
    {
        enemyMove = GetComponent<ExplodingEnemyMove>();
    }

    void Update()
    {
        if (enemyMove.isChasingPlayer && !isExploding)
        {
            isExploding = true;
            Invoke(nameof(Explode), explosionDelay);
        }
    }

    void Explode()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, explosionRadius, playerLayer);

        if (hit != null)
        {
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }

        
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}