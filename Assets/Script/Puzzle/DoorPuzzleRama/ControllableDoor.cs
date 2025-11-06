using UnityEngine;

public class ControllableDoor : MonoBehaviour
{
    [Header("Door Components")]
    [Tooltip("The Particle System or VFX to play when the door is open.")]
    [SerializeField] private ParticleSystem openVFX;

    // We will get these components automatically.
    private SpriteRenderer spriteRenderer;
    private Collider2D doorCollider;

    // Awake is called before the first frame update.
    private void Awake()
    {
        // Get references to this object's own components.
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();

        // Error checking to make sure the door is set up correctly.
        if (spriteRenderer == null)
        {
            Debug.LogError("ControllableDoor is missing a SpriteRenderer component!");
        }
        if (doorCollider == null)
        {
            Debug.LogError("ControllableDoor is missing a Collider2D component!");
        }
    }

    // A public function that can be called by other scripts to open the door.
    public void Open()
    {
        // Deactivate the sprite and collider.
        spriteRenderer.enabled = false;
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
        // Re-activate the sprite and collider.
        spriteRenderer.enabled = true;
        doorCollider.enabled = true;

        // If a VFX has been assigned, stop it.
        if (openVFX != null)
        {
            openVFX.Stop();
        }
    }
}
