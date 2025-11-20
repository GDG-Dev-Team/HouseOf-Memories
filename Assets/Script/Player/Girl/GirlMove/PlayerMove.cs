using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [HideInInspector]
    public static Rigidbody2D rb2d;

    [Header("Movement")]
    public float speed;
    private float input;
    private Vector2 direction;
    private Animator anim;

    [Header("Dash")]
    [SerializeField] private float DashPower = 24f;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float DashingTime = 0.2f;
    [SerializeField] private float DashCoolDown = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip runClip;

    [SerializeField] private TrailRenderer tr;
    private bool isFacingRight = true;
    [SerializeField] private Transform cameraTransform;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        rb2d.linearVelocity = new Vector2(input * speed, rb2d.linearVelocity.y);

        // 🔊 تشغيل صوت الجري بدون قطع أصوات أخرى
        if (Mathf.Abs(input) > 0.01f && runClip != null)
        {
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(runClip);
        }
    }

    public void playermove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());

        Flip();
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // 🔊 صوت الداش
        if (dashClip != null)
            audioSource.PlayOneShot(dashClip);

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
        if ((isFacingRight && input < 0f) || (!isFacingRight && input > 0f))
        {
            Vector3 camPos = cameraTransform.position;
            Quaternion camRot = cameraTransform.rotation;
            cameraTransform.SetParent(null);

            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;

            cameraTransform.SetParent(transform);
            cameraTransform.position = camPos;
            cameraTransform.rotation = camRot;
        }
    }
}
