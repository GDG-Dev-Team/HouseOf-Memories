using UnityEngine;

public class DoorAutomatic : MonoBehaviour
{
    [SerializeField] private GameObject doorGameObject;
    private IDoor door;

    private void Awake(){
        door = doorGameObject.GetComponent<IDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player")){
            //player entered collider
            door.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.CompareTag("Player")){
            //player exited collider
            door.CloseDoor();
        }
    }

}
