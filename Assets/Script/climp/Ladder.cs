using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float TopY => _collider.bounds.max.y;
    public float BottomY => _collider.bounds.min.y;

    private Collider2D _collider;

    private void Awake() => _collider = GetComponent<Collider2D>();

    public Vector2 GetSnapPosition(Vector2 playerPosition) =>
        new Vector2(transform.position.x, playerPosition.y);
}