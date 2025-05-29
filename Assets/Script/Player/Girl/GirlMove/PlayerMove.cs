using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [HideInInspector]
    public static Rigidbody2D rb2d;

    [Header("Movement")]
    public bool enablePlatformMovement;
    public float speed;
    float input;
    Vector2 direction;
    Animator anim;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float DashPower = 24f;
    private float DashingTime = 0.2f;
    private float DashCoolDown = 1f;
    private bool isFacingRight = true;

    [SerializeField] private TrailRenderer tr;
    public int PlayerHealth = 3;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dashClip;
    [SerializeField] private AudioClip runClip;

    [SerializeField] private Transform cameraTransform;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        direction = new Vector2(input * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = direction;

        // 🔊 تشغيل صوت الجري
        if (Mathf.Abs(input) > 0.01f && !audioSource.isPlaying)
        {
            audioSource.clip = runClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if (Mathf.Abs(input) <= 0.01f && audioSource.clip == runClip)
        {
            audioSource.Stop();
        }
    }

    public void playermove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().x;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // 🔊 صوت الداش
        if (audioSource != null && dashClip != null)
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
