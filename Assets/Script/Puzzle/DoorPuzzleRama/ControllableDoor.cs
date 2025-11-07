using UnityEngine;

public class ControllableDoor : MonoBehaviour
{
    [Header("Door Components")]
    [Tooltip("The Particle System or VFX to play when the door is open.")]
    [SerializeField] private ParticleSystem openVFX;


    private Collider2D doorCollider;

    // Awake is called before the first frame update.
    private void Awake()
    {
       
        doorCollider = GetComponent<BoxCollider2D>();

        
        if (doorCollider == null)
        {
            Debug.LogError("ControllableDoor is missing a Collider2D component!");
        }
    }

    // A public function that can be called by other scripts to open the door.
    public void Open()
    {
        
        doorCollider.enabled = false;

        // If a VFX has been assigned, play it.
        if (openVFX != null)
        {
            openVFX.Play();
        }
    }

    // A public function that can be called by other scripts to close the door.
    public void Close()
    {
        
        doorCollider.enabled = true;

        // If a VFX has been assigned, stop it.
        if (openVFX != null)
        {
            openVFX.Stop();
        }
    }
}
