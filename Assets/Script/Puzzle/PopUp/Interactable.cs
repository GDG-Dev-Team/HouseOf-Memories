using UnityEngine;

public class Interactable : MonoBehaviour
{

    public string message;
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>().PopUp(message);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>().ClosePopUp();
        }
    }
}
