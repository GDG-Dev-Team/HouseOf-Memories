using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("EnemyHealth")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Damage Cooldown")]
    [SerializeField] private float damageCooldown = 1f; // ��� �������� ��� �������
    private float lastDamageTime;

    [Header("ChangColor")]
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hurtColor = Color.red;
    private Color originalColor;
    [SerializeField] private float flashDuration = 0.1f;

    private void Start()
    {
        currentHealth = maxHealth;
        lastDamageTime = -damageCooldown; // ���� ������ ������ ��� ���

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

    }

    public void TakeDamage(float damage)
    {
        // �� �� ��� ���� �� ��� ����
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;

            StartCoroutine(FlashEffect());

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                
            }
        }
    }

   IEnumerator FlashEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hurtColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
        }
    }




}