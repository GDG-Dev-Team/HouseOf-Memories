using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player Health References")]
    [SerializeField] private NewPlayerHealth boyHealth;
    [SerializeField] private NewPlayerHealth girlHealth;

    [Header("Scene Names")]
    [SerializeField] private string boyWinsSceneName = "BoyWinsScene"; // Type your scene name here
    [SerializeField] private string girlWinsSceneName = "GirlWinsScene"; // Type your scene name here

    [Header("Collection System Reference")]
    [SerializeField] private CollectionManager collectionManager;

    // This function is called when the script is enabled
    private void OnEnable()
    {
        // Subscribe our functions to the OnDeath events of the players.
        // If the 'boyHealth' script fires its OnDeath event, our 'OnPlayerDeath' function will be called.
        if (boyHealth != null)
        {
            boyHealth.OnDeath.AddListener(() => OnPlayerDeath(boyHealth.gameObject));
        }

        if (girlHealth != null)
        {
            girlHealth.OnDeath.AddListener(() => OnPlayerDeath(girlHealth.gameObject));
        }

        if (collectionManager != null)
        {
            collectionManager.OnAllItemsCollected.AddListener(HandleBoyCollectionWin);
        }
    }

    // It's good practice to unsubscribe when the script is disabled or destroyed
    private void OnDisable()
    {
        // Unsubscribe to prevent errors if the GameManager is destroyed before the players.
        if (boyHealth != null)
        {
            boyHealth.OnDeath.RemoveAllListeners(); // A simple way to clean up
        }
        if (girlHealth != null)
        {
            girlHealth.OnDeath.RemoveAllListeners();
        }
        if (collectionManager != null)
        {
            collectionManager.OnAllItemsCollected.RemoveListener(HandleBoyCollectionWin);
        }
    }
    public void HandleBoyCollectionWin()
    {
        // This function is called when the OnAllItemsCollected event is fired.
        Debug.Log("Boy wins by collecting all items! Loading scene...");
        SceneManager.LoadSceneAsync(boyWinsSceneName);
    }
    // This single function will handle the death of ANY player
    private void OnPlayerDeath(GameObject deadPlayer)
    {
        Debug.Log(deadPlayer.name + " has been defeated!");

        // Now we use the marker component to check WHO died.
        if (deadPlayer.GetComponent<PlayerBoy>() != null)
        {
            // The Boy died, so the Girl wins.
            Debug.Log("Loading Girl Wins Scene...");
            SceneManager.LoadSceneAsync(girlWinsSceneName);
        }
        else if (deadPlayer.GetComponent<PlayerGirl>() != null)
        {
            // The Girl died, so the Boy wins.
            Debug.Log("Loading Boy Wins Scene...");
            SceneManager.LoadSceneAsync(boyWinsSceneName);
        }
    }
}
