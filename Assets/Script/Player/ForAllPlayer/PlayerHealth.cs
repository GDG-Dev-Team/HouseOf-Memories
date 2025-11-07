using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public int Health = 3;

    private bool isInvincible = false;
    [SerializeField] private float invincibilityDuration = 0.5f;

    void Awake()
    {
        if (!instance)
            instance = this;
    }

 
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
            StartCoroutine(TakeDamageWithCooldown(damage));
    }

    private IEnumerator TakeDamageWithCooldown(int damage)
    {
        isInvincible = true;

        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Destroy(gameObject);
            Time.timeScale = 0;
            SceneManage.instance.LoadMenu("Lose Menu");
        }

        UpdateHearts();

        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void UpdateHearts()
    {
        health1.SetActive(Health >= 1);
        health2.SetActive(Health >= 2);
        health3.SetActive(Health >= 3);
    }
}
