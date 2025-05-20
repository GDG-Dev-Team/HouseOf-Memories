using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;

public class DialogBoxLogic : MonoBehaviour
{
    [SerializeField] FreeRoamObjectBehaviour objectBehaviour;
    [SerializeField] Unit troop;
    [SerializeField] SignalItem contextOn;
    [SerializeField] SignalItem contextOff;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TMP_Text dialogText;
    [SerializeField] string dialog;
    [SerializeField] string popUpMessage;
    [SerializeField] bool playerInRange = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectBehaviour == FreeRoamObjectBehaviour.DIALOG)
        {
            if(Input.GetKeyDown(KeyCode.Q)&& playerInRange)
            {
                if (dialogueBox.activeInHierarchy)
                {
                    dialogueBox.SetActive(false);
                }
                else
                {
                    dialogueBox.SetActive(true);
                }
            }
        }
        else if (objectBehaviour == FreeRoamObjectBehaviour.ITEM_RECEIVER)
        {
          if(Input.GetKeyDown(KeyCode.Q) && playerInRange)
            {
                troop = Instantiate(troop);
               // GameManager.AddTroop(troop);
                PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
                pop.PopUp(popUpMessage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && objectBehaviour == FreeRoamObjectBehaviour.DIALOG){
            contextOn.Raise();
            dialogText.text = dialog;
            playerInRange = true;

        }
        else if(collision.CompareTag("Player") && objectBehaviour == FreeRoamObjectBehaviour.ITEM_RECEIVER)
        {
            contextOn.Raise();
            dialogText.text = "You found an item!"; // √Ê √Ì ‰’ Ì‰«”»ﬂ
            playerInRange = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contextOff.Raise(); // «Œ Ì«—Ì
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}


