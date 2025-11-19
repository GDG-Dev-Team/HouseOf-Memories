using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Transform iconContainer;// Parent object (e.g., a UI panel) where icons will appear
    [SerializeField] private int collectedCount = 0;
    [SerializeField] private int totalCount = 5;

    [Header("Events")]
    [Tooltip("Fired when the number of collected items equals the total count.")]
    public UnityEvent OnAllItemsCollected; // <-- ADD THIS EVENT

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateText();
    }


    public void CollectItem(Sprite itemSprite)
    {
        if (collectedCount < totalCount)
        {
            collectedCount++;
            UpdateText();


            GameObject newIcon = Instantiate(iconPrefab, iconContainer);
            Image img = newIcon.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = itemSprite;
            }
            
            if (collectedCount >= totalCount)
            {
                OnAllItemsCollected.Invoke(); // <-- INVOKE THE EVENT
            }

        }
    }

    void UpdateText()
    {
        countText.text = collectedCount + " / " + totalCount;
    }
}
