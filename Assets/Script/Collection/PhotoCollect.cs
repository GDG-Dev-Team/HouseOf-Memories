using UnityEngine;

public class PhotoCollect : MonoBehaviour
{
    public Sprite itemSprite; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectionManager.instance.CollectItem(itemSprite);
            Destroy(gameObject);
        }
    }
}
