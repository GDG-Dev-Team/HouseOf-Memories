using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{

    [SerializeField] private GameObject DoorGameObjectA;
    [SerializeField] private GameObject DoorGameObjectB;
   

    private IDoor doorA;
    private IDoor doorB;
    private void Awake()
    {
        doorA = DoorGameObjectA.GetComponent<IDoor>();
        doorB = DoorGameObjectB.GetComponent<IDoor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))//open door
            doorA.OpenDoor();
        {
            if (Input.GetKeyDown(KeyCode.G))//open door
                doorA.CloseDoor();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            doorB.OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            doorB.CloseDoor();
        }

    }
}
