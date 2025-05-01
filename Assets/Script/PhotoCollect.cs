using UnityEngine;

public class PhotoCollect : MonoBehaviour
{
    public static PhotoCollect instance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectionManager.instance.CollectItem();
            Destroy(gameObject);
        }
    }
}
