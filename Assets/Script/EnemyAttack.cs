using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
   
    [Header("EnemyAttack")]
    [SerializeField] Transform target;
    private Rigidbody2D rb;

   


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Destroy(collision.gameObject);
            target = null;
        }
    }
   


}


