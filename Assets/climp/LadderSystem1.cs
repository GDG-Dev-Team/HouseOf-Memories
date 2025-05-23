using UnityEngine;

public class Ladder_System : MonoBehaviour
{
    [Header("Settings")]
    public float slopeAngle = 45f;
    public Vector2 platformOffset;

    private PlatformEffector2D effector;
    private Collider2D col;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        col = GetComponent<Collider2D>();
        InitializeLadder();
    }

    void InitializeLadder()
    {
        // إعداد المنحدر
        effector.surfaceArc = slopeAngle;
        col.usedByEffector = true;

        // تعيين الطبقة المبدئية
        gameObject.layer = LayerMask.NameToLayer("Background");
    }

    public void ActivatePlatform()
    {
        gameObject.layer = LayerMask.NameToLayer("Platforms");
    }

    public void DeactivatePlatform()
    {
        gameObject.layer = LayerMask.NameToLayer("Background");
    }
}