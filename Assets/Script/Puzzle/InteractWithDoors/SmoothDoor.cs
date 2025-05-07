using UnityEngine;

public class SmoothDoor : MonoBehaviour
{
    [SerializeField] Transform doorTransform;
    [SerializeField] Vector3 openOffset = new Vector3(0, 2f, 0); //  ÇáÇÑÊÝÇÚ 
    [SerializeField] float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;


    void Start()
    {
        closedPosition = doorTransform.position;
        targetPosition = closedPosition;

    }

 
    void Update()
    {
        doorTransform.position = Vector3.MoveTowards(doorTransform.position, targetPosition, openSpeed * Time.deltaTime);
    }

    public void OpenDoor()
    {
        isOpening = true;
        targetPosition = closedPosition + openOffset;
    }

    public void CloseDoor()
    {
        isOpening = false;
        targetPosition = closedPosition;
    }






}
