using Mono.Cecil;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ShootingPlayer : MonoBehaviour
{
    //[Header("bulletdontfollow")]
    //GameObject target;
    //public float speed;
    //Rigidbody2D bulletRB;
    //private Vector2 moveDirection;



    [Header("bulletfollow")]
    public float speed = 5;
    Transform player;//target

    [Header("bulletDestroy")]
    [SerializeField] float DestroyTime = 10f;





    void Start()
    {
        ////"bulletdontfollow"
        //bulletRB = GetComponent<Rigidbody2D>();
        //target = GameObject.FindGameObjectWithTag("Player");//���� �����
        //Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;//����� ������
        //bulletRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);//����� ������ ������
        //Destroy(this.gameObject, 2);



        //"bulletfollow"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = GetClosestPlayer(players).transform;

        Destroy(gameObject, DestroyTime);

    }


    void Update()
    {
        //"bulletfollow"
       
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        
    }

    GameObject GetClosestPlayer(GameObject[] players)
    {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject GirlOrBoy in players)
        {
            float distance = Vector3.Distance(GirlOrBoy.transform.position, currentPos);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = GirlOrBoy;
            }
        }

        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Destroy(gameObject); 
        }
    }
}