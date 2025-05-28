using UnityEngine;

public class GhostChaseAndShoot : MonoBehaviour
{
    [Header("Default Movement")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Vector2 moveDirection = new Vector2(1f, 0.25f);
    [SerializeField] GameObject WallCheck, UpCheck, DownCheck;
    [SerializeField] Vector2 WallCheckSize, DownCheckSize, UpCheckSize;
    [SerializeField] LayerMask groundLayer, platform, Wall;
    [SerializeField] bool goingUp = true;

    [Header("Chase & Shoot")]
    [SerializeField] float lineOfSight = 6f;
    [SerializeField] float shootingRange = 3f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParent;
    [SerializeField] float shootCooldown = 1f;
    private float nextFireTime;

    private Transform player;
    private Rigidbody2D rb;
    private bool touchedWall, touchedUp, touchedDown;
    private bool isChasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        player = GetClosestPlayer(GameObject.FindGameObjectsWithTag("Player")).transform;


        float distance = Vector2.Distance(player.position, transform.position);
        isChasing = distance <= lineOfSight;

        if (isChasing && distance <= shootingRange && Time.time >= nextFireTime)
        {
            Shoot();
        }

        if (!isChasing)
        {
            HitLogic(); // Only do the ghost-style collision if not chasing
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            Vector2 chaseDir = (player.position - transform.position).normalized;
            rb.linearVelocity = chaseDir * moveSpeed;
        }
        else
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
    }

    void HitLogic()
    {
        touchedWall = HitDetector(WallCheck, WallCheckSize, (groundLayer | platform | Wall));
        touchedUp = HitDetector(UpCheck, UpCheckSize, (groundLayer | platform | Wall));
        touchedDown = HitDetector(DownCheck, DownCheckSize, (groundLayer | platform | Wall));

        if (touchedWall) Flip();
        if (touchedUp && goingUp) ChangeYDirection();
        if (touchedDown && !goingUp) ChangeYDirection();
    }

    bool HitDetector(GameObject check, Vector2 size, LayerMask layer)
    {
        return Physics2D.OverlapBox(check.transform.position, size, 0f, layer);
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        moveDirection.x = -moveDirection.x;
    }

    void ChangeYDirection()
    {
        moveDirection.y = -moveDirection.y;
        goingUp = !goingUp;
    }

    void Shoot()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        nextFireTime = Time.time + shootCooldown;
    }

    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(p.transform.position, currentPos);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = p;
            }
        }

        return closest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(DownCheck.transform.position, DownCheckSize);
        Gizmos.DrawCube(UpCheck.transform.position, UpCheckSize);
        Gizmos.DrawCube(WallCheck.transform.position, WallCheckSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}

