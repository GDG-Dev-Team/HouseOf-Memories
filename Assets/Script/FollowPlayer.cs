using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("TargetToFollow")]
    public float speed;
    public float LineOfSite;
    private Transform player;//target

    [Header("shoot")]
    public float shootingRange;
    public GameObject bullet;//shoot bullet
    public GameObject bulletParent;//location where to shoot
    public float FireRate = 1f;
    private float nextFireTime;
    
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

   
    void Update()
    {
        float distanceFormPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFormPlayer < LineOfSite && distanceFormPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFormPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + FireRate;

        }
           
          
    }

    private void OnDrawGizmosSelected()//space if player close of it the enemy attack him
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }







}
