using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    
    private Animator anim;
    public static Rigidbody2D rb2d;
    [SerializeField] Transform targetTransform;

    [Header("move")]
    Vector2 direction;
    float input;
    [SerializeField] float speed;
    
    private Camera mainCamera;
    [SerializeField] LayerMask mouseAimMask;


    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float DashPower = 24f;
    private float DashingTime = 0.2f;
    private float DashCoolDown = 1f;
    private bool isFacingRight = true;

    [SerializeField]private TrailRenderer tr;
    [SerializeField] int PlayerHealth = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
      
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, mouseAimMask);

        if (hit.collider != null)
        {
            targetTransform.position = hit.point;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
       // direction = new Vector2(input * speed, rb2d.linearVelocityY);
       // rb2d.linearVelocity = direction;
        direction = new Vector2(input * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = direction;

        anim.SetBool("IsRunning", input != 0 && !isDashing);
        anim.SetBool("VelocityY", rb2d.linearVelocity.y != 0);
        
        if (Input.GetKeyDown(KeyCode.RightShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        Flip();

        anim.SetBool("IsRunning", input != 0 && !isDashing);

    }

    public void mover(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().x;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        anim.SetBool("IsDashing", true);


        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.linearVelocity = new Vector2(transform.localScale.x * DashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(DashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(DashCoolDown);
        canDash = true;
    }


      
    [SerializeField] private Transform cameraTransform; 

    private void Flip()
    {
        if (isFacingRight && input < 0f || !isFacingRight && input > 0f)
        {
            Vector3 camPosition = cameraTransform.position;
            Quaternion camRotation = cameraTransform.rotation;
            cameraTransform.SetParent(null);

            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;

            cameraTransform.SetParent(transform);
            cameraTransform.position = camPosition;
            cameraTransform.rotation = camRotation;

        }
    }
}
