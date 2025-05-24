using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    void Update()
    {
        // ‰ «ﬂœ ≈–« «··«⁄» ·›
        if (playerTransform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // ·› «·”·«Õ
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // —Ã⁄Â ··Ê÷⁄ «·ÿ»Ì⁄Ì
        }
    }


}
