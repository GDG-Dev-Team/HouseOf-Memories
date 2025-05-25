using UnityEngine;

public class BulletNotFollow : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rb;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject target = GetClosestPlayer(players);

        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;

            // LOG direction
            Debug.Log("Bullet direction: " + direction);

            rb.linearVelocity = direction * speed;
        }

        Destroy(gameObject, 2f);
    }

    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 current = transform.position;

        foreach (GameObject p in players)
        {
            float dist = Vector3.Distance(p.transform.position, current);
            if (dist < minDist)
            {
                minDist = dist;
                closest = p;
            }
        }

        return closest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
