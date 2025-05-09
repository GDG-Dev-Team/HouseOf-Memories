using UnityEngine;

public class DoorTriggerAnim : MonoBehaviour
{
    [SerializeField] private DoorAnimated Door;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Door.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Door.CloseDoor();
        }


    }
}
