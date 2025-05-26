using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    void Update()
    {
        Vector3 currentScale = transform.localScale;

        if (playerTransform.localScale.x < 0)
        {
            // البنت لافة يسار → لف السلاح كمان يسار
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x) * -1, currentScale.y, currentScale.z);
        }
        else
        {
            // البنت لافة يمين → خليه طبيعي
            transform.localScale = new Vector3(Mathf.Abs(currentScale.x), currentScale.y, currentScale.z);
        }
    }
}
