using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    [SerializeField] float explosionDelay = 3f;
    [SerializeField] float explosionRadius = 2f;
    [SerializeField] GameObject explosionEffect;

    private Transform player;
    private float timer;
    private bool hasExploded = false;
    private bool timerStarted = false;
    private ExplodingEnemyMove moveScript;

    [Header("For Attack")]
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float attackCooldown = 1f;
    private float lastAttackTime = -Mathf.Infinity;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        moveScript = GetComponent<ExplodingEnemyMove>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasExploded || player == null || moveScript == null) return;

        if (!timerStarted && moveScript.IsChasing)
        {
            timerStarted = true;
            timer = explosionDelay;

            if (anim != null)
                anim.SetBool("isPreparing", true); 
        }

        if (timerStarted)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Explode(); 
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasExploded) return;

        if (other.CompareTag("Player"))
        {
            Explode();
        }
    }





    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        if (hasExploded) return;

        if (other.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }


    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (anim != null)
        {
            anim.SetTrigger("Explode");
            anim.SetBool("isPreparing", false);
        }

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, 0.5f); 
    }


    public void ExplodeDamage() 
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                var health = hit.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(1);
                }
            }
        }
    }




    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
