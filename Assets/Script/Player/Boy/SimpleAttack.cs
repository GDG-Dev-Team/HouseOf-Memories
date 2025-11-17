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
            Debug.LogError("SimpleAttack script needs an AudioSource component on the same GameObject!");
            this.enabled = false;
        }
    }
    void Update()
    {
        if (isAttacking) return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(Attack());
        }
    }

    System.Collections.IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        // ✅ تشغيل الصوت هنا
        if (audioSource != null && attackClip != null)
        {
            audioSource.clip = attackClip;
            audioSource.Play();
        }

        yield return new WaitForSeconds(attackDuration / 3f);
        audioSource.Stop();
        DoAttack();
        yield return new WaitForSeconds(attackDuration * 2f / 3f);

        isAttacking = false;
    }

    void DoAttack()
    {
        /*
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("ضرب العدو: " + enemy.name);

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
        */


        // CHANGED: Use the new damageableLayer variable
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageableLayer);

        // CHANGED: The loop now checks for CharacterHealth
        foreach (Collider2D target in hitTargets)
        {
            Debug.Log("Sword hit: " + target.name);

            if (target.gameObject == this.gameObject)
            {
                continue; // 'continue' means "skip the rest of this loop iteration and move to the next one"
            }
        
            // We look for our universal CharacterHealth script
            NewPlayerHealth health = target.GetComponent<NewPlayerHealth>();
            if (health != null)
            {
                // We found a health component, so deal damage!
                health.TakeDamage(damage);
            }
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
