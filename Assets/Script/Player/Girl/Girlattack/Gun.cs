using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    void Update()
    {
        // نتاكد إذا اللاعب لف
        if (playerTransform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // لف السلاح
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // رجعه للوضع الطبيعي
        }
    }


}
