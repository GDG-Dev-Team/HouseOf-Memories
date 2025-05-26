using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
        public Sprite itemSprite; // Assign in Inspector per photo/item

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectionManager.instance.CollectItem(itemSprite);
            Destroy(gameObject);
        }
    }

}
