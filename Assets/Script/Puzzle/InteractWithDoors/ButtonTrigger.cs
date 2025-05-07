using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{

    [SerializeField] SmoothDoor doorScript;
    [SerializeField] string playerTag = "Player";

private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            doorScript.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            doorScript.CloseDoor();
        }
    }
}
