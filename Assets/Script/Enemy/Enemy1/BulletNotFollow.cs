using UnityEngine;

public class BulletNotFollow : MonoBehaviour
{

    [Header("bulletdontfollow")]
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    
   





    void Start()
    {
        //"bulletdontfollow"
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");//          
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;//            
        bulletRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);//                   
        Destroy(this.gameObject, 2);


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
