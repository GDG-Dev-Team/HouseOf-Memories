using Unity.Cinemachine;
using UnityEngine;

public class Spider2 : MonoBehaviour
{


    [SerializeField] Transform target;
    private Rigidbody2D rb;
    //[SerializeField] float speed = 3f;
    //[SerializeField] float RotateSpeed = 0.0025f;
   

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();


    }


    //void Update()
    //{
    //    if (!target)
    //    {
    //        GetTarget();
    //    }
    //    else
    //    {
    //        RotateTowardsTarget();
    //    }
    //}


    //private void FixedUpdate()
    //{
    //    rb.linearVelocity = transform.up * speed;
    //}

    //private void RotateTowardsTarget()
    //{
    //    Vector2 targetDirection = target.position - transform.position;
    //    float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
    //    Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
    //    transform.localRotation = Quaternion.Slerp(transform.localRotation, q, RotateSpeed);

    //}

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
