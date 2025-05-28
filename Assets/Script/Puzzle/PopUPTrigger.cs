using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class PopUPTrigger : MonoBehaviour
{
    public GameObject popupPanel;
    public Button exitButton;

    private bool isPlayerNearby = false;

    void Start()
    {
        popupPanel.SetActive(false);

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ClosePopup);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.P))
        {
            popupPanel.SetActive(true);
        }
    }

    void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerNearby = false;
    }
}
