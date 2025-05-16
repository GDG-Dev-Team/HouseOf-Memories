using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    
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
        direction = new Vector2(input * speed, rb2d.linearVelocityY);
        rb2d.linearVelocity = direction;
        if (Input.GetKeyDown(KeyCode.RightShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        Flip();
    }

    public void mover(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().x;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
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

    private void Flip()
    {
        if (isFacingRight && input < 0f || !isFacingRight && input > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }
}
