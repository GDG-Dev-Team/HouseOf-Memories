using UnityEngine;

public class TeleportationPlayer : MonoBehaviour
{
    private GameObject CurrentTeleporter;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && CurrentTeleporter != null)
        {
            transform.position = CurrentTeleporter.GetComponent<TeleporterScript>().GetDestination().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            CurrentTeleporter = other.gameObject;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == CurrentTeleporter)
        {
            CurrentTeleporter = null;
        }
        
    }
}
