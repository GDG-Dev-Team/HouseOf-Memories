using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    public Animator animator;

    [Header("Attack Settings")]
    public float attackDuration = 0.5f;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask damageableLayer;
    public int damage = 1;

    private bool isAttacking = false;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("SimpleAttack script requires AudioSource!");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (isAttacking) return;

        if (Input.GetKeyDown(KeyCode.M))
            StartCoroutine(Attack());
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        if (audioSource != null && attackClip != null)
            audioSource.PlayOneShot(attackClip);

        yield return new WaitForSeconds(attackDuration / 3f);
        DoAttack();
        yield return new WaitForSeconds(attackDuration * 2f / 3f);

        isAttacking = false;
    }

    void DoAttack()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageableLayer);

        foreach (Collider2D target in hitTargets)
        {
            if (target.gameObject == this.gameObject) continue;

            NewPlayerHealth health = target.GetComponent<NewPlayerHealth>();
            if (health != null)
                health.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
