using UnityEngine;

public class DoorInteractButton : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    private IDoor door;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRadius = 10f;
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(playerTransform.position, interactRadius);
            foreach (Collider2D collider2D in collider2DArray)
            {
                door = collider2D.GetComponent<IDoor>();
                if (door != null)
                {
                    //there is door in range
                    door.ToggleDoor();
                }
            }

        }
    }
}
