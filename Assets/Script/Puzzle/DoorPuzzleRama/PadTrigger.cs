using UnityEngine;

public class PadTrigger : MonoBehaviour
{
  [Header("Target Door")]
    [Tooltip("The door that this pressure pad will control.")]
    [SerializeField] private ControllableDoor targetDoor;

  
    private void OnTriggerEnter2D(Collider2D other)
    {
   
        if (other.CompareTag("Player"))
        {
         
            if (targetDoor != null)
            {
                targetDoor.Open();
            }
            else
            {
                Debug.LogError("PadTrigger is not linked to a door!");
            }
        }
    }

 
    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            if (targetDoor != null)
            {
                targetDoor.Close();
            }
        }
    }
}
