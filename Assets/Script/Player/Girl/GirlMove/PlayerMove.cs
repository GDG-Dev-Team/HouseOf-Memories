using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public static Rigidbody2D rb2d;
    public bool enablePlatformMovement;
    [Header("move")]
    Vector2 direction;
    public float speed;
    float input;
    Animator anim;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float DashPower = 24f;
    private float DashingTime = 0.2f;
    private float DashCoolDown = 1f;
    private bool isFacingRight = true;

    [SerializeField]
    private TrailRenderer tr;
    public int PlayerHealth = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Time.timeScale = 1;
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        Flip();

        //velocity
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        direction = new Vector2(input * speed, rb2d.linearVelocityY);
        rb2d.linearVelocity = direction;
    }

    public void playermove(InputAction.CallbackContext context)
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
    void Update2()
    {
        if (enablePlatformMovement)
        {
            // حركة أفقية عادية
            float move = Input.GetAxis("Horizontal");
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(move * speed, 0);
        }
    }
    //playerhealth
    //private void OnTriggerEnter2D(Collider2D collision) //OnTriggerEnter2D ��� �������� ����
    //{
    //    if (collision.tag == "Enemy")
    //        PlayerHealth--;
    //    Debug.Log(PlayerHealth);
    //    if (PlayerHealth <= 0)
    //        Debug.Log("your Dead");
    //}
}
