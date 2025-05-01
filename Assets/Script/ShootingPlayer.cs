using Mono.Cecil;
using UnityEngine;

public class ShootingPlayer : MonoBehaviour
{
    [Header("bulletdontfollow")]
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;


    //[Header("bulletfollow")]
    //public float speed = 5;
    //Transform player;//target





    void Start()
    {
        //"bulletdontfollow"
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");// Õœœ «·Âœ›
        Vector2 moveDir = (target.transform.position - transform.position).normalized* speed;//« Ã«Â «··«⁄»
        bulletRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);//  Õ—ﬂ »« Ã«Â «··«⁄»
        Destroy(this.gameObject, 2);

        ////"bulletfollow"
        //player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    
    void Update()
    {
        ////"bulletfollow"
        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
