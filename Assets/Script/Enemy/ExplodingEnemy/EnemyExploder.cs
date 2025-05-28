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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        moveScript = GetComponent<ExplodingEnemyMove>();
    }

    void Update()
    {
        if (hasExploded || player == null || moveScript == null) return;

        if (!timerStarted && moveScript.IsChasing)
        {
            timerStarted = true;
            timer = explosionDelay;
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
        hasExploded = true;

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

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

        Destroy(gameObject);
    }





    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
