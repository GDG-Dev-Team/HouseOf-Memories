using UnityEngine;

public class GhostMove2 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 moveDirection = new Vector2(1f, 0.25f);
    [SerializeField] GameObject WallCheck, UpCheck, DownCheck;
    [SerializeField] Vector2 WallCheckSize, DownCheckSize,UpCheckSize;
    [SerializeField] LayerMask groundLayer, platform,Wall;
    [SerializeField] bool goingUp = true;
    //check if touch or not
    private bool touchedDown, touchedUp, touchedWall;
    private Rigidbody2D EnemyRB;
    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>(); 
    }

   
    void Update()
    {
        HitLogic();
    }

    void FixedUpdate()
    {
        EnemyRB.linearVelocity = moveDirection*moveSpeed;
    }

    void HitLogic()
    {
        touchedWall = HitDetector(WallCheck, WallCheckSize, (groundLayer | platform | Wall));
        touchedUp = HitDetector(UpCheck, UpCheckSize, (groundLayer | platform | Wall));
        touchedDown = HitDetector(DownCheck, DownCheckSize, (groundLayer | platform | Wall));

        if (touchedWall)
            Flip();
        if (touchedUp && goingUp)
            ChangeYDirection();
        if (touchedDown && !goingUp)
            ChangeYDirection();
  
    }


    bool HitDetector(GameObject gameObject, Vector2 size, LayerMask Layer)
      {
        return Physics2D.OverlapBox(gameObject.transform.position, size, 0f, Layer);
     }

    void ChangeYDirection()
    {
        moveDirection.y = -moveDirection.y;
        goingUp = !goingUp;
    }

    void Flip()
    {
        transform.Rotate(new Vector2(0, 180));
        moveDirection.x = -moveDirection.x;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(DownCheck.transform.position, DownCheckSize);
        Gizmos.DrawCube(UpCheck.transform.position, UpCheckSize);
        Gizmos.DrawCube(WallCheck.transform.position, WallCheckSize);
    }




}