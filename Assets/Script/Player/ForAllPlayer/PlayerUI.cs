using UnityEngine;

public class PlayerUI : MonoBehaviour
{
   [Header("UI References")]
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;

    [Header("Required Component")]
    [SerializeField] private NewPlayerHealth characterHealth; // A reference to the health script on this SAME object.

    private void OnEnable()
    {
        // Subscribe our functions to the events on the CharacterHealth script.
        characterHealth.OnHealthChanged.AddListener(UpdateHearts);
        characterHealth.OnDeath.AddListener(HandlePlayerDeath);
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent errors when the object is destroyed.
        characterHealth.OnHealthChanged.RemoveListener(UpdateHearts);
        characterHealth.OnDeath.RemoveListener(HandlePlayerDeath);
    }
    
    private void Start()
    {
        // Initial UI update when the game starts.
        UpdateHearts(characterHealth.CurrentHealth);
    }

    // This function is now triggered by the OnHealthChanged event from CharacterHealth.
    public void UpdateHearts(int currentHealth)
    {
        health1.SetActive(currentHealth >= 1);
        health2.SetActive(currentHealth >= 2);
        health3.SetActive(currentHealth >= 3);
    }

    // This function is now triggered by the OnDeath event from CharacterHealth.
    public void HandlePlayerDeath()
    {
        Debug.Log("Player has died! Loading lose menu.");
        Time.timeScale = 0;
        // Assuming you have a SceneManage script. If not, comment this line out.
        // SceneManage.instance.LoadMenu("Lose Menu");
        Destroy(gameObject); // Or handle respawning
    }
}
