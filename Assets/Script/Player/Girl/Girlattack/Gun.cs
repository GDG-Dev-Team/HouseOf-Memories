using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    void Update()
    {
        Vector3 gunScale = transform.localScale;
        float direction = Mathf.Sign(playerTransform.localScale.x);
        gunScale.x = Mathf.Abs(gunScale.x) * direction;
        transform.localScale = gunScale;
    }
}
