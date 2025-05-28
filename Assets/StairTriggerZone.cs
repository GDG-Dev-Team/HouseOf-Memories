using UnityEngine;

public class StairTriggerZone : MonoBehaviour
{
     public Collider2D secondFloorCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && secondFloorCollider != null)
        {
            secondFloorCollider.enabled = false;
            Debug.Log("Player entered stairs - second floor collider disabled");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && secondFloorCollider != null)
        {
            secondFloorCollider.enabled = true;
            Debug.Log("Player exited stairs - second floor collider enabled");
        }
    }
}
