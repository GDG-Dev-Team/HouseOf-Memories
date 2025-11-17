using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class NewPlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;
    private int _currentHealth;

    [Header("Invincibility")]
    [SerializeField] private float invincibilityDuration = 0.5f;
    private bool _isInvincible = false;
    [Tooltip("Event fired when health changes. Sends the new health value.")]
    public UnityEvent<int> OnHealthChanged;

    [Tooltip("Event fired when the character dies.")]
    public UnityEvent OnDeath;

    // --- Public Property to access current health safely ---
    public int CurrentHealth
    {
        get { return _currentHealth; }
    }

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        // If we are currently invincible, don't do anything.
        if (_isInvincible)
        {
            return;
        }

        // If we are already dead, don't do anything.
        if (_currentHealth <= 0)
        {
            return;
        }

        _currentHealth -= damageAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);

        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Health is now: " + _currentHealth);

        // Fire the event to notify other scripts (like the UI) that health has changed.
        OnHealthChanged.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            // Fire the death event.
            OnDeath.Invoke();
            // We usually let the death event handler decide to destroy the object.
            // For now, let's just make it inactive.
            // Destroy(gameObject); 
        }
        else
        {
            // If we are not dead, become invincible for a short time.
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        _isInvincible = false;
    }
}
