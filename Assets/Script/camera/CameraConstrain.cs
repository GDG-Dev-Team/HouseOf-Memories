using UnityEngine;

public class CameraConstrain : MonoBehaviour
{
 [Header("Targets & Bounds")]
    public Transform player;
    public Collider2D boundsCollider;   // the level boundary collider

    [Header("Follow Settings")]
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;

        // Good defaults for a 2D perspective camera
        cam.nearClipPlane = 0.1f;
        cam.farClipPlane  = 1000f;

        SnapNow();               // Make sure there is no first-frame flash
    }

    void LateUpdate()
    {
        if (player == null || boundsCollider == null) return;

        Vector3 desired = new Vector3(player.position.x,
                                      player.position.y,
                                      transform.position.z);     // keep current Z

        Vector3 clamped = ClampToBounds(desired);

        transform.position = Vector3.SmoothDamp(transform.position,
                                                clamped,
                                                ref velocity,
                                                smoothTime);
    }

    // ---------- Helper methods ----------
    void SnapNow()
    {
        if (player == null || boundsCollider == null) return;

        Vector3 snap = ClampToBounds(new Vector3(player.position.x,
                                                 player.position.y,
                                                 transform.position.z));
        transform.position = snap;
    }

    Vector3 ClampToBounds(Vector3 target)
    {
        Bounds b = boundsCollider.bounds;

        // 1. How far is the camera from the 2D plane?
        float camDist = Mathf.Abs(transform.position.z);

        // 2. How big is the frustum *at* that distance?
        float halfHeight = camDist * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float halfWidth  = halfHeight * cam.aspect;

        // 3. Shrink the bounds by that padding
        float minX = b.min.x + halfWidth;
        float maxX = b.max.x - halfWidth;
        float minY = b.min.y + halfHeight;
        float maxY = b.max.y - halfHeight;

        // 4. Clamp
        float clampedX = Mathf.Clamp(target.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.y, minY, maxY);

        return new Vector3(clampedX, clampedY, target.z);
    }
}
