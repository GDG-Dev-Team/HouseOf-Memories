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
    [SerializeField]  private float DashPower = 50f;
    private bool canDash = true;
    private bool isDashing;
    private float DashingTime = 0.2f;
    private float DashCoolDown = 1f;
    private bool isFacingRight = true;
    private bool IsRunning = false;
  

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip runClip;
    [SerializeField] private AudioClip dashClip;

    [SerializeField] private TrailRenderer tr;
    [SerializeField] int PlayerHealth = 3;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        direction = new Vector2(input * speed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = direction;

     
        if (input != 0 && !audioSource.isPlaying)
        {
            audioSource.clip = runClip;
            audioSource.loop = false;
            audioSource.Play();
        }
        else if (input == 0 && audioSource.clip == runClip)
        {
            audioSource.Stop();
        }

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

    [SerializeField] private Transform cameraTransform;

    private void Flip()
    {
        if (isFacingRight && input < 0f || !isFacingRight && input > 0f)
        {
            Debug.Log($"input: {input}, isFacingRight: {isFacingRight}");

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
